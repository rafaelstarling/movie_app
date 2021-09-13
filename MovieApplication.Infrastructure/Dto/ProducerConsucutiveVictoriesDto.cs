using MovieApplication.Domain.Models;
using System.Text.Json.Serialization;

namespace MovieApplication.Infrastructure.Dto
{
    public class ProducerConsucutiveVictoriesDto
    {
        public string Producer { get; set; }
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }


        [JsonConstructor]
        public ProducerConsucutiveVictoriesDto() { }

        public ProducerConsucutiveVictoriesDto(ProducerConsucutiveVictories victories)
        {
            Producer = victories.Producer.Name;
            Interval = victories.Interval;
            PreviousWin = victories.PreviousWin;
            FollowingWin = victories.FollowingWin;
        }
    }
}
