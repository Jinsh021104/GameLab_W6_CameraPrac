using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    private Color _obstacleColor = Color.white;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce((transform.forward * speed + (transform.up * 3f)), ForceMode.Impulse);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            Debug.Log(other.name);
            if (other.CompareTag("Obstacle"))
            {
                _obstacleColor = other.GetComponent<Renderer>().material.color;
                _obstacleColor = new Color(_obstacleColor.r - 0.25f, _obstacleColor.g - 0.25f, 1f);
                other.GetComponent<Renderer>().material.color = _obstacleColor;

                if (other.GetComponent<Renderer>().material.color.Equals(Color.blue))
                {
                    other.gameObject.tag = "Frozen";
                }
            }
            if (other.CompareTag("Ground") || other.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
    }
}
