using BenchmarkDotNet.Running;
using BenchmarkTestes.Tests.DatabaseOperations;
using System.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var f = ConfigurationManager. AppSettings["AllowedHosts"];

        var summary = BenchmarkRunner.Run<PostgreOperations>();
    }
}