using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItmObject : MonoBehaviour
{




  public PlayerObject PlayerObject001;

    public int maxQueueSize = 4; // 最大队列大小
    public float radius = 10f; // 区域半径
    public Queue<Player> playerQueue = new Queue<Player>(); // 玩家队列
    public float timer = 0f; // 计时器
    public bool isTimerRunning = false; // 计时器状态
    public int ItenNumber;

    private float GetTimer=3.0f;
    public bool isCloseItem;
    void Start()
    {

    }

     public void OnCollisionEnter2D(Collision2D other)
    {

        // Check the collided object
        GameObject collidedObject = other.gameObject;

        Debug.LogWarning("道具被玩家撞击: " + collidedObject.name);

        // Check if the collided object has the "Player" tag
        if (collidedObject.CompareTag("Player"))
        {
            PlayerObject001  = collidedObject.GetComponent<PlayerObject>();

        if(ItenNumber==1)
        {
            PlayerObject001.SetItem(1);
            Debug.Log("给与玩家了一个"+ItenNumber);
            Debug.Log("给与的玩家是："+PlayerObject001);
                    Destroy(gameObject);
            Debug.Log("Destroy Item of"+gameObject);

        }else if(ItenNumber==2)
        {
        PlayerObject001.SetItem(2);
            Debug.Log("给与玩家了一个"+ItenNumber);
            Debug.Log("给与的玩家是："+PlayerObject001);
                    Destroy(gameObject);
            Debug.Log("Destroy Item of"+gameObject);
        }
        else if(ItenNumber==3)
        {
        PlayerObject001.SetItem(3);
            Debug.Log("给与玩家了一个"+ItenNumber);
            Debug.Log("给与的玩家是："+PlayerObject001);
                    Destroy(gameObject);
            Debug.Log("Destroy Item of"+gameObject);
        }

    }
}


}

