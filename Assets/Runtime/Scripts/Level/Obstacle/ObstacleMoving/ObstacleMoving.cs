using UnityEngine;

public class ObstacleMoving : Obstacle
{
    [SerializeField] 
    private float laneDistanceX = 1.5f;
    [SerializeField]
    private float speedX = 2;

    private float positionT = 0;

    public float SideToSideMoveTime => 1.0f / speedX;

    void Update()
    {
        positionT += speedX * Time.deltaTime;
        var lanePositionX = (Mathf.PingPong(positionT, 1f) - 0.5f) * laneDistanceX * 2;

        transform.position = new Vector3(lanePositionX, transform.position.y, transform.position.z);
    }

    public override void PlayCollisionFeedBack(in IPlayerInfo playerInfo)
    {
        base.PlayCollisionFeedBack(in playerInfo);
        enabled = false;

    }

}
