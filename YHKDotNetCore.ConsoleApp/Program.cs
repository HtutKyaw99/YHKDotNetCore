// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using YHKDotNetCore.ConsoleApp;

Console.WriteLine("Hello, World!");

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();

//adoDotNetExample.Read();

//adoDotNetExample.Create("Title","Author","Content");

//adoDotNetExample.Update(11, "Test Title", "Test Author", "Test Content");

//adoDotNetExample.Delete(11);

adoDotNetExample.ReadSingle(10);

Console.ReadKey();