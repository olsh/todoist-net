#tool "nuget:?package=OpenCover"
#tool "nuget:?package=Codecov"

#addin "nuget:?package=Cake.Codecov"

var target = Argument("target", "Default");

var buildConfiguration = "Release";
var projectName = "Todoist.Net";
var testProjectName = "Todoist.Net.Tests";
var projectFolder = string.Format("./src/{0}/", projectName);
var projectFile = string.Format("./src/{0}/{0}.csproj", projectName);
var testProjectFile = string.Format("./src/{0}/{0}.csproj", testProjectName);

var extensionsVersion = XmlPeek(projectFile, "Project/PropertyGroup[1]/VersionPrefix/text()");

Task("UpdateBuildVersion")
  .WithCriteria(BuildSystem.AppVeyor.IsRunningOnAppVeyor)
  .Does(() =>
{
    var buildNumber = BuildSystem.AppVeyor.Environment.Build.Number;

    BuildSystem.AppVeyor.UpdateBuildVersion(string.Format("{0}.{1}", extensionsVersion, buildNumber));
});

Task("Build")
  .Does(() =>
{
    MSBuild(string.Format("{0}.sln", projectName), 
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

    Codecov(coverageFileName, EnvironmentVariable("codecov:token"));
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
    .IsDependentOn("CreateArtifact");

RunTarget(target);
