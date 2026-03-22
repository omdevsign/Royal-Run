using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource boulderSmashAudioSource;
    [SerializeField] float shakeModifier = 10f;
    [SerializeField] float collisionCooldown = 1f;
    CinemachineImpulseSource cinemachineImpulseSource;
    float collisionTimer = 1f;
    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }
    void Update()
    {
        collisionTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collisionTimer < collisionCooldown) return;
        FireImpulse();
        CollisionFX(collision);
        collisionTimer = 0f;
    }

    void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }
    void CollisionFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        boulderSmashAudioSource.Play();
    }
}
