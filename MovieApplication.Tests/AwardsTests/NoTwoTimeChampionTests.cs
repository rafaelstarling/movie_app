using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieApplication.Domain;
using MovieApplication.Domain.Models;
using MovieApplication.Infrastructure.Dto;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieApplication.Tests.AwardsTests
{
    internal class INoTwoTimeChampionAwardDbContext : IAwardDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("test_notwotimechampawards");
        }

        public override void Seed()
        {
            var producer1 = new Producer()
            {
                Name = "Producer 1",
            };

            var producer2 = new Producer()
            {
                Name = "Producer 2",
            };

            var studio1 = new Studio()
            {
                Name = "Studio 1",
            };

            this.Add(producer1);
            this.Add(producer2);
            this.Add(studio1);

            var awards = new[]
            {
                new AwardNominee()
                {
                    Title = "Movie 1",
                    Year = 1900,
                    IsWinner = true,
                    Studios = new[] { studio1, },
                    Producers = new[] { producer1, },
                },
                new AwardNominee()
                {
                    Title = "Movie 2",
                    Year = 1901,
                    IsWinner = true,
                    Studios = new[] { studio1, },
                    Producers = new[] { producer2, },
                },
                new AwardNominee()
                {
                    Title = "Movie 3",
                    Year = 1902,
                    IsWinner = false,
                    Studios = new[] { studio1, },
                    Producers = new[] { producer1, producer2, },
                },
            };

            this.AddRange(awards);
            this.SaveChanges();
        }
    }

    internal class NoTwoTimeChampionTests : IntegrationTest<INoTwoTimeChampionAwardDbContext>
    {
        [Test]
        public async Task GetMinMaxVictories_WithoutData_ReturnsOkStatusCode()
        {
            var httpResponse = await _client.GetAsync("/producers/minmaxvictories/");
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetWinners_WithThreeNominees_ReturnsCorrectSizeList()
        {
            var httpResponse = await _client.GetAsync("/awards/");
            var responseDto = await httpResponse.Content.ReadAsAsync<List<AwardNomineeDto>>();

            responseDto.Count.Should().Be(2);
        }

        [Test]
        public async Task GetAllAwards_WithThreeNominees_ReturnsCorrectSizeList()
        {
            var httpResponse = await _client.GetAsync("/awards/nominees/");
            var responseDto = await httpResponse.Content.ReadAsAsync<List<AwardNomineeDto>>();

            responseDto.Count.Should().Be(3);
        }

        [Test]
        public async Task GetMinMaxVictories_WithoutWinners_ReturnsEmptyLists()
        {
            var httpResponse = await _client.GetAsync("/producers/minmaxvictories/");
            var responseDto = await httpResponse.Content.ReadAsAsync<MinMaxProducerConsecutiveVictoriesDto>();

            responseDto.Min.Should().BeEmpty();
            responseDto.Max.Should().BeEmpty();
        }
    }
}
