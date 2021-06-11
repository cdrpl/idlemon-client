using UnityEngine;

public class Credentials
{
    const string EMAIL_PREF_KEY = "email";
    const string PASS_PREF_KEY = "pass";

    public static string SavedEmail => PlayerPrefs.GetString(EMAIL_PREF_KEY);
    public static string SavedPassword => PlayerPrefs.GetString(PASS_PREF_KEY);

    /// <summary>
    /// Returns true if user has saved remember me credentials.
    /// </summary>
    public static bool HasSavedCredentials => PlayerPrefs.HasKey(EMAIL_PREF_KEY);

    /// <summary>
    /// Update the player prefs used to store the remember me credentials.
    /// </summary>
    public static void UpdatePlayerPrefs(string email, string password, bool rememberMe)
    {
        if (rememberMe)
        {
            PlayerPrefs.SetString(EMAIL_PREF_KEY, email);
            PlayerPrefs.SetString(PASS_PREF_KEY, password);
        }
    }

    /// <summary>
    /// Delete saved credentials.
    /// </summary>
    public static void Delete()
    {
        PlayerPrefs.DeleteKey(EMAIL_PREF_KEY);
        PlayerPrefs.DeleteKey(PASS_PREF_KEY);
    }
}