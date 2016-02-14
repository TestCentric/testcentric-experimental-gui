//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// SET PACKAGE VERSION
//////////////////////////////////////////////////////////////////////

var version = "0.1";
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

// Packages
var SRC_PACKAGE = PACKAGE_DIR + "NUnit-" + version + modifier + "-src.zip";
var ZIP_PACKAGE = PACKAGE_DIR + "NUnit-" + packageVersion + ".zip";

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
    NuGetRestore(GUI_SOLUTION);

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

		var zipFiles = GetFiles(BIN_DIR + "*.*");

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
	.IsDependentOn("Test");

Task("Travis")
	.IsDependentOn("Build");

Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
