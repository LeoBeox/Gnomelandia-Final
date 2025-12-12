using UnityEngine;

public class PlantingPlot : MonoBehaviour
{
    [Header("Plants to Grow")]
    public GameObject wallPrefab;
    public GameObject turretPrefab;

    [Header("Settings")]
    public KeyCode plantWallKey = KeyCode.G;
    public KeyCode plantTurretKey = KeyCode.T;

    private bool _playerInRange = false;
    private bool _isPlanted = false;
    private GameObject _currentPlant;

    void Update()
    {
        // Only allow planting if player is close AND nothing is planted yet
        if (_playerInRange && !_isPlanted)
        {
            if (Input.GetKeyDown(plantWallKey))
            {
                Plant(wallPrefab);
            }
            else if (Input.GetKeyDown(plantTurretKey))
            {
                Plant(turretPrefab);
            }
        }
    }

    void Plant(GameObject plantPrefab)
    {
        Quaternion correctRotation = transform.rotation * Quaternion.Euler(0, 90, 0);

        
        _currentPlant = Instantiate(plantPrefab, transform.position, correctRotation);

        _isPlanted = true;
        Debug.Log("Planted!");
    }
    
    // Detect when Player walks onto the plot
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}