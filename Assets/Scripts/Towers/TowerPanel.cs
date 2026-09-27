using UnityEngine;
using UnityEngine.UI;

public class TowerPanel : MonoBehaviour
{
    public int ArcherTowerPrice;
    public int BomberTowerPrice;
    public int GoblinTowerPrice;

    public Text ArhcerPriceTxt;
    public Text BomberPriceTxt;
    public Text GoblinPriceTxt;

    private void Start()
    {
        ArhcerPriceTxt.text = ArcherTowerPrice.ToString();
        BomberPriceTxt.text = BomberTowerPrice.ToString();
        GoblinPriceTxt.text = GoblinTowerPrice.ToString();
    }

    public void PlaceArcherTower()
    {
        if (GameManager.Inst.Coins >= ArcherTowerPrice)
        {
            GameManager.Inst.DeductCoins(ArcherTowerPrice);
            PlayingManager.inst.AddArcherTower();
            PlayingManager.inst.UpdateCurrency();
            GameManager.Inst.PlayClickSFX();
            Destroy(gameObject);
        }
    }

    public void PlaceBomberTower()
    {
        if (GameManager.Inst.Coins >= BomberTowerPrice)
        {
            GameManager.Inst.DeductCoins(BomberTowerPrice);
            PlayingManager.inst.AddBombTower();
            PlayingManager.inst.UpdateCurrency();
            GameManager.Inst.PlayClickSFX();
            Destroy(gameObject);
        }
    }

    public void PlaceGoblinTower()
    {
        if (GameManager.Inst.Coins >= GoblinTowerPrice)
        {
            GameManager.Inst.DeductCoins(GoblinTowerPrice);
            PlayingManager.inst.AddGoblinTower();
            PlayingManager.inst.UpdateCurrency();
            GameManager.Inst.PlayClickSFX();
            Destroy(gameObject);
        }
    }
}
