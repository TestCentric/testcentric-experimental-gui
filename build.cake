#tool nuget:?package=NUnit.ConsoleRunner&version=3.5.0

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// SET PACKAGE VERSION
//////////////////////////////////////////////////////////////////////

var version = "0.3";
var modifier = "";

var isAppveyor = BuildSystem.IsRunningOnAppVeyor;
var dbgSuffix = configuration == "Debug" ? "-dbg" : "";
var packageVersion = version + modifier + dbgSuffix;

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
	"https://www.myget.org/F/nunit-gui-team/api/v3/index.json",
	"https://www.myget.org/F/nunit-gui-team/api/v2"
};

// Packages
var SRC_PACKAGE = PACKAGE_DIR + "NUnit-Gui-" + version + modifier + "-src.zip";
var ZIP_PACKAGE = PACKAGE_DIR + "NUnit-Gui-" + packageVersion + ".zip";

//////////////////////////////////////////////////////////////////////
// CLEAN
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(BIN_DIR);
});


//////////////////////////////////////////////////////////////////////
// INITIALIZE FOR BUILD
//////////////////////////////////////////////////////////////////////

Task("InitializeBuild")
    .Does(() =>
{
    NuGetRestore(GUI_SOLUTION, new NuGetRestoreSettings
	{
		Source = PACKAGE_SOURCE,
		Verbosity = NuGetVerbosity.Detailed
	});

	if (BuildSystem.IsRunningOnAppVeyor)
	{
		var tag = AppVeyor.Environment.Repository.Tag;

		if (tag.IsTag)
		{
			packageVersion = tag.Name;
		}
		else
		{
			var buildNumber = AppVeyor.Environment.Build.Number;
			packageVersion = version + "-CI-" + buildNumber + dbgSuffix;
			if (AppVeyor.Environment.PullRequest.IsPullRequest)
				packageVersion += "-PR-" + AppVeyor.Environment.PullRequest.Number;
			else
				packageVersion += "-" + AppVeyor.Environment.Repository.Branch;
		}

		AppVeyor.UpdateBuildVersion(packageVersion);
	}
});

//////////////////////////////////////////////////////////////////////
// BUILD
//////////////////////////////////////////////////////////////////////

Task("Build")
    .IsDependentOn("InitializeBuild")
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

Task("PackageSource")
  .Does(() =>
	{
		CreateDirectory(PACKAGE_DIR);
		RunGitCommand(string.Format("archive -o {0} HEAD", SRC_PACKAGE));
	});

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
			BIN_DIR + "nunit-gui.pdb",
			BIN_DIR + "nunit.uikit.dll",
			BIN_DIR + "nunit.uikit.pdb",
			BIN_DIR + "nunit.engine.api.dll",
			BIN_DIR + "nunit.engine.dll",
			BIN_DIR + "nunit.engine.addins",
			BIN_DIR + "Mono.Cecil.dll"
		};

		Zip(BIN_DIR, File(ZIP_PACKAGE), zipFiles);
	});

//////////////////////////////////////////////////////////////////////
// HELPER METHODS
//////////////////////////////////////////////////////////////////////

void RunGitCommand(string arguments)
{
	StartProcess("git", new ProcessSettings()
	{
		Arguments = arguments
	});
}

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Rebuild")
    .IsDependentOn("Clean")
	.IsDependentOn("Build");

Task("Package")
	.IsDependentOn("PackageSource")
	.IsDependentOn("PackageZip");

Task("Appveyor")
	.IsDependentOn("Build")
	.IsDependentOn("Test")
	.IsDependentOn("Package");

Task("Travis")
	.IsDependentOn("Build");

Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
