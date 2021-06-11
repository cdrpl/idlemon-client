using System.Threading.Tasks;

/// <summary>
/// HttpRequest will accept type T which must be an HTTP response.
/// </summary>
public abstract class HttpRequest<T> where T : HttpResponse
{
    public abstract Task<T> Send();
}
