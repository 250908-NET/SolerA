// See https://aka.ms/new-console-template for more information
using System;
using Serilog;

namespace ToDoApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");



            var builder = WebApplication.CreateBuilder(args);

            //Configure logging before we "build" the app
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            builder.Host.UseSerilog();

            var app = builder.Build();
            app.MapGet("/", (ILogger<Program> logger) =>
            {
                return "Hello World!";
            });

            app.Run();
        }
    }
}