# Example-1: Using Cake for Pre and Post build events

How to use Cake scripts for Pre and Post build events.

* Run Powershell (powershell.exe) with administrator access rights and set execution policy "Unrestricted", execute the following command: `Set-ExecutionPolicy Unrestricted`
* In solution's folder download build.ps1 script: `Invoke-WebRequest http://cakebuild.net/download/bootstrapper/windows -OutFile build.ps1`
* Install Cake.Core package from NuGet
* Open project file (*.csproj) in Visual Studio or in a text editor and add before `</Project>` tag the following code:
```
   <Target Name="BeforeBuild">
    <Exec Command="powershell.exe -File $(SolutionDir)build.ps1 -Script $(ProjectDir)build.cake -Target Pre-Build -Configuration $(Configuration) -ScriptArgs &quot;'-Project=$(ProjectPath)'  '-SolutionDir=$(SolutionDir)' '-Platform=$(Platform)'&quot;" WorkingDirectory=".\" />
  </Target>
  <Target Name="AfterBuild">
    <Exec Command="powershell.exe -File $(SolutionDir)build.ps1 -Script $(ProjectDir)build.cake -Target Post-Build -Configuration $(Configuration) -ScriptArgs &quot;'-Project=$(ProjectPath)'  '-SolutionDir=$(SolutionDir)' '-Platform=$(Platform)'&quot;" WorkingDirectory=".\" />
  </Target>
```
* Create `build.cake` file in project's dir and write the following code:
```
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
```
* Rebuil solution. You can see in Output console additional messages about Pre-\Post-Build tasks and information how much time they took.

# Short explanation
Visual Studio uses [MSBuilld](https://msdn.microsoft.com/library/dd393574.aspx) to build a solution and projects which are in it and we added two targets which should be executed when MSBuild tries to build the project. 
In this example we added targets which run PowerShell scripts, let's consider Pre-Build task: `powershell.exe -File $(SolutionDir)build.ps1 -Script $(ProjectDir)build.cake -Target Pre-Build -Configuration $(Configuration) -ScriptArgs &quot;'-Project=$(ProjectPath)'  '-SolutionDir=$(SolutionDir)' '-Platform=$(Platform)'&quot;`

* `-File $(SolutionDir)build.ps1` - run build.ps1 in solution's directory with parameters which will be below
* `-Script $(ProjectDir)build.cake` - run Cake script in project's directory
* `-Target Pre-Build`- Name of a task in build.cake
* `-Configuration $(Configuration)` - Configuration of build (Debug or Release)
* `-ScriptArgs &quot;'-Project=$(ProjectPath)'  '-SolutionDir=$(SolutionDir)' '-Platform=$(Platform)'&quot;` - additional arguments for our build.cake script. You can add your own arguments and get its in the script, [more details](http://cakebuild.net/docs/fundamentals/args-and-environment-vars).
  * `-Project=$(ProjectPath)` - path to the project
  * `-SolutionDir=$(SolutionDir)` - path to the solution
  * `-Platform=$(Platform)` - Target paltform (AnyCPU, x86 or x64)

We have to use `&quot;` insted of quote symbol because XML doesn't allow to use it directly.