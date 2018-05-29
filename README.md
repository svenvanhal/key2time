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

## Remarks

### FET-CL
This project uses the command-line version of the open source timetable generator [FET](https://lalescu.ro/liviu/fet/). Binaries for Windows and Unix-based systems are bundled and the correct binary is copied by a Post-Build action.

### Testing
A separate `Timetabling.Testing` testing library is available containing unit tests. The tests can be run with NUnit 3.
