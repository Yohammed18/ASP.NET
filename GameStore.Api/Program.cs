//import class
using GameStore.Api.Data;
using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
//Define a connection to db (look in the appsettings)
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

// migration 
//dotnet tool install --global dotnet-ef --version 8.0.4
//dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.4

var app = builder.Build();

app.MapGamesEndPoints();

app.MigrateDb();
app.Run();
