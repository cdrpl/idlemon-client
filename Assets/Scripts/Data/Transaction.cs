using UnityEngine;

public class Transaction
{
    public Type type;
    public int amount;

    public enum Type
    {
        Gems,
        Gold,
        ExpStones,
        UserExp
    }

    /// <summary>
    /// Will update the correct global user data based on the transaction type and amount.
    /// </summary>
    public void Apply()
    {
        Debug.LogFormat("applying transaction type: {0} - amount: {1}", type, amount);

        switch (type)
        {
            case Type.Gems:
                Global.Resource(Const.Resource.Gems).IncreaseAmount(amount);
                break;

            case Type.Gold:
                Global.Resource(Const.Resource.Gold).IncreaseAmount(amount);
                break;

            case Type.ExpStones:
                Global.Resource(Const.Resource.ExpStone).IncreaseAmount(amount);
                break;

            case Type.UserExp:
                Global.User.IncreaseExp(amount);
                break;

            default:
                Debug.LogWarningFormat("could not apply transaction of type {0} since the switch case doesn't handle it", type);
                break;
        }
    }
}
