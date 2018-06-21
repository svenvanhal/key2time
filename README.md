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

#### Add metadata


### Adding constraints


### Upgrading FET
The project currently bundles [FET 5.36.0](https://lalescu.ro/liviu/fet/news.html). To upgrade FET, execute the following steps:

  1. Download the latest FET release from the [download page](https://lalescu.ro/liviu/fet/download.html).
  1. Upgrade the FET binaries in `Implementation` project by replacing the relevant files in `lib/fet/`.
  1. Upgrade the FET binaries in `Timetabling.Tests` project by replacing the relevant files in `lib/fet/`.
  1. If the FET file structure has remained unchanged, the version number in `Timetabling.Algorithms.FET.FetInputGenerator` should be changed as well.
  
### 

## Remarks

### FET-CL
This project uses the command-line version of the open source timetable generator [FET](https://lalescu.ro/liviu/fet/). The Windows binary in bundled with the project.

### Testing
A separate `Timetabling.Testing` testing library is available containing unit tests. The tests can be run with NUnit 3.
