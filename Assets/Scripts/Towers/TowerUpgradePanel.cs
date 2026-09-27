using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradePanel : MonoBehaviour
{

    [Header("ArcherTower")]
    public List<GameObject>ArcherStars = new List<GameObject>();
    public Text ArcherPrice_txt;
    public Text ArcherUpgradeDiscription;
    public Text ArcherHighestDiscription;
    public GameObject ArcherUpgradeBtn;
    public int ArcherPrice;

    [Header("BombTower")]
    public List<GameObject> BombStars = new List<GameObject>();
    public Text BombPrice_txt;
    public Text BomberUpgradeDiscription;
    public Text BomberHighestDiscription;
    public GameObject BomberUpgradeBtn;
    public int BombPrice;

    [Header("GoblinTower")]
    public List<GameObject> GoblinStars = new List<GameObject>();
    public Text GoblinPrice_txt;
    public Text GoblinUpgradeDiscription;
    public Text GoblinHighestDiscription;
    public GameObject GoblinUpgradeBtn;
    public int GoblinPrice;


    void Start()
    {
        SetAllTowerData();

    }

    public void Close()
    {
        GameManager.Inst.PlayClickSFX();
        Destroy(gameObject);
    }

    public void SetAllTowerData()
    {
        SetArcherTower();
        SetBombTower();
        SetGoblinTower();
    }

    public void SetArcherTower()
    {
        for (int i = 0; i < ArcherStars.Count; i++)
        {
            if (i <= TowerManager.inst.AcherTowerUpgradeNum)
            {
                ArcherStars[i].SetActive(true);
            }
            else
            {
                ArcherStars[i].SetActive(false);

            }
        }

        if(TowerManager.inst.AcherTowerUpgradeNum >= 2)
        {
            ArcherHighestDiscription.gameObject.SetActive(true);
            ArcherUpgradeDiscription.gameObject.SetActive(false);
            ArcherUpgradeBtn.gameObject.SetActive(false);
        }
        else
        {
            ArcherHighestDiscription.gameObject.SetActive(false);
            ArcherUpgradeDiscription.gameObject.SetActive(true);
            ArcherUpgradeBtn.gameObject.SetActive(true);

            ArcherPrice_txt.text = TowerManager.inst.ArcherTowerUpgrades[TowerManager.inst.AcherTowerUpgradeNum].UpgradePrice.ToString();
            ArcherPrice = TowerManager.inst.ArcherTowerUpgrades[TowerManager.inst.AcherTowerUpgradeNum].UpgradePrice;

        }
    }

    public void SetBombTower()
    {
        for (int i = 0; i < BombStars.Count; i++)
        {
            if (i <= TowerManager.inst.BombTowerUpgradeNum)
            {
                BombStars[i].SetActive(true);
            }
            else
            {
                BombStars[i].SetActive(false);

            }
        }

        if (TowerManager.inst.BombTowerUpgradeNum >= 2)
        {
            BomberHighestDiscription.gameObject.SetActive(true);
            BomberUpgradeDiscription.gameObject.SetActive(false);
            BomberUpgradeBtn.gameObject.SetActive(false);
        }
        else
        {
            BomberHighestDiscription.gameObject.SetActive(false);
            BomberUpgradeDiscription.gameObject.SetActive(true);
            BomberUpgradeBtn.gameObject.SetActive(true);

            BombPrice_txt.text = TowerManager.inst.BombTowerUpgrades[TowerManager.inst.BombTowerUpgradeNum].UpgradePrice.ToString();
            BombPrice = TowerManager.inst.BombTowerUpgrades[TowerManager.inst.BombTowerUpgradeNum].UpgradePrice;

        }
    }

    public void SetGoblinTower()
    {
        for (int i = 0; i < GoblinStars.Count; i++)
        {
            if (i <= TowerManager.inst.GobllinTowerUpgradeNum)
            {
                GoblinStars[i].SetActive(true);
            }
            else
            {
                GoblinStars[i].SetActive(false);

            }
        }

        if (TowerManager.inst.GobllinTowerUpgradeNum >= 2)
        {
            GoblinHighestDiscription.gameObject.SetActive(true);
            GoblinUpgradeDiscription.gameObject.SetActive(false);
            GoblinUpgradeBtn.gameObject.SetActive(false);
        }
        else
        {
            GoblinHighestDiscription.gameObject.SetActive(false);
            GoblinUpgradeDiscription.gameObject.SetActive(true);
            GoblinUpgradeBtn.gameObject.SetActive(true);

            GoblinPrice_txt.text = TowerManager.inst.GoblinTowerUpgrades[TowerManager.inst.GobllinTowerUpgradeNum].UpgradePrice.ToString();
            GoblinPrice = TowerManager.inst.GoblinTowerUpgrades[TowerManager.inst.GobllinTowerUpgradeNum].UpgradePrice;

        }
    }

    public void ArchertowerUpgarde()
    {
        if(GameManager.Inst.Gems >= ArcherPrice)
        {
            GameManager.Inst.DeductGems(ArcherPrice);
            Dashboard.Inst.UpdateDashCurrency();
            TowerManager.inst.UpgradeArcherTower();
            SetAllTowerData();
            GameManager.Inst.PlayClickSFX();
        }
        else
        {
            Debug.Log("insufficient Money");
        }
    }

    public void BombtowerUpgarde()
    {
        if (GameManager.Inst.Gems >= BombPrice)
        {
            GameManager.Inst.DeductGems(BombPrice);
            Dashboard.Inst.UpdateDashCurrency();
            TowerManager.inst.UpgradeBombTower();
            SetAllTowerData();
            GameManager.Inst.PlayClickSFX();
        }
        else
        {
            Debug.Log("insufficient Money");
        }
    }

    public void GoblintowerUpgarde()
    {
        if (GameManager.Inst.Gems >= GoblinPrice)
        {
            GameManager.Inst.DeductGems(GoblinPrice);
            Dashboard.Inst.UpdateDashCurrency();
            TowerManager.inst.UpgradeGoblinTower();
            SetAllTowerData();
            GameManager.Inst.PlayClickSFX();
        }
        else
        {
            Debug.Log("insufficient Money");
        }
    }
}
