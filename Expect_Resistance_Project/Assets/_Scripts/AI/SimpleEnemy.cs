using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using noGame.Characters;
using noGame.EnemyBehaviour;

[RequireComponent(typeof(AICharacterController2D))]
public class SimpleEnemy : MonoBehaviour
{
    [Header("Input Generation Options")]
    [SerializeField][Tooltip("Distance below which enemy will no longer approach edge or obstacle in horizontal axis")] float stopMovementDistance;
    [SerializeField] float horizontalInputBoost;
    [SerializeField][Range(-1,1)] float initailPatrollDirection;
    [SerializeField][Tooltip("Break time before changing patrolling direction")] float breakTime;
    [SerializeField][Tooltip("Break time before changing patrolling direction")] float minJumpDelay;

    [Header("Detection System")]
    [SerializeField] float killRadius;
    [SerializeField] float detectionRadius;
    [SerializeField][Range(0, 90)][Tooltip("Above this angle (both up and down) enemy will not see player")] float detectionMaxAngle;
    [SerializeField][Tooltip("Time in seconds in which enemy will react after continously seeing player (increasing detection level)")] float detectionLevelThreashold;
    [SerializeField][Range(0, 1)][Tooltip("Rate at which enemy will loose detection level when not seeing player")] float detectionLevelDecreaseFactor;
    [SerializeField] int detectionMask;
    // note: Detection level is defined in PatrollingState since it's not used elsewhere. Consider moving it to this class if more states would like to detect player.


    float horizontalInput;

    internal EnemyState state;
    internal PatrollingState patrollingState;
    internal ChaseState chaseState;
    internal IdleState idleState;

    AICharacterController2D controller;
    GameObject targetObject;
    Collider2D[] detectedObjects = new Collider2D[10];

    public float HorizontalInputBoost { get => horizontalInputBoost; }
    public float KillRadius { get => killRadius; set => killRadius = value; }
    public float DetectionRadius { get => detectionRadius;}
    internal GameObject TargetObject { get => targetObject; set => targetObject = value; }
    internal float StopMovementDistance { get => stopMovementDistance; }
    internal float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
    public float DetectionLevelThreashold { get => detectionLevelThreashold; }
    public float DetectionLevelDecreaseFactor { get => detectionLevelDecreaseFactor; }
    public int DetectionMask { get => detectionMask; }
    public float MinJumpDelay { get => minJumpDelay; set => minJumpDelay = value; }
    public float BreakTime { get => breakTime; set => breakTime = value; }

    void Awake()
    {
        controller = GetComponent<AICharacterController2D>();
        chaseState = new ChaseState(this);
        patrollingState = new PatrollingState(this,initailPatrollDirection);
        idleState = new IdleState(this);
        detectionMask = ~LayerMask.GetMask("Enemy");
    }

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
        ChangeState(patrollingState);
        //ChangeState(chaseState);
    }

    void Update()
    {
        state.Update();
        controller.OnAIHorizontal(horizontalInput);
    }

    internal void KillCheck()
    {
        Physics2D.OverlapCircleNonAlloc(gameObject.transform.position, killRadius, detectedObjects);
        for (int i = 0; i < detectedObjects.Length; i++)
        {
            if (detectedObjects[i] == null) break;
            if (detectedObjects[i].gameObject.CompareTag("Player")) // TO DO: use layers?
            {
                //detectedObjects[i].gameObject.GetComponent<PlayerCharacterController2D>().enabled = false;
                //ChangeState(idleState);
                Debug.Log("Player killed");
            }
        }
    }

    internal void PhaseDownInput()
    {
        controller.OnAIDownKey(true);
    }

    internal void JumpInput(bool active)
    {
        controller.OnAIJump(active);
    }

    internal bool IsFalling()
    {
        return controller.IsFalling;
    }

    internal bool IsGrounded()
    {
        return gameObject.GetComponent<CharacterController2D>().isGrounded;
    }

    internal void OnPlayerDetected()
    {
        ChangeState(chaseState);
    }

    private void ChangeState(EnemyState state)
    {
        this.state = state;
        state.Start();
    }
}
