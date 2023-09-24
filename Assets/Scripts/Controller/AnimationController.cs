using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }
    public void OnEndRoll()
    {
        playerController.isRolling = false;
    }
    public void OnEndAttack()
    {
        playerController.isAttacking = false;
    }
}
