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
        Destroy(gameObject); // Destruye el objeto
    }

    public int GetCoinValue()
    {
        return coinValue;
    }
}
