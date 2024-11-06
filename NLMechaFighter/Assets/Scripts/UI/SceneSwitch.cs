using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex: sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
