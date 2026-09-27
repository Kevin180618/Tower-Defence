using UnityEngine;
using UnityEngine.UI;

public class LvlBtns : MonoBehaviour
{
    public int LvlNum;
    public string Key;

    public GameObject Lock;
    private Button Btn;
    void Start()
    {
        Btn = GetComponent<Button>();
        SetProperties();
    }

    public void LoadLevel()
    {
        LevelManager.inst.CurrentLevelNum = LvlNum - 1;
        GameManager.Inst.PlayClickSFX();
        Dashboard.Inst.LoadScene(Key);
    }

    public void SetProperties()
    {
        if (LvlNum <= LevelManager.inst.MaxLevelUnlockedNum)
        {
            Debug.Log("Level num " + LvlNum);
            Debug.Log("MAx Level num " + LevelManager.inst.MaxLevelUnlockedNum);
            Btn.interactable = true;
            Lock.SetActive(false);
        }
        else
        {
            Debug.Log("Hello");
            Btn.interactable = false;
            Lock.SetActive(true);
        }
    }

}
