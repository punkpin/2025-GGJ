using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemManager : MonoBehaviour
{

    public GameObject bubblePrefab;  // 泡泡的预制体
    public float speedMultiplier = 1.2f;  // 泡泡的速度是玩家速度的1.2倍
    public float bubbleSizeMultiplier = 0.5f;  // 泡泡的大小是玩家的一半
  

    public static ItemManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 如果已经有实例，销毁这个物体
        }
    }



    public void useImem001(Vector3 position, Vector2 direction,float PlayerSpeed,int team,object gameObject)
    {
         // 传递过来的位置信息和朝向
        Debug.Log("Position: " + position);
        Debug.Log("Direction: " + direction);
        Debug.Log("PlayerSpeed: " + PlayerSpeed);
         Debug.Log("teamID: " + team);

         // 计算泡泡的速度，泡泡速度为玩家速度的1.2倍
    float bubbleSpeed = PlayerSpeed * 1.2f;


    // 在玩家正前方生成泡泡物体
    GameObject bubble = Instantiate(bubblePrefab, position, Quaternion.identity);

    // 设置泡泡物体的朝向和速度
    Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
    if (rb != null)
    {//按玩家的方向前进
          rb.velocity = direction.normalized * bubbleSpeed;  // 设置泡泡的速度
        // 给泡泡物体设置朝向并n.normalized * bubbleSpeed;  // 设置泡泡的速度
    }

    
     ItemBobble itemBubbleScript = bubble.GetComponent<ItemBobble>();
    if (itemBubbleScript != null)
    {
        // 调用 SetTeam 方法并传入 team 值
        itemBubbleScript.SetTeam(team,gameObject);
    }
    else
    {
        Debug.LogWarning("ItemBubble script not found on the bubble object!");
    }



    }


   public void useImem002()
    {



    }
   public void useImem003()
    {



    }
}