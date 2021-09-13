using MovieApplication.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace MovieApplication.Infrastructure.Dto
{
    public class StudioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Awards { get; set; }

        [JsonConstructor]
        public StudioDto() { }

        public StudioDto(Studio studio)
        {
            Id = studio.Id;
            Name = studio.Name;
            Awards = studio.Nominations
                .Where(e => e.IsWinner)
                .Select(e => e.Year)
                .OrderByDescending(e => e)
                .ToList();
        }
    }
}
