using UnityEngine;

public class PickupLineSpawner : MonoBehaviour
{
    [Header("Fruits")]
    [SerializeField] private Cherrie pickupCherrie;
    [SerializeField] private Peanut pickupPeanut;
    [SerializeField] [Range(0.0f, 1)] private float pickupPeanutChance = 0.1f;

    [Header("PowerUps")]
    
    [SerializeField] [Range(0f, 1f)] private float powerUpChance = 0.1f;
    [SerializeField] Pickup[] powerUpPrefabs;

    [Space]

    [Header("Pickup Line Configuration")]
    [SerializeField] private Transform pickupsContainer;
    [SerializeField] private Transform startSpawn;
    [SerializeField] private Transform endSpawn;
    [SerializeField] [Range(0.1f, 5f)] private float spaceBeetwenPickups;

    private Vector3 StartSpawnPosition => startSpawn.localPosition;
    private Vector3 EndSpawnPosition => endSpawn.localPosition;

    public void DecidingPickup(Vector3[] skipPosition)
    {
        if(Random.value <= powerUpChance)
        {
            SpawnPowerUpTrigger(skipPosition);
        }
        else
        {
            SpawnPickupLine(skipPosition);
        }

    }

    private void SpawnPowerUpTrigger(Vector3[] skipPositions)
    {
        var powerUpInstance = Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], pickupsContainer);
        powerUpInstance.transform.position = transform.position;

    }

    private void SpawnPickupLine(Vector3[] skipPositions)
    {
        Vector3 currentPickupPosition = startSpawn.position;

        while (currentPickupPosition.z <= endSpawn.position.z)
        {
            if(!ShouldSkipPosition(currentPickupPosition, skipPositions))
            {
                Pickup pickupInstance = Instantiate(GetFruitPickup(), pickupsContainer);
                pickupInstance.transform.position = currentPickupPosition;
            }

            currentPickupPosition.z += spaceBeetwenPickups;
        }
    }

    private Pickup GetFruitPickup()
    {
        return Random.value < pickupPeanutChance ? pickupPeanut : pickupCherrie;
    }

    private bool ShouldSkipPosition(Vector3 currentSpawnPosition, Vector3[] skipPositions)
    {

        foreach (var skipPosition in skipPositions)
        {
            float skipStart = skipPosition.z - spaceBeetwenPickups * 0.5f;
            float skipEnd = skipPosition.z + spaceBeetwenPickups * 0.5f;

            if(currentSpawnPosition.z >= skipStart && currentSpawnPosition.z <= skipEnd)
            {
                return true;
            }
            
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        float currentPositionZ = StartSpawnPosition.z;
        
        while(currentPositionZ <= EndSpawnPosition.z)
        {
            const float sizeCube = 1;
            Vector3 targetPosition = new(transform.position.x, transform.position.y, currentPositionZ);

            Gizmos.color = Color.green;
            Gizmos.DrawCube(targetPosition, Vector3.one * sizeCube);

            currentPositionZ += spaceBeetwenPickups;
        }
        
    }

}
