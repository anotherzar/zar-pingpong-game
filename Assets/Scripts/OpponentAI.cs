using UnityEngine;

public class Opponent_AI : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject ball;

    [Header("AI Configuration")]
    [SerializeField] float baseSpeed = 12f;
    [Range(0f, 1f)] [SerializeField] float currentDifficulty = 0.5f; // Difficulty (0 sampe 1)
    [Range(0f, 1f)] [SerializeField] float noobChance = 0.05f;

    private bool isNoobing = false;
    private float noobTimer = 0f;
    private float checkInterval = 1f;
    private float nextCheck = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = GameObject.FindGameObjectWithTag("ball");
    }

    void FixedUpdate()
    {
        if (ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("ball");
            return;
        }

        // Panggil fungsi utama AI dengan parameter difficulty
        HandleAIBehavior(currentDifficulty);
    }

    void HandleAIBehavior(float difficulty)
    {
        // 1. Kalkulasi Kecepatan berdasarkan difficulty
        // Semakin tinggi difficulty, semakin dekat ke baseSpeed maksimal
        float calculatedSpeed = baseSpeed * (0.4f + (difficulty * 0.6f));

        // 2. Logic tiba tiba nanti dia noob
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkInterval;
            if (Random.value < noobChance)
            {
                isNoobing = true;
                noobTimer = Time.time + Random.Range(0.5f, 1.5f);
            }
        }

        if (isNoobing)
        {
            if (Time.time > noobTimer)
            {
                isNoobing = false;
            }
            else
            {
                // Speed dikurangi drastis kalau lagi noob
                calculatedSpeed *= 0.15f;
            }
        }

        // 3. Eksekusi Pergerakan
        MoveTowardsBall(calculatedSpeed);
    }

    void MoveTowardsBall(float speed)
    {
        float ballY = ball.transform.position.y;
        float aiY = transform.position.y;
        float threshold = 0.5f;

        if (ballY > aiY + threshold)
        {
            rb.linearVelocity = new Vector2(0, speed);
        }
        else if (ballY < aiY - threshold)
        {
            rb.linearVelocity = new Vector2(0, -speed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    // Fungsi helper
    public void SetDifficulty(float newDifficulty)
    {
        currentDifficulty = Mathf.Clamp01(newDifficulty);
    }
}

