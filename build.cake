#tool "nuget:?package=OpenCover"
#tool "nuget:?package=Codecov"
#tool "nuget:?package=PublishCoverity"

#addin "Cake.Incubator"
#addin "nuget:?package=Cake.Codecov"

var target = Argument("target", "Default");
var extensionsVersion = Argument("version", "1.3.1");

var buildConfiguration = "Release";
var projectName = "Todoist.Net";
var testProjectName = "Todoist.Net.Tests";
var projectFolder = string.Format("./src/{0}/", projectName);
var testProjectFile = string.Format("./src/{0}/{0}.csproj", testProjectName);

Task("UpdateBuildVersion")
  .WithCriteria(BuildSystem.AppVeyor.IsRunningOnAppVeyor)
  .Does(() =>
{
    var buildNumber = BuildSystem.AppVeyor.Environment.Build.Number;

    BuildSystem.AppVeyor.UpdateBuildVersion(string.Format("{0}.{1}", extensionsVersion, buildNumber));
});

Task("NugetRestore")
  .Does(() =>
{
    DotNetCoreRestore();
});

Task("UpdateAssemblyVersion")
  .Does(() =>
{
    var assemblyFile = string.Format("{0}/Properties/AssemblyInfo.cs", projectFolder);

    AssemblyInfoSettings assemblySettings = new AssemblyInfoSettings();
    assemblySettings.Title = projectName;
    assemblySettings.FileVersion = extensionsVersion;
    assemblySettings.Version = extensionsVersion;
    assemblySettings.InternalsVisibleTo = new [] { testProjectName };

    CreateAssemblyInfo(assemblyFile, assemblySettings);
});

Task("Build")
  .IsDependentOn("NugetRestore")
  .IsDependentOn("UpdateAssemblyVersion")
  .Does(() =>
{
    DotNetBuild(string.Format("{0}.sln", projectName), 
    settings => settings
        .SetConfiguration(buildConfiguration)
        .SetVerbosity(Verbosity.Minimal));
});

Task("Test")
  .IsDependentOn("Build")
  .Does(() =>
{
     var settings = new DotNetCoreTestSettings
     {
         Configuration = buildConfiguration
     };

     DotNetCoreTest(testProjectFile, settings);
});

Task("CodeCoverage")
  .IsDependentOn("Build")
  .Does(() =>
{
    var settings = new DotNetCoreTestSettings
    {
        Configuration = buildConfiguration
    };

    var coverageSettings = new OpenCoverSettings
    {
        // Workaround for the issue https://github.com/OpenCover/opencover/issues/601
        OldStyle = true
    };

	var coverageFileName = "./coverage.xml";
    OpenCover(tool => { tool.DotNetCoreTest(testProjectFile, settings); },
      new FilePath(coverageFileName),
      coverageSettings
        .WithFilter("+[Todoist.Net]*")
        .WithFilter("-[Todoist.Net.Tests]*"));

    Codecov(coverageFileName);
});

Task("CodeAnalysis")
  .IsDependentOn("Build")
  .Does(() =>
{
    StartProcess("cov-build", "--dir cov-int msbuild /t:Rebuild /v:q");

    var publishCoverity = Context.Tools.Resolve("PublishCoverity.exe");
    StartProcess(publishCoverity, "compress -o coverity.zip -i cov-int --overwrite --nologo");
    StartProcess(publishCoverity, 
        string.Format("publish -z coverity.zip -r olsh/todoist-net -t {1} -e olsh.me@gmail.com -d \"A Todoist API client for .NET written in C#\" --codeVersion \"{0}\" --nologo", 
        extensionsVersion, EnvironmentVariable("coverity:token")));
});

Task("NugetPack")
  .IsDependentOn("Build")
  .Does(() =>
{
     var settings = new DotNetCorePackSettings
     {
         Configuration = buildConfiguration,
         OutputDirectory = "."
     };

     DotNetCorePack(projectFolder, settings);
});

Task("CreateArtifact")
  .IsDependentOn("NugetPack")
  .WithCriteria(BuildSystem.AppVeyor.IsRunningOnAppVeyor)
  .Does(() =>
{
    BuildSystem.AppVeyor.UploadArtifact(string.Format("{0}.{1}.nupkg", projectName, extensionsVersion));
});

Task("Default")
    .IsDependentOn("Test")
    .IsDependentOn("NugetPack");

Task("CI")
    .IsDependentOn("UpdateBuildVersion")
    .IsDependentOn("CodeCoverage")
    .IsDependentOn("CodeAnalysis")
    .IsDependentOn("CreateArtifact");

RunTarget(target);
