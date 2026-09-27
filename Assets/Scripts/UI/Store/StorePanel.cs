using UnityEngine;

public class StorePanel : MonoBehaviour
{
    public static StorePanel inst;

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        
    }

    public void AddCurrency(int coins , int gems)
    {
        GameManager.Inst.AddCoins(coins);
        GameManager.Inst.AddGems(gems);

        Dashboard.Inst.UpdateDashCurrency();
    }

    public void CLose()
    {
        GameManager.Inst.PlayClickSFX();
        Destroy(gameObject);
    }
}
