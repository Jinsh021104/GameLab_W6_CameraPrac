using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceGun : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    
    private PlayerController _playerController;

    private bool _isShot = false;
    private bool _canShot = true;
    private float _delayTime = 0.3f;

    private void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }
    public void OnUseAbility(InputAction.CallbackContext context)
    {
        if(context.started && _playerController.isAim)
        {
            _isShot = true;
        }
        if(context.canceled)
        {
            _isShot = false;
        }
    }

    private void FixedUpdate()
    {
        if(_isShot && _canShot)
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            StartCoroutine(FireDelay());
            _canShot = false;
        }
    }

    IEnumerator FireDelay()
    {
        float delay = 0f;
        while(delay <= _delayTime)
        {
            delay += Time.deltaTime;
            yield return null;
        }
        _canShot = true;
    }
}
