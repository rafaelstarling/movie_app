using System.Collections.Generic;

namespace MovieApplication.Domain.Models
{
    public class AwardNominee
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public bool IsWinner { get; set; }

        #region relationships
        public virtual ICollection<Studio> Studios { get; set; }
        public virtual ICollection<Producer> Producers { get; set; }
        #endregion relationships

        public override string ToString() => $"{Title}, {Year}";
    }
}
