using Newtonsoft.Json;
using System.Threading.Tasks;

public class SignInRequest
{
    const string URL = "/user/sign-in";

    public string email;
    public string pass;

    public SignInRequest(string email, string pass)
    {
        this.email = email;
        this.pass = pass;
    }

    /// <summary>
    /// Makes an HTTP request to the Idlemon sign in route.
    /// </summary>
    public static async Task<SignInResponse> Send(string email, string pass)
    {
        var request = new SignInRequest(email, pass);
        string data = JsonConvert.SerializeObject(request);
        return await Http.PostRequest<SignInResponse>(URL, data);
    }
}
