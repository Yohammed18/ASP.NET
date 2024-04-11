namespace GameStore.Api.Dtos;


//Static class with extension method
public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";

    //declare a list that will hold the data from Dtos
    private static readonly List<GameDto> gameStores = [
        new GameDto(1, "Call Of Duty", "Combat", 59.99M, new DateOnly(1995,5,19)),
        new GameDto(2, "The Witcher 3: Wild Hunt", "Action RPG", 49.99M, new DateOnly(2015,5,19)),
        new GameDto(3, "Grand Theft Auto V", "Action-adventure", 39.99M, new DateOnly(2013,9,17)),
        new GameDto(4, "Red Dead Redemption 2", "Action-adventure", 59.99M, new DateOnly(2018,10,26)),
        new GameDto(5, "The Last of Us Part II", "Action-adventure", 49.99M, new DateOnly(2020,6,19)),
        new GameDto(6, "Cyberpunk 2077", "Action RPG", 59.99M, new DateOnly(2020,12,10))
    ];

    //extend a method of an exisiting class
    public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app){

        var group = app.MapGroup("games").WithParameterValidation();

        //api to handle endpoint displaying games
        group.MapGet("/", () => gameStores);

        //get games by id
        group.MapGet("/{id}", (int id) => 
        {
            GameDto? game = gameStores.Find(game => game.Id == id);
            //handles null case
            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);

        // POST / creating a game and added to the list of Dtos
        group.MapPost("/create", (CreateGameDto newGame) =>
        {
            //end point filters 

            GameDto game = new(
                gameStores.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            //add the new game to the list
            gameStores.Add(game);

            //standard response 200
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        // PUT - games
        group.MapPut("/update/{id}", (int id, UpdateGameDto updateGameDto) =>
        {
            var index = gameStores.FindIndex(game => game.Id == id);

            //handle if index is not found
            if(index == -1){
                return Results.NotFound();
            }

            //update the record based on the index found
            gameStores[index] = new GameDto(
                id,
                updateGameDto.Name,
                updateGameDto.Genre,
                updateGameDto.Price,
                updateGameDto.ReleaseDate
            );

            return Results.NoContent();
        });


        // DELETE /games end point
        group.MapDelete("/delete/{id}", (int id) =>
        {
            gameStores.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });


        return group;
    }

}
