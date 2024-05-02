using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool collected = false;
    public int coinValue = 0;

    public bool IsCollected()
    {
        return collected;
    }

    public void Collect()
    {
        collected = true;
        gameObject.SetActive(false);
    }

    public int GetCoinValue()
    {
        return coinValue;
    }
}
