namespace ShowApp.Models
{
    public class Show
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public int Seasons { get; set; }
        public string Genre { get; set; }
        public int StartYear { get; set; }
        public double Rating { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int UserId {get; set;}
        public User User {get; set;}

       
    }
}