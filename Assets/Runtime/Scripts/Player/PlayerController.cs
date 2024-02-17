using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private PlayerAudioController playerAudio;    

    [Header("Movement")]
    [SerializeField] private float horizontalSpeed = 15;
    [SerializeField] public float ForwardSpeed {get; set;} = 10;
    [SerializeField] private float laneDistanceX = 4;

    [field: SerializeField] public JumpParams JumpParams {get; private set;}

    [Header("Roll")]

    [SerializeField] private float rollDistanceZ = 5;
    [SerializeField] private GameObject regularCollider;
    [SerializeField] private GameObject rollCollider;

    public event Action PlayerDeathEvent;

    Vector3 initialPosition;

    float targetPositionX;

    private float rollStartZ;


    public bool IsJumping { get; private set; }

    public bool IsInvincible {get; set;}

    public bool IsDead {get; private set;} = false;

    public bool IsRolling { get; private set; }

    public float JumpDuration => JumpParams.jumpDistanceZ / ForwardSpeed;

    public float RollDuration => rollDistanceZ / ForwardSpeed;
    float jumpStartZ;

    private float LeftLaneX => initialPosition.x - laneDistanceX;
    private float RightLaneX => initialPosition.x + laneDistanceX;

    private bool CanJump => !IsJumping;
    private bool CanRoll => !IsRolling;
    public int TravalledDistance => Mathf.RoundToInt(transform.position.z - initialPosition.z);

    void Awake()
    {
        initialPosition = transform.position;
        StopRoll();
    }

    void Update()
    {
        if(!IsDead)
        {
            ProcessInput();
        }

        Vector3 position = transform.position;

        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        position.z = ProcessForwardMovement();
        ProcessRoll();

        transform.position = position;
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetPositionX += laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetPositionX -= laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.W) && CanJump)
        {
            StartJump();
        }
        if (Input.GetKeyDown(KeyCode.S) && CanRoll)
        {
            StartRoll();
        }

        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RightLaneX);
    }

    private float ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, Time.deltaTime * horizontalSpeed);
    }

    private float ProcessForwardMovement()
    {
        return transform.position.z + ForwardSpeed * Time.deltaTime;
    }

    private void StartJump()
    {
        IsJumping = true;
        jumpStartZ = transform.position.z;
        playerAudio.PlayJumpAudioClip();
        StopRoll();
    }

    private void StopJump()
    {
        IsJumping = false;
    }

    private float ProcessJump()
    {
        float deltaY = 0;
        if (IsJumping)
        {
            float jumpCurrentProgress = transform.position.z - jumpStartZ;
            float jumpPercent = jumpCurrentProgress / JumpParams.jumpDistanceZ;
            if (jumpPercent >= 1)
            {
                StopJump();
            }
            else
            {   
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * JumpParams.jumpHeightY;
            }
        }
        float targetPositionY = initialPosition.y + deltaY;
        return Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * JumpParams.jumpLerpSpeed);
    }

    private void ProcessRoll()
    {
        if (IsRolling)
        {
            float percent = (transform.position.z - rollStartZ) / rollDistanceZ;
            if (percent >= 1)
            {
                StopRoll();
            }
        }
    }

    private void StartRoll()
    {
        playerAudio.PlayRollAudioClip();
        rollStartZ = transform.position.z;
        IsRolling = true;
        regularCollider.SetActive(false);
        rollCollider.SetActive(true);

        StopJump();
    }

    private void StopRoll()
    {
        IsRolling = false;
        regularCollider.SetActive(true);
        rollCollider.SetActive(false);
    }
    
    public void OnCollidedWithObstacle()
    {
        if(!IsInvincible)
        {
            Die();

        }

    }

    private void Die()
    {
        IsDead = true;
        targetPositionX = transform.position.x;
        ForwardSpeed = 0;
        horizontalSpeed = 0;
        StopRoll();
        StopJump();
        regularCollider.SetActive(false);
        rollCollider.SetActive(false);
        PlayerDeathEvent?.Invoke();

    }

}
