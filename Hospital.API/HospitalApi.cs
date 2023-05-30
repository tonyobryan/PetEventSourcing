using Azure.Core;
using Azure.Messaging;
using Hospital.API;
using Hospital.API.Extensions;
using Hospital.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pet.Bus.Http;

var app = WebApp.Start(args);

app.MapGet("/", () => "Hello Hospital!");

app.MapPost("/PetTransferedEvent", async (IMediator mediator, [FromBody] BusMessage b, CancellationToken token) =>
{
    if (string.IsNullOrWhiteSpace(b?.JsonMessage))
    {
        return Results.BadRequest();
    }

    var transferEvent = JsonConvert.DeserializeObject<PetTransferedEvent>(b.JsonMessage) ?? default;
    //return await mediator.SendPetEvent(transferEvent, token); // FIX ME
    await mediator.Send(transferEvent, token);
    return Results.Ok();
});

app.MapPost("/Admit/{petId}", async (IMediator mediator, int petId, CancellationToken token) =>
{
    //return await mediator.SendPetEvent(weightEvent, token); // FIX ME
    await mediator.Send(new PetAdmittedEvent(petId), token);
    return Results.Ok();
});

app.MapPost("/Discharge/{petId}", async (IMediator mediator,  int petId, CancellationToken token) =>
{
    //return await mediator.SendPetEvent(weightEvent, token); // FIX ME
    await mediator.Send(new PetDischargedEvent(petId), token);
    return Results.Ok();
});

app.MapPost("/SetWeight", async (IMediator mediator, SetPatiantWeightEvent weightEvent, CancellationToken token) =>
{
    //return await mediator.SendPetEvent(weightEvent, token); // FIX ME
    await mediator.Send(weightEvent, token);
    return Results.Ok();
}); 

app.MapPost("/SetBloodType", async (IMediator mediator, SetPatiantBloodTypeEvent bloodEvent, CancellationToken token) =>
{
    //return await mediator.SendPetEvent(bloodEvent, token); // FIX ME
    await mediator.Send(bloodEvent, token);
    return Results.Ok();
});

app.Run("https://localhost:7002");