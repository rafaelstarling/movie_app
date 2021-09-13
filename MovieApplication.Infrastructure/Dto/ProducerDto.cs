using MovieApplication.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace MovieApplication.Infrastructure.Dto
{
    public class ProducerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Awards { get; set; }

        [JsonConstructor]
        public ProducerDto() { }

        public ProducerDto(Producer producer)
        {
            Id = producer.Id;
            Name = producer.Name;
            Awards = producer.Nominations
                .Where(e => e.IsWinner)
                .Select(e => e.Year)
                .OrderByDescending(e => e)
                .ToList();
        }
    }
}
