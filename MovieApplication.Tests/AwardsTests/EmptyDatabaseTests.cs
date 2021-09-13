using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieApplication.Domain;
using MovieApplication.Infrastructure.Dto;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieApplication.Tests.AwardsTests
{
    internal class IEmptyAwardDbContext : IAwardDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("test_emptyawards");
        }

        public override void Seed()
        {
        }
    }

    internal class EmptyDatabaseTests : IntegrationTest<IEmptyAwardDbContext>
    {
        [Test]
        public async Task GetMinMaxVictories_WithoutData_ReturnsOkStatusCode()
        {
            var httpResponse = await _client.GetAsync("/producers/minmaxvictories/");
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetWinners_WithoutNominees_ReturnsCorrectSizeList()
        {
            var httpResponse = await _client.GetAsync("/awards/");
            var responseDto = await httpResponse.Content.ReadAsAsync<List<AwardNomineeDto>>();

            responseDto.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllAwards_WithoutNominees_ReturnsCorrectSizeList()
        {
            var httpResponse = await _client.GetAsync("/awards/nominees/");
            var responseDto = await httpResponse.Content.ReadAsAsync<List<AwardNomineeDto>>();

            responseDto.Should().BeEmpty();
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
