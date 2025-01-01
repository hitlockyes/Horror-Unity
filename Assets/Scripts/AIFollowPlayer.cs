using System;
using UnityEngine;
using UnityEngine.AI;

public class AIFollowPlayer : MonoBehaviour
{
    public Camera ChaseFOV;
    public Transform player;
    public float detectionRange = 20f;
    public float fieldOfViewAngle = 180f;
    public LayerMask obstacleLayer;
    [SerializeField] private NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public float fovTransitionSpeed = 2f;
    public float targetFov;

    public AudioSource footstepAudioSource;
    public AudioClip footstepSound; 
    public float stepInterval = 0.5f;

    private int currentWaypointIndex = 0;
    private bool isPatrolling = true;
    private float nextStepTime = 0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (waypoints.Length > 0)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }

        targetFov = ChaseFOV.fieldOfView;

        footstepAudioSource = footstepAudioSource ?? GetComponent<AudioSource>();
        footstepAudioSource.spatialBlend = 1.0f; 
        footstepAudioSource.maxDistance = 10f; 
        footstepAudioSource.clip = footstepSound; 
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            FollowPlayer();
            targetFov = 120f;
            ChaseFOV.fieldOfView = Mathf.Lerp(ChaseFOV.fieldOfView, targetFov, fovTransitionSpeed * Time.deltaTime);
            isPatrolling = false;
        }
        else
        {
            targetFov = 75f;
            isPatrolling = true;
            Patrol();
        }

        PlayFootsteps();
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= detectionRange)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= fieldOfViewAngle / 2)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, detectionRange, obstacleLayer))
                {
                    if (hit.transform == player)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void FollowPlayer()
    {
        navMeshAgent.SetDestination(player.transform.position);
        navMeshAgent.speed = 14f;
        navMeshAgent.angularSpeed = 600f;
        navMeshAgent.acceleration = 25f;
        navMeshAgent.isStopped = false;
    }

    void Patrol()
    {
        navMeshAgent.speed = 3.5f;
        navMeshAgent.angularSpeed = 120f;
        navMeshAgent.acceleration = 25f;
        navMeshAgent.isStopped = false;

        if (isPatrolling && waypoints.Length > 0)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
            }
        }
    }

    void PlayFootsteps()
    {
        if (navMeshAgent.velocity.magnitude > 0.1f && Time.time >= nextStepTime)
        {
            float speedFactor = CanSeePlayer() ? 0.5f : 1f; 
            nextStepTime = Time.time + stepInterval * speedFactor; 

            footstepAudioSource.Play();
        }
    }
}
