using MediatR;

namespace AiConclave.Business.Application;

/// <summary>
///     Represents a request that includes a presenter for handling the response.
/// </summary>
/// <typeparam name="TResponse">The type of response associated with the request.</typeparam>
public interface IRequestWithPresenter<in TResponse> : IRequest<Unit>
    where TResponse : BaseResponse
{
    /// <summary>
    ///     Gets or sets the presenter responsible for handling the response.
    /// </summary>
    IResponsePresenter<TResponse> Presenter { get; }
}