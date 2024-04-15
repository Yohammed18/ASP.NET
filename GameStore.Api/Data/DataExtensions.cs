﻿using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app){

        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        dbContext.Database.Migrate();

    }
}
