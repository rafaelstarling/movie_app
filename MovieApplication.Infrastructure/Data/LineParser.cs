using MovieApplication.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieApplication.Infrastructure.Data
{
    public class LineParser
    {
        private readonly string _configuration;
        private readonly int _yearColumnIndex;
        private readonly int _titleColumnIndex;
        private readonly int _studiosColumnIndex;
        private readonly int _producersColumnIndex;
        private readonly int _winnerColumnIndex;

        public LineParser(string configuration)
        {
            _configuration = configuration;

            int index = 0;
            foreach (var column in _configuration.Split(";"))
            {
                switch (column)
                {
                    case "year":
                        _yearColumnIndex = index;
                        break;
                    case "title":
                        _titleColumnIndex = index;
                        break;
                    case "studios":
                        _studiosColumnIndex = index;
                        break;
                    case "producers":
                        _producersColumnIndex = index;
                        break;
                    case "winner":
                        _winnerColumnIndex = index;
                        break;
                }

                index++;
            }
        }

        public AwardNominee ParseLine(string line)
        {
            var parts = line.Split(";");

            return new AwardNominee()
            {
                Year = int.Parse(parts[_yearColumnIndex]),
                Title = parts[_titleColumnIndex],
                Studios = GetStudios(parts[_studiosColumnIndex]),
                Producers = GetProducers(parts[_producersColumnIndex]),
                IsWinner = parts[_winnerColumnIndex] == "yes",
            };
        }

        private static List<Studio> GetStudios(string studios)
        {
            return studios.Split(",")
                .Select(studioName => new Studio()
                {
                    Name = studioName.Trim(),
                })
                .ToList();
        }

        private static List<Producer> GetProducers(string producers)
        {
            return producers.Split(",")
                .Select(studioName => new Producer()
                {
                    Name = studioName.Trim(),
                })
                .ToList();
        }
    }
}
