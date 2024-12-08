using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static int lives;
    public static bool gameOver;
    public GameObject gameOverPanel;

    private void Start()
    {
        lives = 3;
        gameOver = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }

    public static void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            gameOver = true;
        }
    }
}
