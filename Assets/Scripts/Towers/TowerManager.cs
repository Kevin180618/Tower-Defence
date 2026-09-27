using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerUpgrades
{
    public float Range;
    public float Damage;
    public float RateOfFire;
    public float Health;

    public int UpgradePrice;
}

public class TowerManager : MonoBehaviour
{
    public static TowerManager inst;

    public List<TowerUpgrades> ArcherTowerUpgrades = new List<TowerUpgrades>();
    public List<TowerUpgrades> BombTowerUpgrades = new List<TowerUpgrades>();
    public List<TowerUpgrades> GoblinTowerUpgrades = new List<TowerUpgrades>();

    public int AcherTowerUpgradeNum;
    public int BombTowerUpgradeNum;
    public int GobllinTowerUpgradeNum;

    private void Awake()
    {
        inst = this;
        LoadTowerUpgrades();
    }

    private void Start()
    {
    }

    public void SaveTowerUpgrades()
    {
        PlayerPrefs.SetInt("ArcherTowerUpgrade", AcherTowerUpgradeNum);
        PlayerPrefs.SetInt("BombTowerUpgrade", BombTowerUpgradeNum);
        PlayerPrefs.SetInt("GoblinTowerUpgrade", GobllinTowerUpgradeNum);

        PlayerPrefs.Save();
    }

    public void LoadTowerUpgrades()
    {
        AcherTowerUpgradeNum = PlayerPrefs.GetInt("ArcherTowerUpgrade", 0);
        BombTowerUpgradeNum = PlayerPrefs.GetInt("BombTowerUpgrade", 0);
        GobllinTowerUpgradeNum = PlayerPrefs.GetInt("GoblinTowerUpgrade", 0);
    }

    public void UpgradeArcherTower()
    {
        if (AcherTowerUpgradeNum < ArcherTowerUpgrades.Count - 1)
        {
            AcherTowerUpgradeNum++;
            SaveTowerUpgrades();
        }
    }

    public void UpgradeBombTower()
    {
        if (BombTowerUpgradeNum < BombTowerUpgrades.Count - 1)
        {
            BombTowerUpgradeNum++;
            SaveTowerUpgrades();
        }
    }

    public void UpgradeGoblinTower()
    {
        if (GobllinTowerUpgradeNum < GoblinTowerUpgrades.Count - 1)
        {
            GobllinTowerUpgradeNum++;
            SaveTowerUpgrades();
        }
    }
}