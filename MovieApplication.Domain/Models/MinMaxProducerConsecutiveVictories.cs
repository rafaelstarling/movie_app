using System.Collections.Generic;

namespace MovieApplication.Domain.Models
{
    public class MinMaxProducerConsecutiveVictories
    {
        private MinMaxProducerConsecutiveVictories() { }

        public List<ProducerConsucutiveVictories> Min { get; set; }
        public List<ProducerConsucutiveVictories> Max { get; set; }

        public class Builder
        {
            private int MaxRange { get; set; }
            private int MinRange { get; set; }
            private MinMaxProducerConsecutiveVictories MinMaxVictories { get; set; }

            public Builder()
            {
                MaxRange = int.MinValue;
                MinRange = int.MaxValue;

                MinMaxVictories = new MinMaxProducerConsecutiveVictories()
                {
                    Min = new List<ProducerConsucutiveVictories>(),
                    Max = new List<ProducerConsucutiveVictories>(),
                };
            }

            public MinMaxProducerConsecutiveVictories Build() => MinMaxVictories;

            public void CheckProducerAwards(Producer producer, AwardNominee firstTitle, AwardNominee secondTitle)
            {
                CheckMinRangeAwards(producer, firstTitle, secondTitle);
                CheckMaxRangeAwards(producer, firstTitle, secondTitle);
            }

            private void CheckMinRangeAwards(Producer producer, AwardNominee firstTitle, AwardNominee secondTitle)
            {
                var dateRange = secondTitle.Year - firstTitle.Year;

                if (dateRange > MinRange)
                {
                    return;
                }

                if (dateRange < MinRange)
                {
                    MinRange = dateRange;
                    MinMaxVictories.Min.Clear();
                }

                AddVictory(
                    MinMaxVictories.Min,
                    producer,
                    dateRange,
                    firstTitle,
                    secondTitle
                );
            }

            private void CheckMaxRangeAwards(Producer producer, AwardNominee firstTitle, AwardNominee secondTitle)
            {
                var dateRange = secondTitle.Year - firstTitle.Year;

                if (dateRange < MaxRange)
                {
                    return;
                }

                if (dateRange > MaxRange)
                {
                    MaxRange = dateRange;
                    MinMaxVictories.Max.Clear();
                }

                AddVictory(
                    MinMaxVictories.Max,
                    producer,
                    dateRange,
                    firstTitle,
                    secondTitle
                );
            }

            private void AddVictory(List<ProducerConsucutiveVictories> victoriesList, Producer producer, int dateRange, AwardNominee firstTitle, AwardNominee secondTitle)
            {
                victoriesList.Add(
                    new ProducerConsucutiveVictories()
                    {
                        Producer = producer,
                        Interval = dateRange,
                        PreviousWin = firstTitle.Year,
                        FollowingWin = secondTitle.Year,
                    }
                );
            }
        }
    }
}
