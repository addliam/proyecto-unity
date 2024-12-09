using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
public class Events : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("Escena2");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
