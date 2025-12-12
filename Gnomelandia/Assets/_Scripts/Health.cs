using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using System;

public class Health : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int maxHealth;

    [NonSerialized] public int CurrentHealth;
 
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

    public void TakeDamage(int damageAmount)
    {  
        CurrentHealth -= damageAmount;
    }

    private void Die()
    {
        if (gameObject.CompareTag("GoldenAppleTree"))
        {
            GameManager.Instance.GameOver();
        }
        
        if (gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.AddScore(10);
        }
        
        Destroy(gameObject);

    }
}
