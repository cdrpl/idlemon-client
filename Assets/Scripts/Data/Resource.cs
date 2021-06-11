using UnityEngine.Events;

public class Resource
{
    public int id;
    public int type;
    public int amount;

    public UnityEvent<Resource> OnAmountChange = new UnityEvent<Resource>();

    public void IncreaseAmount(int amount)
    {
        this.amount += amount;
        OnAmountChange.Invoke(this);
    }
}
