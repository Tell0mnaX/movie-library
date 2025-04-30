namespace ShowApp.Models
{
    public class ShowDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public int Seasons { get; set; }
        public string Genre { get; set; }
        public int StartYear { get; set; }
        public double Rating { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } // Ajouté pour afficher qui possède la série
    }
}