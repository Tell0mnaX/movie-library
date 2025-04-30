using ShowApp.Models;

public static class ShowSeeder
{
    public static void SetShowSeeder(ShowContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Username = "Guillaume" },
                new User { Username = "Alice" },
                new User { Username = "Bob" }
            );
            context.SaveChanges();
        }

        if(!context.Shows.Any()){
            var users = context.Users.ToList();

            var shows = new List<Show>
            {
                new Show { Id = 1, Title = "Breaking Bad", Creator = "Vince Gilligan", Seasons = 5, Genre = "Crime", StartYear = 2008, Rating = 0.0, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMzU5ZGYzNmQtMTdhYy00OGRiLTg0NmQtYjVjNzliZTg1ZGE4XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg", UserId = users.First().Id },
                new Show { Id = 2, Title = "Stranger Things", Creator = "Duffer Brothers", Seasons = 4, Genre = "Science Fiction", StartYear = 2016, Rating = 0.0, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMjg2NmM0MTEtYWY2Yy00NmFlLTllNTMtMjVkZjEwMGVlNzdjXkEyXkFqcGc@._V1_.jpg", UserId = users.First().Id },
                new Show { Id = 3, Title = "The Crown", Creator = "Peter Morgan", Seasons = 6, Genre = "Drama", StartYear = 2016, Rating = 0.0, ImageUrl = "https://m.media-amazon.com/images/M/MV5BODcyODZlZDMtZGE0Ni00NjBhLWJlYTAtZDdlNWY3MzkwMGVhXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg", UserId = users.First().Id },
                new Show { Id = 4, Title = "Dark", Creator = "Baran bo Odar", Seasons = 3, Genre = "Science Fiction", StartYear = 2017, Rating = 0.0, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMzgyNTk3NDktYWEzMS00M2M3LWI1NDUtMWNhZDUzZTllZTU0XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg", UserId = users.First().Id },
                new Show { Id = 5, Title = "Friends", Creator = "David Crane", Seasons = 10, Genre = "Comedy", StartYear = 1994, Rating = 0.0, ImageUrl = "https://m.media-amazon.com/images/M/MV5BMjk0ZmJjY2MtYTdjMi00NzlmLTg4YjktOTRlNTY3OWI2M2JkXkEyXkFqcGc@._V1_.jpg", UserId = users.First().Id }
            };

            context.Shows.AddRange(shows);
            context.SaveChanges();
        }
    } 
}