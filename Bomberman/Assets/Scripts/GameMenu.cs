using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public Canvas canvas;

    public void RestartGame()
    {
        ///Restart current level

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        ///Load menu scene

        SceneManager.LoadScene(0);
    }
}
