using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : BaseBubble
{
    public Color color;
    public int team;
    public string skill;
    public string item;
    public float colliderSize = 4.0f;
    public float skillCoolDown = 10.0f;

    public float skillTimeRemaining;

    public float freezeTimeRemaining;

    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode useSkillKey = KeyCode.Q;
    public KeyCode useItemKey = KeyCode.E;

    private bool isConquerEnable = true;
    private bool clawing = false;

    private float defaultSpeed;
    private float defaultColliderSize;

    private Vector3 defaultScale;

    new void Start()
    {
        base.Start();
        defaultSpeed = moveSpeed;
        defaultColliderSize = colliderSize;
        defaultScale = gameObject.transform.localScale;
    }

    void Update()
    {
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(moveUpKey))
        {
            moveDirection.y += 1;
        }
        if (Input.GetKey(moveDownKey))
        {
            moveDirection.y -= 1;
        }
        if (Input.GetKey(moveLeftKey))
        {
            moveDirection.x -= 1;
        }
        if (Input.GetKey(moveRightKey))
        {
            moveDirection.x += 1;
        }

        if (isConquerEnable)
        {
            Conquer();
        }

        if (freezeTimeRemaining > 0)
        {
            freezeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            Move(moveDirection);
        }

        if (skillCoolDown > 0)
        {
            skillCoolDown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(useSkillKey))
        {
            UseSkill();
        }

        if (Input.GetKeyDown(useItemKey))
        {
            UseItem();
        }

        if (skillTimeRemaining > 0)
        {
            skillTimeRemaining -= Time.deltaTime;
        }
        else
        {
            ResetSkill();
        }
    }

    private void UseSkill()
    {
        if (skillTimeRemaining > 0 || skillCoolDown > 0)
        {
            Debug.Log("Skill is unavailable");
            return;
        }
        switch (skill)
        {
            case "fast":
                skillTimeRemaining = 5.0f;
                moveSpeed = 1.5f * defaultSpeed;
                gameObject.transform.localScale = 0.8f * defaultScale;
                colliderSize = 0.8f * defaultColliderSize;
                break;
            case "fly":
                skillTimeRemaining = 5.0f;
                isConquerEnable = false;
                moveSpeed = 1.5f * defaultSpeed;
                gameObject.transform.localScale = 1.1f * defaultScale;
                break;
            case "large":
                skillTimeRemaining = 5.0f;
                moveSpeed = 0.8f * defaultSpeed;
                gameObject.transform.localScale = 1.3f * defaultScale;
                colliderSize = 1.3f * defaultColliderSize;
                break;
            case "claw":
                freezeTimeRemaining = 0.0f;
                skillTimeRemaining = 5.0f;
                moveSpeed = 1.1f * defaultSpeed;
                clawing = true;
                break;
            default:
                break;
        }
    }

    private void ResetSkill()
    {
        switch (skill)
        {
            case "fast":
                moveSpeed = defaultSpeed;
                gameObject.transform.localScale = defaultScale;
                break;
            case "fly":
                isConquerEnable = true;
                moveSpeed = defaultSpeed;
                gameObject.transform.localScale = defaultScale;
                break;
            case "large":
                moveSpeed = defaultSpeed;
                gameObject.transform.localScale = defaultScale;
                colliderSize = defaultColliderSize;
                break;
            case "claw":
                moveSpeed = defaultSpeed;
                clawing = false;
                break;
            default:
                break;
        }
    }

    private void UseItem()
    {
        // Implement item logic here
        Debug.Log("Item used");
    }

    private void Conquer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, colliderSize);

        foreach (Collider2D collider in colliders)
        {
            GroundObject groundObject = collider.GetComponent<GroundObject>();
            if (groundObject != null && !groundObject.isWall)
            {
                groundObject.SetTeam(team, color);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check the collided object
        GameObject collidedObject = collision.gameObject;

        // Example: Check if the collided object is a GroundObject
        PlayerObject collidedPlayerObject = collidedObject.GetComponent<PlayerObject>();
        if (collidedPlayerObject != null)
        {
            Debug.Log("Collided with a Player");
            if (!clawing) 
            {
                freezeTimeRemaining = 0.5f;
            }
        }
    }
}