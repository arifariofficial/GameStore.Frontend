using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
    private readonly List<GameSummary> games =
    [
    new (){
        Id = 1,
        Name = "Street Figher 2",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateOnly(1992, 7, 15)
    },
    new () {
        Id = 2,
        Name = "Final Fantasy XIV",
        Genre = "Roleplaying",
        Price = 59.99M,
        ReleaseDate = new DateOnly(2010, 9, 30)
    },
    new () {
        Id = 3,
        Name = "Fifa 23",
        Genre = "Sports",
        Price = 69.99M,
        ReleaseDate = new DateOnly(2022, 9, 27)
    }
    ];

    private readonly Genre[] genres = new GenresClient().GetGenres();

    public GameSummary[] GetGames() => [.. games];

    public void AddGame(GameDetails gameDetails)
    {
        Genre genre = GetGenreById(gameDetails.GenreId);

        var gameSummary = new GameSummary
        {
            Id = games.Count + 1,
            Name = gameDetails.Name,
            Genre = genre.Name,
            Price = gameDetails.Price,
            ReleaseDate = gameDetails.ReleaseDate
        };

        games.Add(gameSummary);
    }

    public GameDetails GetGame(int id)
    {
        GameSummary game = GetGameSummaryById(id);

        var genre = genres.Single(genre => string.Equals(
            genre.Name,
            game.Genre,
            StringComparison.OrdinalIgnoreCase));

        return new GameDetails
        {
            Id = game.Id,
            Name = game.Name,
            GenreId = genre.Id.ToString(),
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }


    public void UpdateGame(GameDetails updatedGame)
    {
        var genre = GetGenreById(updatedGame.GenreId);
        GameSummary existingGame = GetGameSummaryById(updatedGame.Id);

        existingGame.Name = updatedGame.Name;
        existingGame.Genre = genre.Name;
        existingGame.Price = updatedGame.Price;
        existingGame.ReleaseDate = updatedGame.ReleaseDate;
    }

    public void DeleteGame(int id)
    {
        GameSummary game = GetGameSummaryById(id);
        games.Remove(game);
    }

    private GameSummary GetGameSummaryById(int id)
    {
        GameSummary? game = games.Find(g => g.Id == id);
        ArgumentNullException.ThrowIfNull(game);
        return game;
    }

    private Genre GetGenreById(string? id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return genres.Single(genre => genre.Id == int.Parse(id));

    }

}
