using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerObject : BaseBubble 
{
    public Color color;
    public int team;
    public string skill;

    public float speedMultiplier=1f;

    

//item
    public int TestItem;
    public int item;

    public bool isBoosted;
   public Vector2 PlayeryDirection;


       public ItemManager ItemManager;

//item

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

  private GameGenerator gameGenerator;

    private float defaultSpeed;
    private float defaultColliderSize;

    private Vector3 defaultScale;

    new void Start()
    {
        base.Start();
       
//ITEM 
       SetItem(TestItem);
        ItemManager itemManager = ItemManager.Instance;  
         GameObject targetObject = GameObject.Find("itemManager");
        if (targetObject != null)
        {
            ItemManager = targetObject.GetComponent<ItemManager>();
        }
//



        defaultSpeed = moveSpeed;
        defaultColliderSize = colliderSize;
        defaultScale = gameObject.transform.localScale;
        gameGenerator = FindObjectOfType<GameGenerator>();
    }

    void Update()
    {


 if (!gameGenerator.globalFreeze)  
{
        Vector2 moveDirection = Vector2.zero;

        if (Input.GetKey(moveUpKey))
        {
            moveDirection.y += 1*speedMultiplier;
        }
        if (Input.GetKey(moveDownKey))
        {
            moveDirection.y -= 1*speedMultiplier;
        }
        if (Input.GetKey(moveLeftKey))
        {
            moveDirection.x -= 1*speedMultiplier;
        }
        if (Input.GetKey(moveRightKey))
        {
            moveDirection.x += 1*speedMultiplier;
        }
         PlayeryDirection=moveDirection;

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



    /// <summary>
/// itme
/// </summary>


    public void SetItem(int itemNumber)
    {
        item = itemNumber;
        Debug.Log("获得了物品"+itemNumber);
    }

    private void UseItem()
    {
        // Implement item logic here
           if (item != 0)
        {
            

            if(item==1){
               // 调用ITEM的Use方法
               Vector3 playerPosition = transform.position;

                ItemManager.useImem001(playerPosition, PlayeryDirection,moveSpeed,team,gameObject);
            }
            else if(item==2)
            {
                // 调用ITEM的Use方法

                   // ItemManager.useImem002(PlayerSpeed,gameObject);

                    BoostSpeed();
            }
            else if(item==3)
            {
                Vector3 playerPosition = transform.position;
                // 调用ITEM的Use方法
                ItemManager.useImem003(playerPosition,team);
           
            }

             Debug.Log("Item ID USED:"+item);
             item=0;
        }
        else
        {
            Debug.Log("没有物品可以使用");
        }
   
    }


 void BoostSpeed()
    {
        
        isBoosted = true;

        speedMultiplier=1.5f;
       moveSpeed = 1.5f * defaultSpeed; // 将速度提升为基础速度的1.5倍
        Debug.Log("Speed boosted to: " + moveSpeed);

        // 启动定时器，5秒后恢复基础速度
        Invoke("ResetSpeed", 5f);
    }


     void ResetSpeed()
    {
        moveSpeed = 50f; // 恢复到基础速度
      
      
        speedMultiplier=1f;
        Debug.Log("Speed reset to base: " + moveSpeed);
        isBoosted = false;
    }
/// <summary>
/// itme
/// </summary>


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