using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hammer : MonoBehaviour
{
    private PlayerController _playerController;
    private void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }
    public void OnUseAbility(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(!_playerController.isAttacking)
            {
                _playerController.isAttacking = true;
                _playerController.animator.SetTrigger("isAttack");
                Hammering();
            }
        }
    }

    void Hammering()
    {
        if (Physics.Raycast(_playerController.target.position, _playerController.target.forward, out RaycastHit hit, 3f, LayerMask.GetMask("Obstacle")))
        {
            if(hit.collider.CompareTag("Frozen"))
            {
                Destroy(hit.collider.gameObject);
            }
            if(hit.collider.CompareTag("Button"))
            {
                hit.collider.gameObject.GetComponent<ButtonTrigger>().isTrigger = true;
                hit.collider.gameObject.SetActive(false);
            }
        }
    }
}
