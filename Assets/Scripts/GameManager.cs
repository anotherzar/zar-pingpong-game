using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement; // Diperlukan untuk manajemen scene

public class GameManager : MonoBehaviour
{
    [Header("Skor")]
    public int playerScore = 0;
    public int opponentScore = 0;

    [Header("Hubungkan Teks UI")]
    [SerializeField] private TextMeshProUGUI playerText; 
    [SerializeField] private TextMeshProUGUI opponentText;

    [Header("UI Menus")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject scorePanel; // Panel UI untuk menampilkan skor
    [SerializeField] private TextMeshProUGUI winnerText;
    
    [Header("Win/Loss Messages")]
    [SerializeField] private string winMessage = "SELAMAT! KAMU MENANG!";
    [SerializeField] private string lossMessage = "MAAF! KAMU KALAH!";
    
    [Header("Game Settings")]
    [SerializeField] private int winScore = 5;
    private bool isGameActive = false;

    [Header("Events")]
    public UnityEvent playerWin;
    public UnityEvent opponentWin;

    private void Start()
    {
        UpdateUI(); 
        ShowMenu(); // Menampilkan menu utama saat memulai
    }

    private void OnEnable()
    {
        playerWin.AddListener(AddPlayerScore);
        opponentWin.AddListener(AddOpponentScore);
    }

    private void OnDisable()
    {
        playerWin.RemoveAllListeners();
        opponentWin.RemoveAllListeners();
    }

    public void StartGame()
    {
        isGameActive = true;
        
        if (menuPanel != null) menuPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (scorePanel != null) scorePanel.SetActive(true); 

        ResetScores();

        // Memberikan jeda sebelum meluncurkan bola untuk pengalaman pengguna yang lebih baik.
        StartCoroutine(DelayLaunchBall());
    }

    public void AddPlayerScore()
    {
        if (!isGameActive) return;

        playerScore++;
        UpdateUI();

        if (playerScore >= winScore) {
            GameOver(winMessage);
        } else {
            ResetBall();
        }
    }

    public void AddOpponentScore()
    {
        if (!isGameActive) return;

        opponentScore++;
        UpdateUI();

        if (opponentScore >= winScore) {
            GameOver(lossMessage);
        } else {
            ResetBall();
        }
    }

    void UpdateUI()
    {
        if (playerText != null) playerText.text = playerScore.ToString();
        if (opponentText != null) opponentText.text = opponentScore.ToString();
    }

    void ShowMenu()
    {
        isGameActive = false;
        if (menuPanel != null) menuPanel.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (scorePanel != null) scorePanel.SetActive(false); 
        
        GameObject ball = GameObject.FindGameObjectWithTag("ball");
        if (ball != null) {
            ball.SetActive(true);
            ball.GetComponent<PongBall>().StopBall();
        }
    }

    void GameOver(string winnerName)
    {
        isGameActive = false;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (scorePanel != null) scorePanel.SetActive(false); 
        if (winnerText != null) winnerText.text = winnerName;

        GameObject ball = GameObject.FindGameObjectWithTag("ball");
        if (ball != null) ball.GetComponent<PongBall>().StopBall();
    }

    void ResetScores()
    {
        playerScore = 0;
        opponentScore = 0;
        UpdateUI();
    }

    public void RestartGame()
    {
        // Memuat ulang scene saat ini untuk mengatur ulang kondisi permainan.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ResetBall()
    {
        StartCoroutine(DelayLaunchBall());
    }

    // Coroutine untuk menunda peluncuran bola agar transisi lebih halus.
    private System.Collections.IEnumerator DelayLaunchBall()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("ball");
        if (ball != null)
        {
            ball.GetComponent<PongBall>().StopBall();
        }

        yield return new WaitForSeconds(1f); // Menunggu durasi yang ditentukan sebelum meluncurkan bola.

        if (ball != null && isGameActive)
        {
            ball.GetComponent<PongBall>().LaunchBall();
        }
    }
}