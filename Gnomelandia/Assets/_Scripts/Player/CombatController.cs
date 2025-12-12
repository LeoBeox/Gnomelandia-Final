using UnityEngine;
using System.Collections; 

public class PlayerCombat : MonoBehaviour
{
    [Header("Magic Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    
    [Header("Melee Settings")]
    public GameObject meleeHitbox; 
    public float attackDuration = 0.2f; 

    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            ShootMagic();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SwingShovel());
        }
    }

    void ShootMagic()
    {
        // Spawn bullet at the firePoint location
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    IEnumerator SwingShovel()
    {
        meleeHitbox.SetActive(true); // Turn on the hitbox
        
        // Wait for a split second
        yield return new WaitForSeconds(attackDuration);
        
        meleeHitbox.SetActive(false); // Turn it off
    }
}