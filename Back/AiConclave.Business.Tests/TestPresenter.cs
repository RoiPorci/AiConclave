using AiConclave.Business.Application;

namespace AiConclave.Business.Tests;

/// <summary>
/// A test presenter implementation for capturing and retrieving the response of a use case.
/// </summary>
/// <typeparam name="TUseCaseResponse">The type of the use case response.</typeparam>
public class TestPresenter<TUseCaseResponse> : IResponsePresenter<TUseCaseResponse> 
    where TUseCaseResponse : BaseResponse
{
    private TUseCaseResponse? _response { get; set; }

    /// <summary>
    /// Captures and stores the use case response.
    /// </summary>
    /// <param name="response">The response to store.</param>
    public void Present(TUseCaseResponse response)
    {
        _response = response;
    }

    /// <summary>
    /// Retrieves the stored response.
    /// </summary>
    /// <returns>The stored response.</returns>
    /// <exception cref="NullReferenceException">
    /// Thrown if no response has been set before calling this method.
    /// </exception>
    public TUseCaseResponse GetResponse()
    {
        if (_response == null)
            throw new NullReferenceException("The response is null.");
        
        return _response;
    }
}