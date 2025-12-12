using UnityEngine;
using UnityEngine.AI; 

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameObject _target;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _target = GameObject.Find("GoldenAppleTree");

        if (_target != null)
        {
            // Tell the agent to go to the tree's position
            _agent.SetDestination(_target.transform.position);
        }
        else
        {
            Debug.LogError("Enemy cannot find Target.");
        }
    }

    void Update()
    {
        // _agent.SetDestination(_target.transform.position);

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("GoldenAppleTree"))
        {
            Health treeHealth = other.GetComponent<Health>();
            
            if (treeHealth != null)
            {
                // Deal damage to the tree
                treeHealth.TakeDamage(1); 
                
                Destroy(gameObject);
            }
        }
    }
}