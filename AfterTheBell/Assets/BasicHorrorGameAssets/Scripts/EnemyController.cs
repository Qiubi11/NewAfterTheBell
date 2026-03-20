using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float idleTime = 2f;
    public float walkSpeed = 2f; 
    public float chaseSpeed = 4f; 
    public float sightDistance = 10f;
    public AudioClip idleSound, walkingSound, chasingSound;

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private float idleTimer = 0f;
    private Transform player;
    private AudioSource audioSource;

    private enum EnemyState { Idle, Walk, Chase }
    private EnemyState currentState = EnemyState.Idle;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // This finds your player. Make sure your player object is tagged "Player"!
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        if (waypoints != null && waypoints.Length > 0) SetDestinationToWaypoint();
    }

    private void Update()
    {
        if (agent == null || player == null) return;

        switch (currentState)
        {
            case EnemyState.Idle:
                idleTimer += Time.deltaTime;
                if (idleTimer >= idleTime) NextWaypoint();
                CheckForPlayerDetection();
                break;
            case EnemyState.Walk:
                if (agent.remainingDistance <= agent.stoppingDistance) currentState = EnemyState.Idle;
                CheckForPlayerDetection();
                break;
            case EnemyState.Chase:
                agent.SetDestination(player.position);
                if (Vector3.Distance(transform.position, player.position) > sightDistance)
                {
                    currentState = EnemyState.Walk;
                    agent.speed = walkSpeed;
                }
                break;
        }
    }

    private void CheckForPlayerDetection()
    {
        if (player == null) return;
        if (Vector3.Distance(transform.position, player.position) < sightDistance)
        {
            currentState = EnemyState.Chase;
            agent.speed = chaseSpeed;
        }
    }

    private void NextWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        SetDestinationToWaypoint();
    }

    private void SetDestinationToWaypoint()
    {
        if (agent == null || waypoints == null || waypoints.Length == 0) return;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentState = EnemyState.Walk;
        agent.speed = walkSpeed;
    }

    // THIS IS THE FIX:
    private void OnDrawGizmos()
    {
        // We only draw the line IF the game is running AND the player exists.
        if (Application.isPlaying && player != null)
        {
            Gizmos.color = (currentState == EnemyState.Chase) ? Color.red : Color.green;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}