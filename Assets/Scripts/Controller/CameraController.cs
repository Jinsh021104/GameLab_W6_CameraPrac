using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public GameObject mainCamera;
    public GameObject aimCamera;

    private float _cameraRotationXSpeed;
    private float _cameraRotationYSpeed;
    private float _mouseSensitive = 10f;
    private float _padSensitive = 100f;

    public void OnLook(InputAction.CallbackContext context)
    {
        _cameraRotationXSpeed = context.ReadValue<Vector2>().x * (context.control.device.name.Equals("Mouse") ? _mouseSensitive : _padSensitive);
        _cameraRotationYSpeed = context.ReadValue<Vector2>().y * (context.control.device.name.Equals("Mouse") ? _mouseSensitive : _padSensitive);
    }

    public void OnAiming(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
        }
        if (context.canceled)
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
        }
    }

    private void Update()
    {
        target.transform.rotation *= Quaternion.AngleAxis(_cameraRotationXSpeed * Time.deltaTime, Vector3.up);
        target.transform.rotation *= Quaternion.AngleAxis(-_cameraRotationYSpeed * Time.deltaTime, Vector3.right);
        var angles = target.transform.localEulerAngles;
        angles.z = 0;
        var angle = target.transform.localEulerAngles.x;
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }
        target.transform.localEulerAngles = angles;
    }
}
