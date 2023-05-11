using UnityEngine;
using Cinemachine;
using System.Collections;

public class ThirdPersonShooter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform gunTip;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;

    [SerializeField] private float zoomFOV = 30f;
    [SerializeField] private float zoomDuration = 0.2f;
    [SerializeField] private float maxLeftRightAngle = 10f;
    [SerializeField] private float maxUpDownAngle = 10f;
    [SerializeField] private float moveSpeed = 5f;

    private bool isADS = false;
    private float originalFOV;
    private Quaternion originalRotation;
    private Quaternion targetRotation;


    private bool isShooting = false;
    private void Start()
    {
        originalFOV = virtualCamera.m_Lens.FieldOfView;
        originalRotation = virtualCamera.transform.localRotation;
        targetRotation = originalRotation;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isShooting = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            isShooting = false;

        }
        if (Input.GetButtonDown("Fire2"))
        {
            isADS = true;
            StartCoroutine(ZoomIn());
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isADS = false;
            StartCoroutine(ZoomOut());
        }
    }

    private void FixedUpdate()
    {
        if (isShooting)
        {
            Shoot();
        }

        if (isADS)
        {
            // Calculate rotation based on input
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Vector3 eulerRotation = targetRotation.eulerAngles;
            eulerRotation.y += mouseX * moveSpeed;
            eulerRotation.x = Mathf.Clamp(eulerRotation.x - mouseY * moveSpeed, -maxUpDownAngle, maxUpDownAngle);
            eulerRotation.z = Mathf.Clamp(eulerRotation.z - mouseX * moveSpeed, -maxLeftRightAngle, maxLeftRightAngle);
            targetRotation = Quaternion.Euler(eulerRotation);
            virtualCamera.transform.localRotation = Quaternion.Lerp(virtualCamera.transform.localRotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    private void Shoot()
    {
        // Spawn bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);

        // Add force to the bullet in the forward direction
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = gunTip.forward * bulletSpeed;

        // Play gunshot sound
        // (You can implement this using an AudioSource component on the gun or elsewhere)

        // Shake camera
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1f;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 10f;
        Invoke("StopCameraShake", 0.1f);
    }

    private void StopCameraShake()
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
    }

    private IEnumerator ZoomIn()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / zoomDuration;
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(originalFOV, zoomFOV, t);
            yield return null;
        }
    }

    private IEnumerator ZoomOut()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / zoomDuration;
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(zoomFOV, originalFOV, t);
            yield return null;
        }
    }
}
