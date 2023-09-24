using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    public GameManager gameManager;

    private void OnDestroy()
    {
        gameManager.ChangeCamera();
    }
}
