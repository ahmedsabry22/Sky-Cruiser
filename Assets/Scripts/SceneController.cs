using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void StartScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void PausePlay(float timeScele)
    {
        Time.timeScale = timeScele;
    }

    public void Quit()
    {
        Application.Quit();
    }
}