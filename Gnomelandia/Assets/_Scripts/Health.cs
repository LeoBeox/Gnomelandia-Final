using JetBrains.Annotations;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int maxHealth;

    private int CurrentHealth;
 
    void Start()
    {
        CurrentHealth = maxHealth;
    }

    void Update()
    {   
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void TakeDamage(int damageAmount)
    {  
        CurrentHealth -= damageAmount;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
