using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementVector;
    [SerializeField] private float speed = 15f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // make sure Rigidbody2D is present to avoid null reference errors.
        if (rb == null) {
            Debug.LogError("Error: Rigidbody2D component missing from player object!");
        }
    }

    void Update() 
    {
        // Menerima input vertikal dari pemain.
        float moveInput = Input.GetAxisRaw("Vertical");
        movementVector = new Vector2(0, moveInput);
    }

    void FixedUpdate() 
    {
        if (rb != null) {
            rb.linearVelocity = new Vector2(0, movementVector.y * speed);
        }
    }
}