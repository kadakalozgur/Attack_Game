using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public Camera playerCamera;

    public float mouseSensitivity = 1.5f;

    private float yaw = 0f;
    private float pitch = 20f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -30f, 50f);

        Vector3 baseOffset = new Vector3(0, 25f, -40f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 trackCamera = rotation * baseOffset;

        playerCamera.transform.position = player.position + trackCamera;

        playerCamera.transform.LookAt(player.position + Vector3.up * 22f);

        Vector3 lookDirection = playerCamera.transform.position - player.position;
        lookDirection.y = 0;

        if (lookDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-lookDirection);
            player.rotation = Quaternion.Slerp(player.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
