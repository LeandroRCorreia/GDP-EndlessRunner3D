using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstaclePrefabOptions;

    private Obstacle currentObstacle;

    public void SpawnObstacle()
    {
        if(obstaclePrefabOptions.Length > 0)
        {
            Obstacle prefab = obstaclePrefabOptions[Random.Range(0, obstaclePrefabOptions.Length)];
            currentObstacle = Instantiate(prefab, transform);
            currentObstacle.transform.localPosition = Vector3.zero;
            currentObstacle.transform.rotation = Quaternion.identity;
            currentObstacle.SpawnDecorations();
        }
        else
        {
            Debug.LogWarning("Does not exist obstacle options, put some options on array");
        }

    }
}
