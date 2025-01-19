using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMoveController : MonoBehaviour
{
    
    private Animator animator;  // Animator组件
    private Rigidbody2D rb;     // Rigidbody2D组件
    private Vector2 movement;   // 存储玩家的移动向量

    public float moveSpeed = 5f;  // 玩家移动速度
    private string lastDirection; // 最后一次移动的方向

    private float delay = 1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        animator.SetBool("MoveUp", false);
        animator.SetBool("MoveDown", false);
        animator.SetBool("MoveSide", false);
        //animator.SetBool("MoveRight", false);
        animator.SetBool("isMoving", false);
    }

    void Update()
    {
        // 获取玩家的输入（使用 I, K, J, L 控制）
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.J)) // J 键向左
        {
            moveX = -5f;
        }
        else if (Input.GetKey(KeyCode.L)) // L 键向右
        {
            moveX = 5f;
        }

        if (Input.GetKey(KeyCode.I)) // I 键向上
        {
            moveY = 5f;
        }
        else if (Input.GetKey(KeyCode.K)) // K 键向下
        {
            moveY = -5f;
        }

        // 创建移动向量
        movement = new Vector2(moveX, moveY).normalized;

        // 控制动画状态和翻转
        UpdateAnimationState(moveX, moveY);

        // 实现动画翻转（镜像效果）
        if (moveX < 0) // 玩家按下 'J' 键 或 向左移动
        {
            Flip(true);  // 翻转角色（镜像）
        }
        else if (moveX > 0) // 玩家按下 'L' 键 或 向右移动
        {
            Flip(false); // 恢复正向
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UseSkill();
        }
        
    }

    void FixedUpdate()
    {
        // 使用物理引擎控制玩家移动
        rb.velocity = movement * moveSpeed;
    }

    void UpdateAnimationState(float moveX, float moveY)
    {
        // 判断是否在移动
        bool isMoving = movement.magnitude > 0;

        // 设置是否在移动的参数
        animator.SetBool("isMoving", isMoving);

        // 设置最后一次移动的方向
        if (isMoving)
        {
            if (Mathf.Abs(moveY) > Mathf.Abs(moveX)) // 判断是上下移动
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
            else // 判断是左右移动
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
            // 如果没有移动，则切换到最后一次移动的方向的待机状态
            animator.SetBool("MoveUp", lastDirection == "MoveUp");
            animator.SetBool("MoveDown", lastDirection == "MoveDown");
           // animator.SetBool("MoveLeft", lastDirection == "MoveLeft");
            animator.SetBool("MoveSide", lastDirection == "MoveSide");
        }
    }

    // 翻转角色的方法
    void Flip(bool flipLeft)
    {
        Vector3 localScale = transform.localScale;
        if (flipLeft)
        {
            localScale.x = -Mathf.Abs(localScale.x); // 向左翻转，负值
        }
        else
        {
            localScale.x = Mathf.Abs(localScale.x);  // 向右恢复正向
        }
        transform.localScale = localScale;
    }
    
    void UseSkill()
    {
        animator.SetTrigger("UseSkill");
        
        
    }

    void OnCollisionEnter2D(Collision2D collision)
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


