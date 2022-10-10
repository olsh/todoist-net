#tool nuget:?package=MSBuild.SonarQube.Runner.Tool&version=4.8.0
#addin nuget:?package=Cake.Sonar&version=1.1.30

var target = Argument("target", "Default");

var buildConfiguration = "Release";
var projectName = "Todoist.Net";
var testProjectName = "Todoist.Net.Tests";
var projectFolder = string.Format("./src/{0}/", projectName);
var testProjectFolder = string.Format("./src/{0}/", testProjectName);
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
    var settings = new DotNetBuildSettings
    {
        Configuration = buildConfiguration
    };

    DotNetBuild(string.Format("{0}.sln", projectName), settings);
});

Task("UnitTest")
  .IsDependentOn("Build")
  .Does(() =>
{
     var settings = new DotNetTestSettings
     {
         Configuration = buildConfiguration,
         Filter = "trait=unit"
     };

     DotNetTest(testProjectFile, settings);
});

Task("Test")
  .IsDependentOn("Build")
  .Does(() =>
{
     var settings = new DotNetTestSettings
     {
         Configuration = buildConfiguration,
         Loggers = new List<string> { "console;verbosity=detailed" }
     };

     DotNetTest(testProjectFile, settings);
});

Task("NugetPack")
  .IsDependentOn("Build")
  .Does(() =>
{
     var settings = new DotNetPackSettings
     {
         Configuration = buildConfiguration,
         OutputDirectory = "."
     };

     DotNetPack(projectFolder, settings);
});

Task("CreateArtifact")
  .IsDependentOn("NugetPack")
  .WithCriteria(BuildSystem.AppVeyor.IsRunningOnAppVeyor)
  .Does(() =>
{
    BuildSystem.AppVeyor.UploadArtifact(string.Format("{0}.{1}.nupkg", projectName, extensionsVersion));
});

Task("SonarBegin")
  .Does(() => {
     SonarBegin(new SonarBeginSettings {
        Url = "https://sonarcloud.io",
        Login = EnvironmentVariable("sonar:apikey"),
        Key = "todoist-net",
        Name = "Todoist.Net",
        ArgumentCustomization = args => args
            .Append($"/o:olsh-github"),
        Version = "1.0.0.0"
     });
  });

Task("SonarEnd")
  .Does(() => {
     SonarEnd(new SonarEndSettings {
        Login = EnvironmentVariable("sonar:apikey")
     });
  });

Task("Sonar")
  .IsDependentOn("SonarBegin")
  .IsDependentOn("Build")
  .IsDependentOn("SonarEnd");

Task("Default")
    .IsDependentOn("Test")
    .IsDependentOn("NugetPack");

Task("CI")
    .IsDependentOn("UpdateBuildVersion")
    .IsDependentOn("Sonar")
    .IsDependentOn("UnitTest")
    .IsDependentOn("CreateArtifact");

RunTarget(target);
