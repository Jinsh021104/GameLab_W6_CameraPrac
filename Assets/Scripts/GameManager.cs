using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject Menu;

    [Header("Camera")]
    public GameObject p1Camera;
    public GameObject p2Camera;
    public GameObject mainCamera;
    [Header("Controller")]
    public PlayerController p1Controller;
    public PlayerController p2Controller;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Cursor.visible = !Cursor.visible;
            if (Cursor.visible)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
            Menu.SetActive(Cursor.visible);
        }
    }
    public void Test(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ChangeCamera();
        }
    }

    public void ChangeCamera()
    {
        mainCamera.SetActive(!mainCamera.activeSelf);
        p1Camera.SetActive(!p1Camera.activeSelf);
        p2Camera.SetActive(!p2Camera.activeSelf);
        p1Controller.isCutScene = mainCamera.activeSelf;
        p2Controller.isCutScene = mainCamera.activeSelf;
        if(mainCamera.activeSelf)
        {
            StartCoroutine(CutSceneTimer(6f));
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator CutSceneTimer(float time)
    {
        float currentTime = 0f;
        while(currentTime < time)
        {
            yield return null;
            currentTime += Time.deltaTime;
        }
        ChangeCamera();
    }
}
