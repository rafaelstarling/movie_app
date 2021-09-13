using System.Collections.Generic;

namespace MovieApplication.Domain.Models
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region relationships
        public virtual ICollection<AwardNominee> Nominations { get; set; }
        #endregion relationships

        public override string ToString() => Name;
    }
}
