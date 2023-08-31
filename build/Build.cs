using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.AppVeyor;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Git;
using Nuke.Common.Tools.SonarScanner;

using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.SonarScanner.SonarScannerTasks;

class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile, x => x.UnitTest, x => x.NugetPack);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("SonarQube API key", Name = "sonar:apikey")] readonly string SonarQubeApiKey;

    [Solution(GenerateProjects = true)] readonly Solution Solution;

    [CI] readonly AppVeyor AppVeyor;

    Target UpdateBuildVersion => _ => _
        .Requires(() => AppVeyor)
        .Before(Compile)
        .Executes(() =>
        {
            AppVeyor.Instance.UpdateBuildVersion($"{Solution.src.Todoist_Net.GetProperty("Version")}.{AppVeyor.BuildNumber}");
        });

    Target Compile => _ => _
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration));
        });

    Target UnitTest => _ => _
        .DependsOn(Compile)
        .Before(Sonar)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution.src.Todoist_Net_Tests)
                .SetConfiguration(Configuration)
                .SetFilter("trait=unit")
                .SetNoBuild(true));
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution.src.Todoist_Net_Tests)
                .SetConfiguration(Configuration)
                .SetLoggers("console;verbosity=detailed")
                .SetFilter("trait!=mfa-required")
                .SetNoBuild(true));
        });

    Target NugetPack => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetProject(Solution.src.Todoist_Net)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(RootDirectory / "artifacts")
                .SetNoBuild(true)
                .SetNoRestore(true));
        });

    Target SonarBegin => _ => _
        .Unlisted()
        .Before(Compile)
        .Executes(() =>
        {
            SonarScannerBegin(s =>
            {
                s = s
                    .SetServer("https://sonarcloud.io")
                    .SetFramework("net5.0")
                    .SetLogin(SonarQubeApiKey)
                    .SetProjectKey("todoist-net")
                    .SetName("Todoist.Net")
                    .SetOrganization("olsh")
                    .SetVersion("1.0.0.0");

                if (AppVeyor != null)
                {
                    if (AppVeyor.PullRequestNumber != null)
                    {
                        s = s
                            .SetPullRequestKey(AppVeyor.PullRequestNumber.ToString())
                            .SetPullRequestBase(AppVeyor.RepositoryBranch)
                            .SetPullRequestBranch(AppVeyor.PullRequestHeadRepositoryBranch);
                    }
                    else
                    {
                        s = s
                            .SetBranchName(AppVeyor.RepositoryBranch);
                    }
                }

                return s;
            });
        });

    Target Sonar => _ => _
        .DependsOn(SonarBegin, Compile)
        .Executes(() =>
        {
            SonarScannerEnd(s => s
                .SetLogin(SonarQubeApiKey)
                .SetFramework("net5.0"));
        });
}
