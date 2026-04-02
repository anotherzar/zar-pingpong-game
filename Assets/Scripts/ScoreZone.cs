using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    GameManager manager;

    private void Start() {
        manager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("ball")) {
            // Jika bola memasuki area belakang pemain, lawan mendapatkan poin.
            if (gameObject.name == "OutZone_Player") {
                manager.opponentWin.Invoke();
            } 
            // Jika bola memasuki area belakang lawan, pemain mendapatkan poin.
            else if (gameObject.name == "OutZone_Opponent") {
                manager.playerWin.Invoke();
            }
        }
    }
}