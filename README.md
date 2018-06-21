# TI3806 - Bachelor End Project
[![Build status](https://ci.appveyor.com/api/projects/status/gls52n579c7rkar6/branch/master?svg=true)](https://ci.appveyor.com/project/svenvanhal/bachelorproject/branch/master) [![Coverage Status](https://coveralls.io/repos/github/svenvanhal/bachelorproject/badge.svg?branch=master)](https://coveralls.io/github/svenvanhal/bachelorproject?branch=master)

*Karim Osman, Sven van Hal*

Automated timetable generator.

## Building the project
Prerequisites: a MS SQL database with a compatible schema.

 1. Clone this repository
 1. Rename `Implementation/connection.config.example` to `Implementation/connection.config` and configure the database connection string.
 1. Open `BEP.sln` in Visual Studio.
 1. Right click on the `Implementation/connection.config` and choose `Properties`. Change `"Copy to Output Directory"` to `"Copy always"`.
 1. Build the solution and run the `Implementation` project.

## Usage

### Creating timetables
To generate timetables, include the `Timetabling` project in your solution. To fully utilize this library, the following four classes are relevant:

 * `Timetabling.TimetableGenerator`
 * `Timetabling.DB.DataModel`
 * A `Timetabling.Algorithms.TimetablingStrategy` subclass, e.g. `Timetabling.Algorithms.FetAlgorithm`
 * `Timetabling.Helper.DatabaseHelper` (optional, to save the generated timetable)
 
The timetabling algorithm runs asynchronous and is wrapped in a `Task<Timetable>` ([docs](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap)). To create the task, execute the `TimetableGenerator.RunAlgorithm(strategy, model)` method with a `TimetablingStrategy` and a `DataModel`:

```
using (var model = new DataModel(StageId))
using (var generator = new TimetableGenerator())
{
    Task<Timetable> task = generator.RunAlgorithm(new FetAlgorithm(), model);
}
```

The algorithm has now started running in the background. By attaching task continuation handlers, the task output can be processed:

```
task.ContinueWith(OnSuccess, TaskContinuationOptions.OnlyOnRanToCompletion);
task.ContinueWith(OnCanceled, TaskContinuationOptions.OnlyOnCanceled);
task.ContinueWith(OnError, TaskContinuationOptions.OnlyOnFaulted);
```

Use `DatabaseHelper` to save the timetable to the database. The `OnSuccess` handler could be used for this purpose:

```
public static void OnSuccess(Task<Timetable> task)
{

    using (var db = new DatabaseHelper())
    {
        Timetable tt = task.Result;
        db.SaveTimetable(tt);
    }

}
```

By default, the `DatabaseHelper` operates on a new database connection. A `DataModel` can be passed to its constructor to override that behavior.

#### Add metadata
Currently, not all required meta data about the timetable (e.g. `AcademicYearId`, `SectionId` and `QuarterId`) is retrieved from the database. This data can therefore optionally be set (and consequently saved to the database) by altering the `OnSuccess` handler:

```
public static void OnSuccess(Task<Timetable> task)
{

    Timetable tt = task.Result;

    // Set meta data
    tt.AcademicYearId = 1;
    tt.QuarterId = 2;
    tt.SectionId = 3;

    // Save to database here

}
```

#### Working example
An example implementation (aptly named `Implementation`) project is included in the solution.

### Adding constraints
The FET algorithm is configured by an XML file containing all resources, activities to schedule and constraints. To add a new constraint, perform the following steps:

 1. Add a constraint class which extends `AbstractConstraint` in `Timetabling.Objects.Constraints`. Please refer to existing FET input files and the documentation at [this page](http://timetabling.de/manual/FET-manual.en.html) for the correct XML constraint output.
 2. Update `Timetabling.Algorithms.FET.FetInputGenerator` to include your new constraint.
 3. Add tests in `Timetabling.Tests.Objects.Constraints` for the new constraint.

N.B.: the FET constraint system is rather complicated and its behaviour might not be intuitive when combining constraints. Make sure to test a new constraint thoroughly before using it in production.

### Upgrading FET
The project currently bundles [FET 5.36.0](https://lalescu.ro/liviu/fet/news.html). To upgrade FET, execute the following steps:

  1. Download the latest FET release from the [download page](https://lalescu.ro/liviu/fet/download.html).
  1. Upgrade the FET binaries in `Implementation` project by replacing the relevant files in `lib/fet/`.
  1. Upgrade the FET binaries in `Timetabling.Tests` project by replacing the relevant files in `lib/fet/`.
  1. If the FET file structure has remained unchanged, the version number in `Timetabling.Algorithms.FET.FetInputGenerator` should be changed as well.

## Remarks

### FET-CL
This project uses the command-line version of the open source timetable generator [FET](https://lalescu.ro/liviu/fet/). The Windows binary in bundled with the project.

### Testing
A separate `Timetabling.Testing` testing library is available containing unit tests. The tests can be run with NUnit 3.
