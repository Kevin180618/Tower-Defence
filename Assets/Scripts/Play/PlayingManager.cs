using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class PlayingManager : MonoBehaviour
{
    public static PlayingManager inst;
    public bool IsgameOver;
    public SplineContainer spline;
    public Transform spawnPoint;
    public GameObject TowerPanel;
    public GameObject CanvasParent;
    [Header("Currency")]
    public Text CoinsTxt;
    public Text GemsTxt;
    
    [Header("Towers")]
    public GameObject ArcherTower;
    public GameObject BombTower;
    public GameObject GoblinTower;
    public GameObject PlayerGobin;
    public GameObject EnemyGoblin;

    [Header("waves")]
    public int TotalWaves;
    public int CurrentWave;
    public int Enemycount;
    public Text WaveTxt;

    public ParticleSystem BlastParticle;
    internal int KilledEnemy;

    public GameObject placer;

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        UpdateCurrency();
        TotalWaves = LevelManager.inst.Levels[LevelManager.inst.CurrentLevelNum].wavesManagers.Count;
        CurrentWave = 0;
        Enemycount = LevelManager.inst.Levels[LevelManager.inst.CurrentLevelNum].wavesManagers[CurrentWave].EnemyCount;
        WaveTxt.text = "Wave:- " + (CurrentWave + 1).ToString() + "/" + (TotalWaves).ToString();
        StartCoroutine(SpawnEnemy());
    }

    public void AddArcherTower()
    {
        Instantiate(ArcherTower, placer.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        placer.SetActive(false);
    }
    public void AddBombTower()
    {
        Instantiate(BombTower, placer.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        placer.SetActive(false);
    }
    public void AddGoblinTower()
    {
        GameObject tower = Instantiate(GoblinTower, placer.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        GoblinTower goblinTower = tower.GetComponent<GoblinTower>();
        TowerPlacer towerPlacer = placer.GetComponent<TowerPlacer>();
        goblinTower.P1 = towerPlacer.G1.transform;
        goblinTower.P2 = towerPlacer.G2.transform;
        goblinTower.P3 = towerPlacer.G3.transform;
        placer.SetActive(false);
    }

    public void Subtractenemy()
    {
        Enemycount--;
        KilledEnemy++;

        if (Enemycount <= 0)
        {
            if (CurrentWave + 1 < TotalWaves)
            {
                CurrentWave++;

                Enemycount = LevelManager.inst.Levels[LevelManager.inst.CurrentLevelNum].wavesManagers[CurrentWave].EnemyCount;

                WaveTxt.text = "Wave:- " + (CurrentWave + 1) + "/" + TotalWaves;

                StartCoroutine(SpawnEnemy());
            }
            else
            {
                Instantiate(GameManager.Inst.Victory,CanvasParent.transform);

                LevelManager.inst.Levels[LevelManager.inst.CurrentLevelNum].IsCompleted = true;

                LevelManager.inst.MaxLevelUnlockedNum++;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < Enemycount; i++)
        {
            GameObject goblin = Instantiate(EnemyGoblin, spawnPoint.position, Quaternion.identity);
            Enemy enemy = goblin.GetComponent<Enemy>();
            enemy.enemyHp.maxhealth = LevelManager.inst.Levels[LevelManager.inst.CurrentLevelNum].wavesManagers[CurrentWave].EnemyHealth;
            enemy.Speed = LevelManager.inst.Levels[LevelManager.inst.CurrentLevelNum].wavesManagers[CurrentWave].EnemySpeed;
            enemy.PlayerMoveSpeed = LevelManager.inst.Levels[LevelManager.inst.CurrentLevelNum].wavesManagers[CurrentWave].EnemySpeed;
            enemy.spline = spline;

            float randomDelay = Random.Range(3, 6);

            yield return new WaitForSeconds(randomDelay);
        }
    }

    public void Pause()
    {
        Instantiate(GameManager.Inst.PausePanel , CanvasParent.transform);
    }

    public void GameOver()
    {
        Instantiate(GameManager.Inst.GameOver, CanvasParent.transform);
    }

    public void UpdateCurrency()
    {
        CoinsTxt.text = GameManager.Inst.Coins.ToString();
        GemsTxt.text = GameManager.Inst.Gems.ToString();
    }
}
