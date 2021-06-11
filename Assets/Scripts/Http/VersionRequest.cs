using System.Threading.Tasks;

/// <summary>
/// HTTP request to determine the expected client version.
/// </summary>
public class VersionRequest : HttpRequest<VersionResponse>
{
    override public async Task<VersionResponse> Send()
    {
        return await Http.GetRequest<VersionResponse>(Const.HTTP_URL + "/version");
    }
}
