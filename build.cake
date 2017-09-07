#tool nuget:?package=NUnit.ConsoleRunner&version=3.6.1
#tool nuget:?package=GitVersion.CommandLine
#addin "Cake.Incubator"

using System.Text.RegularExpressions;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// SET PACKAGE VERSION DEFAULTS
//////////////////////////////////////////////////////////////////////

GitVersion GitVersionInfo { get; set; }
BuildInfo Build { get; set;}

//////////////////////////////////////////////////////////////////////
// DEFINE RUN CONSTANTS
//////////////////////////////////////////////////////////////////////

// Directories
var PROJECT_DIR = Context.Environment.WorkingDirectory.FullPath + "/";
var PACKAGE_DIR = PROJECT_DIR + "package/";
var BIN_DIR = PROJECT_DIR + "bin/" + configuration + "/";

// Solution
var GUI_SOLUTION = PROJECT_DIR + "nunit-gui.sln";

// Test Assembly
var GUI_TESTS = BIN_DIR + "nunit-gui.tests.dll";

// Package sources for nuget restore
var PACKAGE_SOURCE = new string[]
{
    "https://www.nuget.org/api/v2",
    "https://www.myget.org/F/nunit-gui-team/api/v3/index.json",
    "https://www.myget.org/F/nunit-gui-team/api/v2"
};

//////////////////////////////////////////////////////////////////////
// CLEAN
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(BIN_DIR);
});


//////////////////////////////////////////////////////////////////////
// RESTORE NUGET PACKAGES
//////////////////////////////////////////////////////////////////////

Task("RestorePackages")
    .Does(() =>
{
    NuGetRestore(GUI_SOLUTION, new NuGetRestoreSettings
    {
        Source = PACKAGE_SOURCE,
        Verbosity = NuGetVerbosity.Detailed
    });
});

//////////////////////////////////////////////////////////////////////
// SET BUILD INFO
//////////////////////////////////////////////////////////////////////
Task("SetBuildInfo")
    .Does(() =>
{
    var settings = new GitVersionSettings();
    if (!BuildSystem.IsLocalBuild)
    {
        settings.UpdateAssemblyInfo = true;
        settings.UpdateAssemblyInfoFilePath = "src/CommonAssemblyInfo.cs";
    }

    GitVersionInfo = GitVersion(settings);
    Build = new BuildInfo(GitVersionInfo);

// Debug info for testing GitVersion
    Information("GitVersion Info:\n" + GitVersionInfo.Dump());
    Information("\nBuildInfo:\n" + Build.Dump());

    if (BuildSystem.IsRunningOnAppVeyor)
    {
        string version = "0.6.0";
        string dbgSuffix = configuration == "Debug" ? "-dbg" : "";
        string packageVersion = version + dbgSuffix;
        var tag = AppVeyor.Environment.Repository.Tag;

        if (tag.IsTag)
        {
            packageVersion = tag.Name;
        }
        else
        {
            var buildNumber = AppVeyor.Environment.Build.Number.ToString("00000");
            var branch = AppVeyor.Environment.Repository.Branch;
            var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;

            if (branch == "master" && !isPullRequest)
            {
                packageVersion = version + "-dev-" + buildNumber + dbgSuffix;
            }
            else
            {
                var suffix = "-ci-" + buildNumber + dbgSuffix;

                if (isPullRequest)
                    suffix += "-pr-" + AppVeyor.Environment.PullRequest.Number;
                else
                    suffix += "-" + branch;

                // Nuget limits "special version part" to 20 chars. Add one for the hyphen.
                if (suffix.Length > 21)
                    suffix = suffix.Substring(0, 21);

                packageVersion = version + suffix;
            }
        }

        Information("\nOld PackageVersion would be " + packageVersion + " on AppVeyor\n");
    }
// End of debug info
});

//////////////////////////////////////////////////////////////////////
// BUILD
//////////////////////////////////////////////////////////////////////

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("RestorePackages")
    .IsDependentOn("SetBuildInfo")
    .Does(() =>
    {
        if(IsRunningOnWindows())
        {
            // Use MSBuild
            MSBuild(GUI_SOLUTION, new MSBuildSettings()
                .SetConfiguration(configuration)
                .SetVerbosity(Verbosity.Minimal)
                .SetNodeReuse(false)
                .SetPlatformTarget(PlatformTarget.MSIL)
            );
        }
        else
        {
            // Use XBuild
            XBuild(GUI_SOLUTION, new XBuildSettings()
                .WithTarget("Build")
                .WithProperty("Configuration", configuration)
                .SetVerbosity(Verbosity.Minimal)
            );
        }
    });

//////////////////////////////////////////////////////////////////////
// TEST
//////////////////////////////////////////////////////////////////////

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        NUnit3(GUI_TESTS);
    });

//////////////////////////////////////////////////////////////////////
// PACKAGE
//////////////////////////////////////////////////////////////////////

