using Newtonsoft.Json;
using System.Threading.Tasks;

public class SignUpRequest
{
    public string name;
    public string email;
    public string pass;

    public SignUpRequest(string name, string email, string pass)
    {
        this.name = name;
        this.email = email;
        this.pass = pass;
    }

    /// <summary>
    /// HTTP request for new user sign up.
    /// </summary>
    public static async Task<SignUpResponse> Send(string name, string email, string pass)
    {
        var request = new SignUpRequest(name, email, pass);
        string data = JsonConvert.SerializeObject(request);
        return await Http.PostRequest<SignUpResponse>("/user/sign-up", data);
    }
}
