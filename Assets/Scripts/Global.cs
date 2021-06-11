using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Global values that belong to the user logged into the client.
/// </summary>
public static class Global
{
    /// <summary>
    /// Will be set to true once the user data has been initialized.
    /// </summary>
    public static bool IsInit { get; private set; }

    /// <summary>
    /// This is the API token of the user.
    /// </summary>
    public static string Token { get; private set; }

    /// <summary>
    /// This is the user account of the user.
    /// </summary>
    public static User User { get; private set; }

    public static Campaign Campaign { get; private set; }

    public static DailyQuestProgress[] DailyQuestProgress { get; private set; }

    public static Resource[] Resources { get; private set; }

    public static Unit[] Units { get; private set; }

    /* Game Data */
    public static UnitTemplate[] UnitTemplates { get; private set; }

    /// <summary>
    /// Triggered when a user is initialized.
    /// </summary>
    public static UnityEvent OnUserInit = new UnityEvent();

    public static void Init(SignInResponse response)
    {
        Token = response.token;
        User = response.user;
        Campaign = response.campaign;
        DailyQuestProgress = response.dailyQuestProgress;
        Resources = response.resources;
        Units = response.units;
        UnitTemplates = response.unitTemplates;

        // add default HTTP authentication header
        string auth = User.id + ":" + Token;
        Http.Client.DefaultRequestHeaders.Clear();
        if (!Http.Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", auth))
        {
            Debug.LogError("fail to add authorization header to HTTP client");
        }

        IsInit = true;

        OnUserInit.Invoke();
    }

    public static Resource Resource(Const.Resource resource)
    {
        return Resources[(int)resource];
    }
}
