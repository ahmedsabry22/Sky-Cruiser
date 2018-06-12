using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void StartScene(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(sceneIndex);
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