using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

//Data annotation will assist us with ensuring value that are null are properly handled [reqiored]

//end point filters

public record class CreateGameDto(
    [Required][StringLength(50)]string Name, 
    [Required][StringLength(20)]string Genre, 
    [Range(1,200)]decimal Price, 
    DateOnly ReleaseDate
);
