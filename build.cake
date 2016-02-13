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
var displayVersion = "0.1";

var isAppveyor = BuildSystem.IsRunningOnAppVeyor;
var dbgSuffix = configuration == "Debug" ? "-dbg" : "";
var packageVersion = version + modifier + dbgSuffix;

//////////////////////////////////////////////////////////////////////
// DEFINE RUN CONSTANTS
//////////////////////////////////////////////////////////////////////

var PROJECT_DIR = Context.Environment.WorkingDirectory.FullPath + "/";
var PACKAGE_DIR = PROJECT_DIR + "package/";
var BIN_DIR = PROJECT_DIR + "bin/" + configuration + "/";
var IMAGE_DIR = PROJECT_DIR + "images/";

// Test Runners
var NUNIT3_CONSOLE = BIN_DIR + "nunit3-console.exe";
var NUNITLITE_RUNNER = "nunitlite-runner.exe";

// Test Assemblies
var FRAMEWORK_TESTS = "nunit.framework.tests.dll";
var NUNITLITE_TESTS = "nunitlite.tests.exe";
var ENGINE_TESTS = "nunit.engine.tests.dll";
var PORTABLE_AGENT_TESTS = "agents/nunit.portable.agent.tests.dll";
var ADDIN_TESTS = "addins/tests/addin-tests.dll";
var V2_PORTABLE_AGENT_TESTS = "addins/v2-tests/nunit.v2.driver.tests.dll";
var CONSOLE_TESTS = "nunit3-console.tests.dll";

// Packages
var SRC_PACKAGE = PACKAGE_DIR + "NUnit-" + version + modifier + "-src.zip";
var ZIP_PACKAGE = PACKAGE_DIR + "NUnit-" + packageVersion + ".zip";
var ZIP_PACKAGE_SL = PACKAGE_DIR + "NUnitSL-" + packageVersion + ".zip";
var ZIP_PACKAGE_CF = PACKAGE_DIR + "NUnitCF-" + packageVersion + ".zip";

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
    NuGetRestore("./nunit-gui.sln");

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
			MSBuild("nunit-gui.sln", new MSBuildSettings()
				.SetConfiguration(configuration)
				//.SetMSBuildPlatform(buildPlatform)
				.SetVerbosity(Verbosity.Minimal)
				.SetNodeReuse(false)
			);
		}
		else
		{
			// Use XBuild
			XBuild("nunit-gui.sln", new XBuildSettings()
				.WithTarget("Build")
				.WithProperty("Configuration", configuration)
				.SetVerbosity(Verbosity.Minimal)
			);
		}
    });

//////////////////////////////////////////////////////////////////////
// TEST
//////////////////////////////////////////////////////////////////////

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

Task("Appveyor")
	.IsDependentOn("Build");

Task("Travis")
	.IsDependentOn("Build");

Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
