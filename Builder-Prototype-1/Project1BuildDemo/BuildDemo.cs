//////////////////////////////////////////////////////////////////////////////
// BuildDemo.cs - builds the Project files provided and displays the logs   //
//--------------------------------------------------------------------------//
// Author: Amritbani Sondhi, asondhi@syr.edu                                //
// Application: CSE681-Software Modeling and Analysis Demo                  //
// Environment: C# console                                                  //
//////////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * ===================
 * Builds the project files ie. .csproj files provided
 * displays the logs and the results of the Build Process
 * 
 * Required Files:
 * ---------------
 * DemoApp.csproj
 * stored at \bin\Debug\DemoApp\DemoApp
 * 
 * Maintenance History:
 * --------------------
 * ver 1.0 : Sept 13, 2017
 * - first release
 * 
 */


using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Build.Evaluation;
using Microsoft.Build.Logging;
using Microsoft.Build.Framework;

namespace Project1BuildDemo
{
    class BuildDemo
    {
        public static void CompileProjectFile()
        {
            Console.WriteLine("\n ====================================================================================");
            Console.WriteLine("Sample Demo App is kept in folder:");
            Console.WriteLine(" \n \\Project1BuildDemo\\Project1BuildDemo\\bin\\Debug\\DemoApp\\DemoApp");
            Console.WriteLine("====================================================================================");

            // creates the complete absolute path of the project file
            string currentFullDir = Directory.GetCurrentDirectory();
            string sampleApp = "\\DemoApp\\DemoApp\\DemoApp.csproj";
            string sampleAppDir = Path.GetDirectoryName(sampleApp);
            string mainProjDir = Directory.GetCurrentDirectory();
            string completeDirPath = mainProjDir + "\\DemoApp\\DemoApp\\";
            Directory.SetCurrentDirectory(completeDirPath);
            string projectFilePath = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(sampleApp));

            // creates a list of loggers implementing the ILogger interface
            List <ILogger> loggers = new List<ILogger>();   // The other way is to create a custom Logger
                                                            // which inherits from the Logger Class
                                                            // used this, cause it fulfills the prototype requirements
            ConsoleLogger clogs = new ConsoleLogger();      // to print the logs directly at the Console
            loggers.Add(clogs);                             // binds the ConsoleLogs 

            var projectCol = new ProjectCollection();       // present in Microsoft.Build.Evaluation
                                                            // encapsulates the project, its toolsets, a default set of 
                                                            // global properties, and the loggers that should be used 
                                                            // to build them
            projectCol.RegisterLoggers(loggers);            // binds logger with the project
            var project = projectCol.LoadProject(projectFilePath); 
            
            try
            {
                project.Build();
            }
            finally
            {
                projectCol.UnregisterAllLoggers();
            }
        }

        static void Main(string[] args)
        {
            CompileProjectFile();
            Console.ReadKey();
        }
    }
}
