using MovieApplication.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace MovieApplication.Infrastructure.Dto
{
    public class MinMaxProducerConsecutiveVictoriesDto
    {
        public List<ProducerConsucutiveVictoriesDto> Min { get; set; }
        public List<ProducerConsucutiveVictoriesDto> Max { get; set; }

        [JsonConstructor]
        public MinMaxProducerConsecutiveVictoriesDto() { }

        public MinMaxProducerConsecutiveVictoriesDto(MinMaxProducerConsecutiveVictories consecutiveVictories) 
        {
            Min = consecutiveVictories.Min?.Select(e => new ProducerConsucutiveVictoriesDto(e)).ToList();
            Max = consecutiveVictories.Max?.Select(e => new ProducerConsucutiveVictoriesDto(e)).ToList();
        }
    }
}
