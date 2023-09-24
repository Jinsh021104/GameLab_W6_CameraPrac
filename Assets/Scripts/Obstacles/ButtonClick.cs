using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public bool isClicked = false;
    public GameObject button;
    public List<GameObject> movingPlatforms;
    private void Update()
    {
        isClicked = button.GetComponent<ButtonTrigger>().isTrigger;
        if(isClicked )
        {
            foreach(var item in movingPlatforms)
            {
                item.SetActive(true);
            }
        }
    }
}
