using UnityEngine;
using UnityEngine.AI;

public class AIFootsteps : MonoBehaviour
{
    public AudioSource footstepAudioSource;
    public AudioClip[] footstepSounds;
    public float stepInterval = 0.5f;
    private NavMeshAgent agent;
    private float nextStepTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (footstepAudioSource == null)
        {
            footstepAudioSource = GetComponent<AudioSource>();
        }
        footstepAudioSource.spatialBlend = 1.0f; 
        footstepAudioSource.maxDistance = 10f; 
    }

    void Update()
    {
        if (agent.velocity.magnitude > 0.1f && Time.time >= nextStepTime)
        {
            PlayFootstep();
            float speedFactor = Mathf.Lerp(1f, 0.25f, (agent.speed - 3.5f) / (14f - 3.5f));
            nextStepTime = Time.time + stepInterval * speedFactor; 
        }
    }

    void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            footstepAudioSource.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            footstepAudioSource.Play();
        }
    }
}
