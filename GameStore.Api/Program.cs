//import class
using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//declare a list that will hold the data from Dtos
List<GameDto> gameStores = [
    new GameDto(1, "Call Of Duty", "Combat", 59.99M, new DateOnly(1995,5,19)),
    new GameDto(2, "The Witcher 3: Wild Hunt", "Action RPG", 49.99M, new DateOnly(2015,5,19)),
    new GameDto(3, "Grand Theft Auto V", "Action-adventure", 39.99M, new DateOnly(2013,9,17)),
    new GameDto(4, "Red Dead Redemption 2", "Action-adventure", 59.99M, new DateOnly(2018,10,26)),
    new GameDto(5, "The Last of Us Part II", "Action-adventure", 49.99M, new DateOnly(2020,6,19)),
    new GameDto(6, "Cyberpunk 2077", "Action RPG", 59.99M, new DateOnly(2020,12,10))
];

app.MapGet("/", () => gameStores);

app.Run();
