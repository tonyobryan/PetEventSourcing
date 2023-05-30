using Microsoft.AspNetCore.Mvc;
using Pets.API.Request;
using MediatR;
using Pets.API;

var app = WebApp.Start(args);

app.MapGet("/", () => "Hello Pets!");

// TODO: Add SetDateOfBirth endpoint?

app.MapPost("/AddPet", async (IMediator mediator, [FromBody] CreatePetRequest request) =>
{
    await mediator.Send(request);
    return Results.Ok(); //TODO, return something other than okay :)
});

app.MapPost("/FlagForAdoption/{petId}", async (IMediator mediator, AdoptionRequest request) =>
{
    await mediator.Send(request);
    return Results.Ok();
});

app.MapPost("/Transfer/{petId}", async (IMediator mediator, int petId) =>
{
    await mediator.Send(new TransferRequest { PetId = petId });
    return Results.Ok();
});

app.Run("https://localhost:7000");
    