using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public int damage = 3; // Shovel hits harder than magic

    void OnTriggerEnter(Collider other)
    {
        Health targetHealth = other.GetComponent<Health>();
        
        if (!other.CompareTag("Player"))
        {
            targetHealth.TakeDamage(damage);
            Debug.Log("Whack!");
        }
    }
}

