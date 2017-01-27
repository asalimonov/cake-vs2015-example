//////////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");
var project = Argument("Project", "Example1.sln");
var platform = Argument("Platform", "AnyCPU");
var solutionDir =  Argument("SolutionDir", "./");

//////////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////////


Task("Pre-Build")
    .Does(() =>
    {
        Information("Hi! It is pre-build script");
    });

Task("Post-Build")
    .Does(() =>
    {
        Information("Hi! It is post-build script");
    });

// TASK TARGETS
Task("Default");


// EXECUTION
RunTarget(target);