using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;

using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile, x => x.UnitTest, x => x.NugetPack);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution(GenerateProjects = true)] readonly Solution Solution;

    Target Compile => _ => _
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration));
        });

    Target UnitTest => _ => _
        .DependsOn(Compile)
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
}
