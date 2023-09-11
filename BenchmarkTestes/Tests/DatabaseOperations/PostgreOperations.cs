using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using Moments_Backend.Data;
using Moments_Backend.Models;
using System.Configuration;

namespace BenchmarkTestes.Tests.DatabaseOperations
{
    [RankColumn]
    [RPlotExporter]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class PostgreOperations : ManualConfig
    {
        public AppDbContext appDbContext { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            string occupation = ConfigurationManager.AppSettings["occupation"];
        }

        [Benchmark]
        public Moment UsingOnlyFirstMethod()
        {
            Moment foundMoment = appDbContext.Moments
                                .First(item => item.Id.Equals(19));

            return foundMoment;
        }

        [Benchmark]
        public Moment UsingOnlyFindMethod()
        {
            Moment foundMoment = appDbContext.Moments
                              .ToList()
                              .Find(item => item.Id.Equals(19));


            return foundMoment;
        }

        [Benchmark]
        public List<Moment> GetAll()
        {
            List<Moment> moments = appDbContext.Moments
                                               .ToList();


            return moments;
        }

        [Benchmark]
        public List<Moment> GetAllWithSelect()
        {
            List<Moment> moments = appDbContext.Moments
                                                 .Select(item => new Moment(item.Id, item.Title, item.Description, item.ImageURL, item.CreatedAt, item.Comments))
                                                 .ToList();


            return moments;
        }
    }
}
