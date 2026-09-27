using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    [Header("Sound")]
    public Image SoundBtnImage;

    [Header("Music")]
    public Image MusicBtnImage;


    [Header("Sprites")]
    public Sprite OnSprite;
    public Sprite OffSprite;
    private void Start()
    {
        InitializeSettings();

    }

    // Initialize button images based on GameManager values
    private void InitializeSettings()
    {
        UpdateSoundImage();
        UpdateMusicImage();
    }

    public void ToggleSound()
    {
        GameManager.Inst.ToggleSound();
        GameManager.Inst.PlayClickSFX();
        UpdateSoundImage();
    }

    private void UpdateSoundImage()
    {
        if (GameManager.Inst.IsSoundOn)
        {
            SoundBtnImage.sprite = OnSprite;
        }
        else
        {
            SoundBtnImage.sprite = OffSprite;
        }
    }

    public void ToggleMusic()
    {
        GameManager.Inst.ToggleMusic();
        GameManager.Inst.PlayClickSFX();
        UpdateMusicImage();
    }

    private void UpdateMusicImage()
    {
        if (GameManager.Inst.IsMusicOn)
        {
            MusicBtnImage.sprite = OnSprite;
        }
        else
        {
            MusicBtnImage.sprite = OffSprite;
        }
    }

    public void Cloase()
    {
        GameManager.Inst.PlayClickSFX();
        Destroy(gameObject);
    }
}
