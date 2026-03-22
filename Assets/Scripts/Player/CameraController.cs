using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem speedUpParticleSystem;
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 120f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifier = 5f;
    CinemachineCamera cinemachineCamera;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }
    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));

        if (speedAmount > 0f)
        {
            speedUpParticleSystem.Play();
        }
        else
        {
            speedUpParticleSystem.Stop();
        }
    }

    IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV+ speedAmount * zoomSpeedModifier, minFOV, maxFOV);
        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }
        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
