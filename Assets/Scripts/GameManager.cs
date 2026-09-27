using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;

    [Header("Currency")]
    public int Coins;
    public int Gems;

    [Header("Audio Settings")]
    public bool IsSoundOn;
    public bool IsMusicOn;

    public AudioSource AudioSource;
    public AudioClip ClickSFX;

    [Header("Panels")]
    public GameObject PausePanel;
    public GameObject Victory;
    public GameObject GameOver;
    public GameObject storePanel;
    public GameObject SettingPanel;
    public GameObject TowerUpgradePanel;

    private const string COINS_KEY = "Coins";
    private const string GEMS_KEY = "Gems";

    private const string SOUND_KEY = "IsSoundOn";
    private const string MUSIC_KEY = "IsMusicOn";

    private void Awake()
    {
        if (Inst != null && Inst != this)
        {
            Destroy(gameObject);
            return;
        }

        Inst = this;
        DontDestroyOnLoad(gameObject);

        LoadCurrency();
        LoadAudioSettings();
    }
    private void LoadCurrency()
    {
        Coins = PlayerPrefs.GetInt(COINS_KEY, 500);
        Gems = PlayerPrefs.GetInt(GEMS_KEY, 10);
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0)
            return;

        Coins += amount;
        SaveCoins();
    }

    public void AddGems(int amount)
    {
        if (amount <= 0)
            return;

        Gems += amount;
        SaveGems();
    }

    public bool DeductCoins(int amount)
    {
        if (amount <= 0)
            return false;

        if (Coins < amount)
        {
            Debug.Log("Not enough coins!");
            return false;
        }

        Coins -= amount;
        SaveCoins();

        return true;
    }

    public bool DeductGems(int amount)
    {
        if (amount <= 0)
            return false;

        if (Gems < amount)
        {
            Debug.Log("Not enough gems!");
            return false;
        }

        Gems -= amount;
        SaveGems();

        return true;
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(COINS_KEY, Coins);
        PlayerPrefs.Save();
    }

    private void SaveGems()
    {
        PlayerPrefs.SetInt(GEMS_KEY, Gems);
        PlayerPrefs.Save();
    }

    private void LoadAudioSettings()
    {
        IsSoundOn = PlayerPrefs.GetInt(SOUND_KEY, 1) == 1;
        IsMusicOn = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;
    }

    public void SetSound(bool value)
    {
        IsSoundOn = value;

        PlayerPrefs.SetInt(SOUND_KEY, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetMusic(bool value)
    {
        IsMusicOn = value;

        PlayerPrefs.SetInt(MUSIC_KEY, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleSound()
    {
        SetSound(!IsSoundOn);
    }

    public void ToggleMusic()
    {
        SetMusic(!IsMusicOn);
    }

    public void PlayClickSFX()
    {
        if (!IsSoundOn)
            return;

        if (AudioSource == null || ClickSFX == null)
            return;

        AudioSource.PlayOneShot(ClickSFX);
    }
}