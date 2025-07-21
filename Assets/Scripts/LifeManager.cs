using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;

    [Header("Life Settings")]
    public int startingLives = 3;
    private int currentLives;
    private bool gameOver = false;

    [Header("UI References")]
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public GameObject restartButton;
    public List<Button> directionButtons;

    [Header("Base Visual")]
    public SpriteRenderer baseSprite;
    public Color flashColor = Color.red;
    public float flashDuration = 0.2f;

    [Header("Audio")]
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioSource audioSource;

    private Color originalColor;

    [Header("Game References")]
    public GameObject spawner; // Assign your enemy spawner here

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        currentLives = startingLives;
        UpdateUI();

        if (baseSprite != null)
            originalColor = baseSprite.color;

        if (restartButton != null)
            restartButton.GetComponent<Button>().onClick.AddListener(RestartGame);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void LoseLife()
    {
        if (gameOver) return;

        currentLives--;
        UpdateUI();

        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        FlashBase();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + currentLives;
    }

    void FlashBase()
    {
        if (baseSprite != null)
            StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        baseSprite.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        baseSprite.color = originalColor;
    }

    void GameOver()
    {
        gameOver = true;
        Debug.Log("Game Over!");

        // Play death sound
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Disable buttons
        foreach (Button b in directionButtons)
        {
            b.interactable = false;
        }

        // Stop spawning
        if (spawner != null)
            spawner.SetActive(false);

        // Show Game Over panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
}
