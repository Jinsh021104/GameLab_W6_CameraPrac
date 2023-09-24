using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveingPlattform : MonoBehaviour
{
    public float maxDistance;
    public bool isReverse;
    float positionMaxZ;
    float positionOriginZ;
    float time = 5f;
    private void Start()
    {
        positionOriginZ = transform.position.z;
        if (isReverse)
        {
            positionMaxZ = transform.position.z - maxDistance;
        }
        else
        {
            positionMaxZ = transform.position.z + maxDistance;
        }
    }
    private void Update()
    {
        if(gameObject.CompareTag("Obstacle") && transform.GetComponentInParent<ButtonClick>().isClicked)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(positionOriginZ, positionMaxZ, time));
            time -= Time.deltaTime;
            if(time < 0f)
            {
                time = 3f;
                transform.GetComponentInParent<ButtonClick>().button.GetComponent<ButtonTrigger>().isTrigger = false;
                transform.GetComponentInParent<ButtonClick>().button.SetActive(true);
                if(!gameObject.CompareTag("Frozen"))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
