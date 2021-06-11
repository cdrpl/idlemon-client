using System;

public class CampaignCollectResponse : HttpResponse
{
    public Transaction[] transactions;
    public DateTimeOffset lastCollectedAt;
}
