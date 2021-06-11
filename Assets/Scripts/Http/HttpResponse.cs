public class HttpResponse
{
    public string message;
    public System.Net.HttpStatusCode httpCode;

    /// <summary>
    /// Returns true if the message var is not equal to null.
    /// </summary>
    public bool HasError => message != null;

    /// <summary>
    /// Alias for the message variables.
    /// </summary>
    public string Error => message;
}
