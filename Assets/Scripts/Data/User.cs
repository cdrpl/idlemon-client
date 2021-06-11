using System;
using UnityEngine.Events;

public class User
{
    public string id;
    public string name;
    public string email;
    public int exp;
    public DateTimeOffset createdAt;

    /// <summary>
    /// Param is the current amount of exp the user has.
    /// </summary>
    public UnityEvent<int> OnExpGain = new UnityEvent<int>();

    public void IncreaseExp(int exp)
    {
        this.exp += exp;
        OnExpGain.Invoke(this.exp);
    }
}
