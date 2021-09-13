namespace MovieApplication.Domain.Models
{
    public class ProducerConsucutiveVictories
    {
        public Producer Producer { get; set; }
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }

        public override string ToString() => $"{Producer}, {Interval} = {PreviousWin} a {FollowingWin}";
    }
}
