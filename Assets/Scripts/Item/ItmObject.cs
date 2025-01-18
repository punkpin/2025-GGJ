using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItmObject : MonoBehaviour
{




  public PlayerObject PlayerObject;

public int maxQueueSize = 4; // 最大队列大小
    public float radius = 5f; // 区域半径
    private Queue<Player> playerQueue = new Queue<Player>(); // 玩家队列
    private float timer = 0f; // 计时器
    private bool isTimerRunning = false; // 计时器状态




public int ItenNumber;

private float GetTimer=3.0f;
public bool isCloseItem;


    // Start is called before the first frame update
    void Start()
    {
    }

private class Player
    {
        public GameObject playerObject;
        public float enterTime;

        public Player(GameObject playerObject, float enterTime)
        {
            this.playerObject = playerObject;
            this.enterTime = enterTime;
        }
    }

    void Update()
    {
 if(ItenNumber!=3)
      return;


        // 如果计时器在运行
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            
            // 如果计时器结束，发放道具并重置
            if (timer <= 0f)
            {
                GiveItemToFirstPlayer();
                ResetQueue();
            }
        }
    }

    // 当玩家进入区域A
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerQueue.Count < maxQueueSize)
        {
            // 创建玩家对象并加入队列
            Player newPlayer = new Player(other.gameObject, Time.time);
            playerQueue.Enqueue(newPlayer);

            // 启动计时器
            if (playerQueue.Count == 1 && !isTimerRunning)
            {
                StartTimer();
            }
        }
    }

    // 当玩家退出区域A
    private void OnTriggerExit2D(Collider2D other)
    {
        // 移除离开的玩家
        Player playerToRemove = null;
        foreach (var player in playerQueue)
        {
            if (player.playerObject == other.gameObject)
            {
                playerToRemove = player;
                break;
            }
        }

        if (playerToRemove != null)
        {
            playerQueue = new Queue<Player>(playerQueue.Where(p => p != playerToRemove));
            Debug.Log("Player removed: " + other.gameObject.name);
            
            // 如果队列为空，停止计时器
            if (playerQueue.Count == 0)
            {
                StopTimer();
            }
            else
            {
                // 重置计时器并调整队列
                ResetTimerAndAdjustQueue();
            }
        }
    }

    // 启动计时器
    private void StartTimer()
    {
        timer = 3f; // 设置3秒计时器
        isTimerRunning = true;
    }

    // 停止计时器
    private void StopTimer()
    {
        isTimerRunning = false;
    }

    // 重置计时器并调整队列顺序
    private void ResetTimerAndAdjustQueue()
    {
        timer = 3f;
        // 重排队列：按进入时间排序
        playerQueue = new Queue<Player>(playerQueue.OrderBy(p => p.enterTime));
        StartTimer();
    }

    // 发放道具给队列最先进入的玩家
    private void GiveItemToFirstPlayer()
    {
        if (playerQueue.Count > 0)
        {
            Player firstPlayer = playerQueue.Dequeue(); // 移除最前面的玩家

     
            Debug.Log("Giving item to: " + firstPlayer.playerObject.name);
            GameObject XX=firstPlayer.playerObject;

            PlayerObject = XX.GetComponent<PlayerObject>(); 
        if(ItenNumber==3)
          {
        PlayerObject.SetItem(3);
         Destroy(gameObject);
        Debug.Log("Destroy Item of"+gameObject);
          }
       }
    
    }

    // 重置队列
    private void ResetQueue()
    {
        playerQueue.Clear();
        StopTimer();
    }




    
private void OnCollisionEnter2D(Collision2D collision)
{


    // Check the collided object
    GameObject collidedObject = collision.gameObject;
     Debug.Log("Collided with: " + collidedObject.name);

 
    // Check if the collided object has the "Player" tag
    if (collidedObject.CompareTag("Player"))
    {
         PlayerObject = collidedObject.GetComponent<PlayerObject>(); 
    if(ItenNumber==1)
     {
        PlayerObject.SetItem(1);
                 Destroy(gameObject);
        Debug.Log("Destroy Item of"+gameObject);

     }else if(ItenNumber==2)
     {
       PlayerObject.SetItem(2);
                Destroy(gameObject);
        Debug.Log("Destroy Item of"+gameObject);
     }

    }
  }

}

