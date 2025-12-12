using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float duration = 5f; // How long the boost lasts (seconds)

    void OnTriggerEnter(Collider other)
    {
        // Check if the thing colliding with the powerup is the Player
        if (other.CompareTag("Player"))
        {
            FPSInput playerScript = other.GetComponent<FPSInput>();

            if (playerScript != null)
            {
                playerScript.ActivateSpeedBoost(duration);
                
                Destroy(gameObject);
            }
        }
    }
}