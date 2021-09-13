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
    internal class IMultipleWinnerAwardDbContext : IAwardDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("test_multiplewinnerawards");
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
                    Producers = new[] { producer1, },
                },
                new AwardNominee()
                {
                    Title = "Movie 3",
                    Year = 1902,
                    IsWinner = true,
                    Studios = new[] { studio1, },
                    Producers = new[] { producer2, },
                },
                new AwardNominee()
                {
                    Title = "Movie 4",
                    Year = 1903,
                    IsWinner = true,
                    Studios = new[] { studio1, },
                    Producers = new[] { producer2, },
                },
                new AwardNominee()
                {
                    Title = "Movie 5",
                    Year = 1905,
                    IsWinner = true,
                    Studios = new[] { studio1, },
                    Producers = new[] { producer1, producer2, },
                },
            };

            this.AddRange(awards);
            this.SaveChanges();
        }
    }

    internal class MultipleWinnerTests : IntegrationTest<IMultipleWinnerAwardDbContext>
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

            responseDto.Count.Should().Be(5);
        }

        [Test]
        public async Task GetAllAwards_WithThreeNominees_ReturnsCorrectSizeList()
        {
            var httpResponse = await _client.GetAsync("/awards/nominees/");
            var responseDto = await httpResponse.Content.ReadAsAsync<List<AwardNomineeDto>>();

            responseDto.Count.Should().Be(5);
        }

        [Test]
        public async Task GetMinMaxVictories_WithoutWinners_ReturnsCorrectProducer()
        {
            var httpResponse = await _client.GetAsync("/producers/minmaxvictories/");
            var responseDto = await httpResponse.Content.ReadAsAsync<MinMaxProducerConsecutiveVictoriesDto>();

            responseDto.Min.Count.Should().Be(2);
            responseDto.Max.Count.Should().Be(1);

            responseDto.Min[0].Interval.Should().Be(1);
            responseDto.Min[1].Interval.Should().Be(1);
            responseDto.Max[0].Interval.Should().Be(4);

            responseDto.Min[0].PreviousWin.Should().Be(1900);
            responseDto.Min[0].FollowingWin.Should().Be(1901);

            responseDto.Min[1].PreviousWin.Should().Be(1902);
            responseDto.Min[1].FollowingWin.Should().Be(1903);

            responseDto.Max[0].PreviousWin.Should().Be(1901);
            responseDto.Max[0].FollowingWin.Should().Be(1905);

            responseDto.Min[0].Producer.Should().Be("Producer 1");
            responseDto.Min[1].Producer.Should().Be("Producer 2");
            responseDto.Max[0].Producer.Should().Be("Producer 1");
        }
    }
}
