using UnityEngine;
using UnityEngine.UI;

public class CampaignResourcesEarnedUI : MonoBehaviour
{
    public Text expText;
    public Text goldText;
    public Text expStonesText;

    CampaignResourcesEarned campaignResourcesEarned;

    public void UpdateUI(int exp, int gold, int expStones)
    {
        expText.text = exp.ToString();
        goldText.text = gold.ToString();
        expStonesText.text = expStones.ToString();
    }
}
