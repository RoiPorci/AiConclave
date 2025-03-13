namespace AiConclave.Business.Application;

/// <summary>
/// Defines a presenter interface for processing and presenting a use case response.
/// </summary>
/// <typeparam name="T">The type of the use case response, derived from <see cref="BaseResponse"/>.</typeparam>
public interface IResponsePresenter<in T> where T : BaseResponse
{
    /// <summary>
    /// Processes and presents the specified use case response.
    /// </summary>
    /// <param name="response">The use case response to present.</param>
    void Present(T response);
}