Task("PackageZip")
    .IsDependentOn("Build")
    .Does(() =>
    {
        CreateDirectory(PACKAGE_DIR);

        CopyFileToDirectory("LICENSE", BIN_DIR);
        CopyFileToDirectory("CHANGES.txt", BIN_DIR);

        var zipFiles = new FilePath[]
        {
            BIN_DIR + "LICENSE",
            BIN_DIR + "CHANGES.txt",
            BIN_DIR + "nunit-gui.exe",
            BIN_DIR + "nunit-gui.exe.config",
            BIN_DIR + "nunit.uikit.dll",
            BIN_DIR + "nunit.engine.api.dll",
            BIN_DIR + "nunit.engine.dll",
            BIN_DIR + "Mono.Cecil.dll",
            BIN_DIR + "nunit-agent.exe",
            BIN_DIR + "nunit-agent.exe.config",
            BIN_DIR + "nunit-agent-x86.exe",
            BIN_DIR + "nunit-agent-x86.exe.config"
        };

        Zip(BIN_DIR, File(PACKAGE_DIR + "NUnit-Gui-" + Build.PackageVersion + ".zip"), zipFiles);
    });

Task("PackageChocolatey")
    .IsDependentOn("Build")
    .Does(() =>
    {
        CreateDirectory(PACKAGE_DIR);

        ChocolateyPack("choco/nunit-gui.nuspec", 
            new ChocolateyPackSettings()
            {
                Version = Build.PackageVersion,
                OutputDirectory = PACKAGE_DIR,
                Files = new ChocolateyNuSpecContent[]
                {
                    new ChocolateyNuSpecContent() { Source = "../LICENSE" },
                    new ChocolateyNuSpecContent() { Source = "../CHANGES.txt" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit-gui.exe", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit-gui.exe.config", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit.uikit.dll", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit.engine.dll", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit.engine.api.dll", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "Mono.Cecil.dll", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit-agent.exe", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit-agent.exe.config", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = "nunit-agent.exe.ignore", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit-agent-x86.exe", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit-agent-x86.exe.config", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = "nunit-agent-x86.exe.ignore", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = "nunit.choco.addins", Target="tools" }
                }
            });
    });

//////////////////////////////////////////////////////////////////////
// BUILD INFO
//////////////////////////////////////////////////////////////////////

class BuildInfo
{
    public BuildInfo(GitVersion gitVersion)
    {
        Version = gitVersion.MajorMinorPatch;
        BranchName = gitVersion.BranchName;
        BuildNumber = gitVersion.CommitsSinceVersionSourcePadded;

        // Initially assume it's neither master nor a PR
        IsMaster = false;
        IsPullRequest = false;
        PullRequestNumber = string.Empty;

        if (BranchName == "master")
        {
            IsMaster = true;
            PreReleaseSuffix = "dev-" + BuildNumber;
        }
        else
        {
            var re = new Regex(@"(pull|pull\-requests?|pr)[/-](\d*)[/-]");
            var match = re.Match(BranchName);

            if (match.Success)
            {
                IsPullRequest = true;
                PullRequestNumber = match.Groups[2].Value;
                PreReleaseSuffix = "pr-" + PullRequestNumber + "-" + BuildNumber;
            }
            else
            {
                PreReleaseSuffix = "ci-" + BuildNumber + "-" + Regex.Replace(BranchName, "[^0-9A-Za-z-]+", "-");
                // Nuget limits "special version part" to 20 chars.
                if (PreReleaseSuffix.Length > 20)
                    PreReleaseSuffix = PreReleaseSuffix.Substring(0, 20);
            }
        }

        PackageVersion = Version + "-" + PreReleaseSuffix;

        AssemblyVersion = gitVersion.AssemblySemVer;
        AssemblyFileVersion = PackageVersion;
    }

    public string BranchName { get; private set; }
    public string Version { get; private set; }
    public bool IsMaster { get; private set; }
    public bool IsPullRequest { get; private set; }
    public string PullRequestNumber { get; private set; }
    public string BuildNumber { get; private set; }
    public string PreReleaseSuffix { get; private set; }
    public string PackageVersion { get; private set; }

    public string AssemblyVersion { get; private set; }
    public string AssemblyFileVersion { get; private set; }

    public string Dump()
    {
        var NL = Environment.NewLine;
        return "           BranchName: " + BranchName + NL +
               "              Version: " + Version + NL +
               "     PreReleaseSuffix: " + PreReleaseSuffix + NL +
               "        IsPullRequest: " + IsPullRequest.ToString() + NL +
               "    PullRequestNumber: " + PullRequestNumber + NL +
               "      AssemblyVersion: " + AssemblyVersion + NL +
               "  AssemblyFileVersion: " + AssemblyFileVersion + NL +
               "      Package Version: " + PackageVersion + NL;
    }
}

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Package")
    .IsDependentOn("PackageZip")
    .IsDependentOn("PackageChocolatey");

Task("Appveyor")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Package");

Task("Travis")
    .IsDependentOn("Build")
    .IsDependentOn("PackageZip");

Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
