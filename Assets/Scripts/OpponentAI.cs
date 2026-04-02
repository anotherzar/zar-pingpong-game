using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent_AI : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject ball;
    
    [SerializeField] float speed = 12f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Mencari objek bola berdasarkan tag.
        ball = GameObject.FindGameObjectWithTag("ball");
    }

    void FixedUpdate()
    {
        // Jika bola tidak ditemukan, cari kembali untuk menghindari kesalahan referensi.
        if (ball == null)
        {
            ball = GameObject.FindGameObjectWithTag("ball");
            return;
        }

        // AI mengikuti posisi sumbu Y bola.
        // Gunakan toleransi untuk mencegah gerakan bergetar saat posisi sejajar.
        if (ball.transform.position.y > transform.position.y + 0.5f)
        {
            rb.linearVelocity = new Vector2(0, speed);
        }
        else if (ball.transform.position.y < transform.position.y - 0.5f)
        {
            rb.linearVelocity = new Vector2(0, -speed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}