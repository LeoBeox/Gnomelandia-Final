using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] float _speed = 11.0f;
    [SerializeField] int _damage = 1;
    [SerializeField] float _lifetime = 5.0f;
    

    void Start()
    {
        
        Destroy(gameObject, _lifetime);

    }

    
    void Update()
    {
        
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        Health targetHealth = other.GetComponent<Health>();

        if (!other.CompareTag("Player"))
        {
            targetHealth.TakeDamage(_damage);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlaySFX(GameManager.Instance.enemyHitClip);
            }
            
            Destroy(gameObject);
        }

        
    }
}
