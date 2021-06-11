using UnityEngine;
using UnityEngine.UI;

public class CampaignCollectRequestButton : MonoBehaviour
{
    public CampaignResourcesEarned campaignResourcesEarned;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public async void OnClick()
    {
        CampaignCollectResponse response = await CampaignCollectRequest.Send();

        if (response.HasError)
        {
            FlashMessage.Instance.Flash(response.message);
        }
        else
        {
            Global.Campaign.lastCollectedAt = response.lastCollectedAt;

            campaignResourcesEarned.UpdateResources(Global.Campaign);

            foreach (var transaction in response.transactions)
            {
                transaction.Apply();
            }
        }
    }
}
