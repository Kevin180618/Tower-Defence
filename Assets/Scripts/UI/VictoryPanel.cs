using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{
    internal int coinreward = 500;
    internal int GemsReward = 50;

    public Text WavesTxt;
    public Text EnemyTxt;
    public Text CoinTxt;
    public Text GemsTxt;
    void Start()
    {
        WavesTxt.text = "Waves:- "+ PlayingManager.inst.TotalWaves.ToString() + "/" + PlayingManager.inst.TotalWaves.ToString();
        EnemyTxt.text = "Enemy:- " + PlayingManager.inst.KilledEnemy.ToString();
        CoinTxt.text = coinreward.ToString();
        GemsTxt.text = GemsReward.ToString();
    }

    public void home()
    {
        GameManager.Inst.AddCoins(coinreward);
        GameManager.Inst.AddGems(GemsReward);
        GameManager.Inst.PlayClickSFX();

        SceneManager.LoadScene("DashBoard");
    }

}
