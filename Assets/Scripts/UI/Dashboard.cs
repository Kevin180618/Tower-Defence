using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    public static Dashboard Inst;

    public Text CoinsTxt;
    public Text GemsText;

    public Transform CanvasParent;

    private void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        UpdateDashCurrency();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateDashCurrency()
    {
        CoinsTxt.text = GameManager.Inst.Coins.ToString();
        GemsText.text = GameManager.Inst.Gems.ToString();
    }

    public void OpenStore()
    {
        GameManager.Inst.PlayClickSFX();
        Instantiate(GameManager.Inst.storePanel, CanvasParent);
    }

    public void OpenSettings()
    {
        GameManager.Inst.PlayClickSFX();
        Instantiate(GameManager.Inst.SettingPanel, CanvasParent);
    }

    public void OpenTowerUpgrade()
    {
        GameManager.Inst.PlayClickSFX();
        Instantiate(GameManager.Inst.TowerUpgradePanel, CanvasParent);
    }

    public void ExitGame()
    {
        GameManager.Inst.PlayClickSFX();
        Application.Quit();
    }
}
