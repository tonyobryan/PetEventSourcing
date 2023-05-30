using MediatR;

namespace Hospital.API.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task<IResult> SendPetEvent(this IMediator mediator, IRequest request, CancellationToken token)
        {
            if (request == null)
            {
                return Results.BadRequest();
            }

            // TODO: Handle error and send something back other than okay

            await mediator.Send(request, token);
            return Results.Ok();
        }
    }
}
