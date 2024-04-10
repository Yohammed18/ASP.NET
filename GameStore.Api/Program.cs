//import class
using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

//declare a list that will hold the data from Dtos
List<GameDto> gameStores = [
    new GameDto(1, "Call Of Duty", "Combat", 59.99M, new DateOnly(1995,5,19)),
    new GameDto(2, "The Witcher 3: Wild Hunt", "Action RPG", 49.99M, new DateOnly(2015,5,19)),
    new GameDto(3, "Grand Theft Auto V", "Action-adventure", 39.99M, new DateOnly(2013,9,17)),
    new GameDto(4, "Red Dead Redemption 2", "Action-adventure", 59.99M, new DateOnly(2018,10,26)),
    new GameDto(5, "The Last of Us Part II", "Action-adventure", 49.99M, new DateOnly(2020,6,19)),
    new GameDto(6, "Cyberpunk 2077", "Action RPG", 59.99M, new DateOnly(2020,12,10))
];

//api to handle endpoint displaying games
app.MapGet("/games", () => gameStores);

//get games by id
app.MapGet("/games/{id}", (int id) => gameStores.Find(game => game.Id == id )).WithName(GetGameEndpointName);

// POST / creating a game and added to the list of Dtos
app.MapPost("/create", (CreateGameDto newGame) => {
    GameDto game = new(
        gameStores.Count+1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    //add the new game to the list
    gameStores.Add(game);

    //standard response 200
    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
});

// PUT - games
app.MapPut("/games/update/{id}", (int id, UpdateGameDto updateGameDto) => {
    var index = gameStores.FindIndex(game => game.Id == id);
    

    gameStores[index] = new GameDto(
        id,
        updateGameDto.Name,
        updateGameDto.Genre,
        updateGameDto.Price,
        updateGameDto.ReleaseDate
    );

    return Results.NoContent();
});



//home page
app.MapGet("/", () => "Welcome to video games");

app.Run();
