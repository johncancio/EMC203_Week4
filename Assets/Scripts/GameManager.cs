using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerHP = 10;
    public TMP_Text hpText;
    public TMP_Text gameOverText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        UpdateHPUI();
    }

    public void LoseHP()
    {
        playerHP--;

        UpdateHPUI();

        if (playerHP <= 0)
        {
            GameOver();
        }
    }

    void UpdateHPUI()
    {
        if (hpText != null)
            hpText.text = "HP: " + playerHP;
    }

    void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}