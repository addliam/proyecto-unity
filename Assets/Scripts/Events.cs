using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
public class Events : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
