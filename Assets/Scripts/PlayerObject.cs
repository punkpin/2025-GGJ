using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : BaseBubble
{
    public Color color;
    public int team;
    public string skill;
    public string item;

    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode useSkillKey = KeyCode.Q;
    public KeyCode useItemKey = KeyCode.E;

    new void Start()
    {
        base.Start();
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

        Move(moveDirection);
        Conquer();

        if (Input.GetKeyDown(useSkillKey))
        {
            UseSkill();
        }

        if (Input.GetKeyDown(useItemKey))
        {
            UseItem();
        }
        
    }

    private void UseSkill()
    {
        // Implement skill logic here
        Debug.Log($"Skill used: {skill}");
    }

    private void UseItem()
    {
        // Implement item logic here
        Debug.Log("Item used");
    }

    private void Conquer()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1), 0);
        foreach (Collider2D collider in colliders)
        {
            GroundObject groundObject = collider.GetComponent<GroundObject>();
            if (groundObject != null && groundObject.team != team)
            {
                groundObject.SetTeam(team, color);
            }
        }
    }
}