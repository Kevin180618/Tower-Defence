using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0f;
    }


    public void Home()
    {
        Time.timeScale = 1f;
        GameManager.Inst.PlayClickSFX();
        SceneManager.LoadScene("DashBoard");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        GameManager.Inst.PlayClickSFX();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Close()
    {
        Time.timeScale = 1f;
        GameManager.Inst.PlayClickSFX();
        Destroy(gameObject);
    }

}
