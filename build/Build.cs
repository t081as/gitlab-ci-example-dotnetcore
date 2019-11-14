using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Pack);

    [Parameter("Configuration to build")]
    readonly Configuration Configuration = Configuration.Debug;

    [Solution] readonly Solution Solution;

    AbsolutePath SourceDirectory => RootDirectory / "src";

    AbsolutePath OutputDirectory => RootDirectory / "output";

    Target Clean => _ => _
        .Executes(() =>
        {
            DotNetClean();
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(OutputDirectory)
                .EnableNoRestore());
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Requires(() => Configuration == Configuration.Release)
        .Executes(() =>
        {
            RootDirectory.GlobFiles("*.zip").ForEach(DeleteFile);
            OutputDirectory.GlobFiles("*.dev.*").ForEach(DeleteFile);

            CopyFile(RootDirectory / "AUTHORS.txt", OutputDirectory / "AUTHORS.txt");
            CopyFile(RootDirectory / "CHANGELOG.md", OutputDirectory / "CHANGELOG.txt");

            CompressionTasks.CompressZip(OutputDirectory, RootDirectory / "DiabLaunch-VERSION-win32-anycpu.zip", null, System.IO.Compression.CompressionLevel.Optimal, System.IO.FileMode.CreateNew);
        });
}