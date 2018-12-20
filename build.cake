#tool nuget:?package=NUnit.ConsoleRunner&version=3.9.0
#tool nuget:?package=GitVersion.CommandLine

using System.Text.RegularExpressions;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// SET PACKAGE VERSION DEFAULTS
//////////////////////////////////////////////////////////////////////

//GitVersion GitVersionInfo { get; set; }
//BuildInfo Build { get; set;} = new BuildInfo();

string version = "0.6.0";
string modifier = "";
string dbgSuffix = configuration == "Debug" ? "-dbg" : "";

//////////////////////////////////////////////////////////////////////
// DETERMINE BUILD ENVIRONMENT
//////////////////////////////////////////////////////////////////////

string monoVersion = null;

Type type = Type.GetType("Mono.Runtime");
if (type != null)
{
    var displayName = type.GetMethod("GetDisplayName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
    if (displayName != null)
        monoVersion = displayName.Invoke(null, null).ToString();
}

bool isMonoButSupportsMsBuild = monoVersion!=null && System.Text.RegularExpressions.Regex.IsMatch(monoVersion,@"^([5-9]|\d{2,})\.\d+\.\d+(\.\d+)?");

var msBuildSettings = new MSBuildSettings {
    Verbosity = Verbosity.Minimal,
    ToolVersion = MSBuildToolVersion.Default,//The highest available MSBuild tool version//VS2017
    Configuration = configuration,
    PlatformTarget = PlatformTarget.MSIL,
    MSBuildPlatform = MSBuildPlatform.Automatic,
    DetailedSummary = true,
};

if(!IsRunningOnWindows() && isMonoButSupportsMsBuild)
{
    msBuildSettings.ToolPath = new FilePath(@"/usr/lib/mono/msbuild/15.0/bin/MSBuild.dll");//hack for Linux bug - missing MSBuild path
}

var xBuildSettings = new XBuildSettings {
    Verbosity = Verbosity.Minimal,
    ToolVersion = XBuildToolVersion.Default,//The highest available XBuild tool version//NET40
    Configuration = configuration,
};

//////////////////////////////////////////////////////////////////////
// DEFINE RUN CONSTANTS
//////////////////////////////////////////////////////////////////////

// HACK: Engine Version - Must update this manually to match package used
var ENGINE_VERSION = "3.9.0";

// Directories
var PROJECT_DIR = Context.Environment.WorkingDirectory.FullPath + "/";
var PACKAGE_DIR = PROJECT_DIR + "package/";
var CHOCO_DIR = PROJECT_DIR + "choco/";
var BIN_DIR = PROJECT_DIR + "bin/" + configuration + "/";

// Packaging
var PACKAGE_NAME = "testcentric-experimental-gui";
var PACKAGE_VERSION = version + modifier + dbgSuffix;

// Solution
var GUI_SOLUTION = PROJECT_DIR + "experimental-gui.sln";

// Test Assembly
var ALL_TESTS = BIN_DIR + "*.Tests.dll";

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
        Verbosity = NuGetVerbosity.Detailed
    });
});

//////////////////////////////////////////////////////////////////////
// SET BUILD INFO
//////////////////////////////////////////////////////////////////////
//Task("SetBuildInfo")
//    .Does(() =>
//{
//    var settings = new GitVersionSettings();
//    if (!BuildSystem.IsLocalBuild)
//    {
//        settings.UpdateAssemblyInfo = true;
//        settings.UpdateAssemblyInfoFilePath = "src/CommonAssemblyInfo.cs";
//    }
//
//    GitVersionInfo = GitVersion(settings);
//    Build = new BuildInfo(GitVersionInfo);
//});

//////////////////////////////////////////////////////////////////////
// BUILD
//////////////////////////////////////////////////////////////////////

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("RestorePackages")
    //.IsDependentOn("SetBuildInfo")
    .Does(() =>
    {
        if(IsRunningOnWindows() || isMonoButSupportsMsBuild)
        {
            MSBuild(GUI_SOLUTION, msBuildSettings);
        }
        else
        {
            XBuild(GUI_SOLUTION, xBuildSettings);
        }

        CopyFileToDirectory("packages/NUnit.Engine." + ENGINE_VERSION + "/lib/nunit-agent.exe.config", BIN_DIR);
        CopyFileToDirectory("packages/NUnit.Engine." + ENGINE_VERSION + "/lib/nunit-agent-x86.exe.config", BIN_DIR);
    });

//////////////////////////////////////////////////////////////////////
// TEST
//////////////////////////////////////////////////////////////////////

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        NUnit3(ALL_TESTS);
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
            BIN_DIR + "tc-next.exe",
            BIN_DIR + "tc-next.exe.config",
            BIN_DIR + "TestCentric.Gui.Runner.dll",
            BIN_DIR + "TestCentric.Gui.Components.dll",
            BIN_DIR + "TestCentric.Gui.Model.dll",
            BIN_DIR + "nunit.engine.api.dll",
            BIN_DIR + "nunit.engine.dll",
            BIN_DIR + "Mono.Cecil.dll",
            BIN_DIR + "nunit-agent.exe",
            BIN_DIR + "nunit-agent.exe.config",
            BIN_DIR + "nunit-agent-x86.exe",
            BIN_DIR + "nunit-agent-x86.exe.config"
        };

        //Zip(BIN_DIR, File(PACKAGE_DIR + PACKAGE_NAME + "-" + Build.PackageVersion + ".zip"), zipFiles);
        Zip(BIN_DIR, File(PACKAGE_DIR + PACKAGE_NAME +"-" + PACKAGE_VERSION + ".zip"), zipFiles);
    });

Task("PackageChocolatey")
    .IsDependentOn("Build")
    .Does(() =>
    {
        CreateDirectory(PACKAGE_DIR);

        ChocolateyPack(CHOCO_DIR + PACKAGE_NAME + ".nuspec", 
            new ChocolateyPackSettings()
            {
                //Version = Build.PackageVersion,
                Version = PACKAGE_VERSION,
                OutputDirectory = PACKAGE_DIR,
                Files = new ChocolateyNuSpecContent[]
                {
                    new ChocolateyNuSpecContent() { Source = PROJECT_DIR + "LICENSE" },
                    new ChocolateyNuSpecContent() { Source = PROJECT_DIR + "CHANGES.txt" },
					new ChocolateyNuSpecContent() { Source = CHOCO_DIR + "VERIFICATION.txt" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "tc-next.exe", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "tc-next.exe.config", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "TestCentric.Gui.Runner.dll", Target="tools"},
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "TestCentric.Gui.Components.dll", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "TestCentric.Gui.Model.dll", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit.engine.dll", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit.engine.api.dll", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "Mono.Cecil.dll", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit-agent.exe", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit-agent.exe.config", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = CHOCO_DIR + "nunit-agent.exe.ignore", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit-agent-x86.exe", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = BIN_DIR + "nunit-agent-x86.exe.config", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = CHOCO_DIR + "nunit-agent-x86.exe.ignore", Target="tools" },
                    new ChocolateyNuSpecContent() { Source = CHOCO_DIR + "nunit.choco.addins", Target="tools" }
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
    .IsDependentOn("Test")
    .IsDependentOn("PackageZip");

Task("All")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Package");

Task("Default")
    .IsDependentOn("Test");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
