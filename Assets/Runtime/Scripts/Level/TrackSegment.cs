using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private ObstacleSpawner[] obstacleSpawners;
    [SerializeField] private DecorationSpawner decorationSpawner;
    [SerializeField] private PickupLineSpawner[] pickupLineSpawner;

    public Transform Start => start;
    public Transform End => end;

    public void SpawnPickups()
    {
        if(pickupLineSpawner.Length < 1)  return;
        var lineSpawner = pickupLineSpawner[Random.Range(0, pickupLineSpawner.Length)];

        Vector3[] skipPositions = new Vector3[obstacleSpawners.Length];

        for (int i = 0; i < skipPositions.Length; i++)
        {
            skipPositions[i] = obstacleSpawners[i].transform.position;
        } 

        lineSpawner.DecidingPickup(skipPositions);

    }
    
    public void SpawnObstacle()
    {
        foreach (var currentObstacleSpawner in obstacleSpawners)
        {
            currentObstacleSpawner.SpawnObstacle();
        }

    }

    public void SpawnDecoration()
    {
        decorationSpawner.SpawnDecorations();
    }

}
