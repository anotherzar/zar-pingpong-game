using UnityEngine;

public class PongBall : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 3f;
    float force = 100f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Meluncurkan bola ke arah acak (dipanggil oleh GameManager).
    public void LaunchBall() {
        // Reset speed nya
        rb.linearVelocity = Vector2.zero;
        
        // nentuin arah awal secara acak
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector2 direction = new Vector2(x, y);

        // ngasih gaya buat ngeluncurin bola
        rb.AddForce(direction * speed * force);
    }

    public void StopBall() {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        transform.position = Vector3.zero;
    }
}