using MovieApplication.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace MovieApplication.Infrastructure.Dto
{
    public class AwardNomineeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public bool IsWinner { get; set; }
        public List<string> Producers { get; set; }
        public List<string> Studios { get; set; }

        [JsonConstructor]
        public AwardNomineeDto() { }

        public AwardNomineeDto(AwardNominee awardNominee)
        {
            Id = awardNominee.Id;
            Title = awardNominee.Title;
            Year = awardNominee.Year;
            IsWinner = awardNominee.IsWinner;
            Producers = awardNominee.Producers
                .Select(e => e.Name)
                .OrderBy(e => e)
                .ToList();
            Studios = awardNominee.Studios
                .Select(e => e.Name)
                .OrderBy(e => e)
                .ToList();
        }
    }
}
