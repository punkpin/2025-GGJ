using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationController : MonoBehaviour
{
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    public float delay = 1.0f;

    private string lastDirection; // 最后一次移动的方向
    private Animator animator;  // Animator组件

    void Start()
    {
        animator = GetComponent<Animator>();
        
        animator.SetBool("MoveUp", false);
        animator.SetBool("MoveDown", false);
        animator.SetBool("MoveSide", false);
        animator.SetBool("isMoving", false);
        
        animator.SetBool("isDizzy", false);
    }

    void Update()
    {  
        UpdateAnimationState(moveX, moveY);

        if (moveX < 0)
        {
            Flip(true);
        }
        else if (moveX > 0)
        {
            Flip(false);
        }
    }

    void UpdateAnimationState(float moveX, float moveY)
    {
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        bool isMoving = movement.magnitude > 0;
        animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            if (Mathf.Abs(moveY) > Mathf.Abs(moveX))
            {
                if (moveY > 0)
                {
                    animator.SetBool("MoveUp", true);
                    lastDirection = "MoveUp";
                }
                else
                {
                    animator.SetBool("MoveDown", true);
                    lastDirection = "MoveDown";
                }
            }
            else
            {
                if (moveX > 0)
                {
                    animator.SetBool("MoveSide", true);
                    lastDirection = "MoveSide";
                }
                else
                {
                    animator.SetBool("MoveSide", true);
                    lastDirection = "MoveSide";
                }
            }
        }
        else
        {
            animator.SetBool("MoveUp", lastDirection == "MoveUp");
            animator.SetBool("MoveDown", lastDirection == "MoveDown");
            animator.SetBool("MoveSide", lastDirection == "MoveSide");
        }
    }
    void Flip(bool flipLeft)
    {
        Vector3 localScale = transform.localScale;
        if (flipLeft)
        {
            localScale.x = -Mathf.Abs(localScale.x);
        }
        else
        {
            localScale.x = Mathf.Abs(localScale.x);
        }
        transform.localScale = localScale;
    }

    public void UseSkill()
    {
        animator.SetTrigger("UseSkill");
    }
    
    public void TriggerDizzy()
    {
        animator.SetBool("isDizzy",true);
        
        StartCoroutine(ChangeBool());
    }
    
    private IEnumerator ChangeBool()
    {
        yield return new WaitForSeconds(delay);

        animator.SetBool("isDizzy", false);
    }
}
