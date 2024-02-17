using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollideableReact
{
    [SerializeField] private DecorationSpawner[] decorationSpawners;
    
    private List<ObstacleDecoration> obstacleDecorations = new();

    public void SpawnDecorations()
    {
        foreach (var decorationSpawner in decorationSpawners)
        {
            decorationSpawner.SpawnDecorations();
            if(decorationSpawner.CurrentDecoration.TryGetComponent<ObstacleDecoration>(out var decorationComponent))
            {
                obstacleDecorations.Add(decorationComponent);
            }

        }
    }

    public void PlayCollisionFeedBack(Collider collider)
    {
        ObstacleDecoration obstacleDecoration = FindNearestDecoration(collider);
        if(obstacleDecoration != null)
        {
            obstacleDecoration.PlayCollisionFeedBack();
        }
        
    }

    private ObstacleDecoration FindNearestDecoration(Collider collider)
    {
        float minDistX = Mathf.Infinity;
        ObstacleDecoration obstacleDecoration = null;

        foreach (var decoration in obstacleDecorations)
        {
            float colliderDecorationX = collider.bounds.center.x;
            float obstacleDecorationX = decoration.transform.position.x;

            float currentDistX = Mathf.Abs(collider.bounds.center.x - decoration.transform.position.x);

            if(currentDistX < minDistX)
            {
                minDistX = currentDistX;
                obstacleDecoration = decoration;
            }
            
        }

        return obstacleDecoration;
    }

    public virtual void PlayCollisionFeedBack(in IPlayerInfo playerInfo)   
    {
        PlayCollisionFeedBack(playerInfo.objectCollided);
        playerInfo.player.OnCollidedWithObstacle();

    }

}
