using UnityEngine;

public class Storecell : MonoBehaviour
{
    public int CoinsCount;
    public int GemsCount;
    void Start()
    {
        
    }

    public void AddCoinsAndGems()
    {
        GameManager.Inst.PlayClickSFX();
        StorePanel.inst.AddCurrency(CoinsCount , GemsCount);
    }
}
