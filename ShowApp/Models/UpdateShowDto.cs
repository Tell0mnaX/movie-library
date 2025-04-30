namespace ShowApp.Models
{
    public class UpdateShowDto
    {
        public string Title { get; set; }
        public string Creator { get; set; }
        public string Genre { get; set; }
        public int Seasons { get; set; }
        public int StartYear { get; set; }
        public double Rating { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
    }
}
