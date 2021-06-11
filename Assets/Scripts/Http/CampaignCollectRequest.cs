using System.Threading.Tasks;

public class CampaignCollectRequest
{
    const string URL = "/campaign/collect";

    public static async Task<CampaignCollectResponse> Send()
    {
        return await Http.PutRequest<CampaignCollectResponse>(URL);
    }
}