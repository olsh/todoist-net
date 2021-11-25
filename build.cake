#tool nuget:?package=Codecov&version=1.13.0
#addin nuget:?package=Cake.Codecov&version=1.0.1

#tool nuget:?package=MSBuild.SonarQube.Runner.Tool&version=4.8.0
#addin nuget:?package=Cake.Sonar&version=1.1.25

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
    var settings = new DotNetCoreBuildSettings
    {
        Configuration = buildConfiguration
    };

    DotNetCoreBuild(string.Format("{0}.sln", projectName), settings);
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
        Configuration = buildConfiguration,
        ArgumentCustomization = args => args
                                            .Append("/p:CollectCoverage=true")
                                            .Append("/p:CoverletOutputFormat=opencover")
                                            .Append("/p:Include=\"[Todoist.Net]*\"")
    };

    DotNetCoreTest(testProjectFile, settings);

    Codecov(string.Format("{0}coverage.netcoreapp3.1.opencover.xml", testProjectFolder), EnvironmentVariable("codecov:token"));
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
    .IsDependentOn("CodeCoverage")
    .IsDependentOn("CreateArtifact");

RunTarget(target);
