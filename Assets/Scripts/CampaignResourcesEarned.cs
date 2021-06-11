using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Will track how many campaign resources have been earned.
/// </summary>
public class CampaignResourcesEarned : MonoBehaviour
{
    int exp;
    int gold;
    int expStones;

    /// <summary>
    /// On tick will broadcast exp, gold, and expStones.
    /// </summary>
    public UnityEvent<int, int, int> OnTick = new UnityEvent<int, int, int>();

    void OnEnable()
    {
        if (Global.IsInit)
        {
            StopAllCoroutines();
            StartCoroutine(Tick());
        }
    }

    IEnumerator Tick()
    {
        var wait = new WaitForSeconds(1f);
        Campaign campaign = Global.Campaign;

        while (gameObject.activeInHierarchy)
        {
            UpdateResources(campaign);
            yield return wait;
        }
    }

    /// <summary>
    /// Will update the local prediction of the amount of campaign resources earned.
    /// </summary>
    public void UpdateResources(Campaign campaign)
    {
        int diff = (int)DateTimeOffset.Now.Subtract(campaign.lastCollectedAt).TotalSeconds;

        if (diff > Const.CAMPAIGN_MAX_COLLECT)
        {
            diff = Const.CAMPAIGN_MAX_COLLECT;
        }

        exp = diff * (Const.CAMPAIGN_EXP_PER_SEC + (campaign.level / 5 * Const.CAMPAIGN_EXP_GROWTH));
        gold = diff * (Const.CAMPAIGN_GOLD_PER_SEC + (campaign.level / 5 * Const.CAMPAIGN_GOLD_GROWTH));
        expStones = diff * (Const.CAMPAIGN_EXP_STONE_PER_SEC + (campaign.level / 5 * Const.CAMPAIGN_EXP_STONE_GROWTH));

        OnTick.Invoke(exp, gold, expStones);
    }
}
