using System;
using System.Collections.Generic;
using System.Linq;



namespace Lambda_test
{
    class StarRating
    {
        public string Name { get; set; }
        public double Stars { get; set; }
    }

    class Game
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int SteamScore { get; set; } 
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            var gameList = new List<Game>
            {
                new Game {Name = "Risk of Rain", ReleaseDate = new DateTime(2019,11,8), SteamScore = 9},
                new Game {Name = "Dark Souls 3", ReleaseDate = new DateTime(2015,3,24), SteamScore = 9}
            };

            List<string> gameNames = gameList.Select(g => g.Name).ToList();

            List<StarRating> starRatings = gameList.Select(g => new StarRating
            {
                Name = g.Name,
                Stars = g.SteamScore * 0.5
            }).ToList();
            Game gameWithScoreOf2 = gameList.First(g => g.SteamScore == 2);
            Console.WriteLine(gameNames);

            Console.ReadKey();
        }
    }
}
