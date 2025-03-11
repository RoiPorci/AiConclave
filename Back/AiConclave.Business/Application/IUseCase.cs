using System.Threading.Tasks;

namespace AiConclave.Business.Application;

/// <summary>
/// Defines the structure for a use case, specifying how it should execute its logic.
/// </summary>
/// <typeparam name="TUseCaseRequest">The type of the input request, derived from <see cref="IUseCaseRequest"/>.</typeparam>
/// <typeparam name="TUseCaseResponse">The type of the output response, derived from <see cref="UseCaseResponse"/>.</typeparam>
public interface IUseCase<in TUseCaseRequest, out TUseCaseResponse>
    where TUseCaseRequest : IUseCaseRequest
    where TUseCaseResponse : UseCaseResponse
{
    /// <summary>
    /// Executes the use case asynchronously, processing the given request and using the provided presenter for output.
    /// </summary>
    /// <param name="request">The input request containing the required data for the use case.</param>
    /// <param name="presenter">
    /// The presenter responsible for processing and presenting the response data.
    /// </param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task ExecuteAsync(TUseCaseRequest request, IUseCasePresenter<TUseCaseResponse> presenter);
}

