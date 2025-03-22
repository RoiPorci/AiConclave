using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace AiConclave.Business.Application;

/// <summary>
///     Base handler for processing requests with a response presenter.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public abstract class BaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, Unit>
    where TRequest : IRequestWithPresenter<TResponse>
    where TResponse : BaseResponse
{
    /// <summary>
    ///     Handles the incoming request asynchronously and presents the response.
    /// </summary>
    /// <param name="request">The request containing the input data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, returning <see cref="Unit.Value" /> upon completion.
    /// </returns>
    public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var response = await HandleRequest(request, cancellationToken);

        request.Presenter.Present(response);

        return Unit.Value;
    }

    /// <summary>
    ///     Processes the request and returns a response.
    /// </summary>
    /// <param name="request">The request containing the input data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, returning the response of type <typeparamref name="TResponse" />.
    /// </returns>
    protected abstract Task<TResponse> HandleRequest(TRequest request, CancellationToken cancellationToken);
}