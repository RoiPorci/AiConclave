using AiConclave.Business.Application;

namespace AiConclave.Business.Tests;

public class TestPresenter<TUseCaseResponse> : IUseCasePresenter<TUseCaseResponse> 
    where TUseCaseResponse : UseCaseResponse
{
    private TUseCaseResponse? _response { get; set; }
    
    public void Present(TUseCaseResponse response)
    {
        _response = response;
    }

    public TUseCaseResponse GetResponse()
    {
        if (_response == null)
            throw new NullReferenceException("The response is null.");
        
        return _response;
    }
}