using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public int coinreward = 0;
    public int GemsReward = 0;

    public Text WavesTxt;
    public Text EnemyTxt;
    public Text CoinTxt;
    public Text GemsTxt;
    void Start()
    {
        WavesTxt.text = "Waves:- " + PlayingManager.inst.CurrentWave.ToString() + "/" + PlayingManager.inst.TotalWaves.ToString();
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
