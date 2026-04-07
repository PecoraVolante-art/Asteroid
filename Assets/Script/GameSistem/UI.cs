using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI finalscore;
    public TextMeshProUGUI highscore;

    public GameObject GameOverPanel;
    public GameObject Live1;
    public GameObject Live2;
    public GameObject Live3;

    void Start()
    {
        GameManager.playerLives = 3;
        GameManager.playerscore = 0;

        GameOverPanel.SetActive(false);

        Live1.SetActive(true);
        Live2.SetActive(true);
        Live3.SetActive(true);

        Time.timeScale = 1;
    }
    void Update()
    {
        score.SetText(GameManager.playerscore.ToString());

        UpdateLives();
        GameOver();
    }

    void UpdateLives()
    {
        if (GameManager.playerLives <= 2)
            Live3.SetActive(false);

        if (GameManager.playerLives <= 1)
            Live2.SetActive(false);

        if (GameManager.playerLives <= 0)
            Live1.SetActive(false);
    }

    void UpdateHighscore()
    {
        int savedHighScore = PlayerPrefs.GetInt("SavedHighScore", 0);

        if (GameManager.playerscore > savedHighScore)
        {
            PlayerPrefs.SetInt("SavedHighScore", GameManager.playerscore);
            PlayerPrefs.Save();
        }
    }

    void GameOver()
    {
        if (GameManager.playerLives == 0)
        {
            Time.timeScale = 0;

            UpdateHighscore();

            GameOverPanel.SetActive(true);

            finalscore.SetText("Score: " + GameManager.playerscore);

            highscore.SetText("Highscore: " + PlayerPrefs.GetInt("SavedHighScore"));
        }
    }

    public void RestartUI()
    {
        GameOverPanel.SetActive(false);

        Live1.SetActive(true);
        Live2.SetActive(true);
        Live3.SetActive(true);

        Time.timeScale = 1;
    }
}