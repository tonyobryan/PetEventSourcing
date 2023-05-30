using MediatR;
using Newtonsoft.Json;
using Pet.Bus.Http;
using Rescue.API;
using Rescue.API.Events;

var app = WebApp.Start(args);
app.MapGet("/", () => "Hello Rescuers!");
app.MapPost("/PetAdoptedEvent", async (IMediator mediator, BusMessage b) =>
{
    if (string.IsNullOrWhiteSpace(b?.JsonMessage))
    {
        return Results.BadRequest();
    }

    var adoptionEvent = JsonConvert.DeserializeObject<PetAdoptedEvent>(b.JsonMessage);

    if (adoptionEvent == null)
    {
        return Results.BadRequest();
    }

    await mediator.Send(adoptionEvent);
    return Results.Ok();
});

// TODO: Adopters table?
// CRUD Methods for Adopters
// Set Phone number

// CQRS? Mediator?

//TODO: New MS for querying, using Dapper?
//TODO: Query for rescue data

app.Run("https://localhost:7001");
