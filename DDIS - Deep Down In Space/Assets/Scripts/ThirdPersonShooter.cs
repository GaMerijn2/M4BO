using UnityEngine;
using Cinemachine;

public class ThirdPersonShooter : MonoBehaviour
{
    public Transform playerTransform;
    public Transform gunTransform;
    public CinemachineVirtualCamera virtualCamera;

    private CinemachineComposer composer;

    private void Start()
    {
        // Get the composer component from the virtual camera
        composer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
    }

    private void Update()
    {
        // Update the camera position and rotation based on the player and gun positions
        Vector3 targetPos = playerTransform.position + playerTransform.forward * -2f + Vector3.up * 1.5f;
        Quaternion targetRot = Quaternion.LookRotation(gunTransform.position - targetPos, playerTransform.up);

        transform.position = targetPos;
        transform.rotation = targetRot;

        // Adjust the composer's frame height to simulate an over-the-shoulder view
        composer.m_TrackedObjectOffset = new Vector3(0f, 1.5f, 0f);

        // Handle shooting input
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Perform shooting logic here
        Debug.Log("Shoot!");
    }
}
