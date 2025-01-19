using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemManager : MonoBehaviour
{

      // 物体列表，用于存储生成的物体
   private List<GameObject> objectList = new List<GameObject>();

    // 物体预制体（将需要的物体拖到此变量中）
     public GameObject[] prefabs; // 存储所有的预制体

    //物体数组
    public GameObject[] objects;


public PlayerObject PlayerObjectXXX;



    // 定时器
    private float timerObj;


    public GameObject bubblePrefab;  // 泡泡的预制体
    public float speedMultiplier = 1.2f;  // 泡泡的速度是玩家速度的1.2倍
    public float bubbleSizeMultiplier = 0.5f;  // 泡泡的大小是玩家的一半



public AudioClip[] BGMClips;  // 管理背景音乐
  public AudioSource bgmAudioSource;




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


    void Start()
    {


 bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.clip = BGMClips[0];  
            bgmAudioSource.Play();

        // 初始化定时器
        timerObj = 8f;
    }

    void Update() 
    {
        // 定时器倒计时
        timerObj -= Time.deltaTime;

        // 每隔 8 秒执行一次
        if (timerObj <= 0f)
        {
            timerObj = 8f; // 重置定时器

            // 随机选择一个数字（1、2 或 3）
            int randomNum = Random.Range(1, 3); // 1 到 3
            int randomNumItemID = Random.Range(1, 4); // 1 到 3

            float RandomTime = Random.Range(5f, 10f); // 1 到 3
            // 打印选中的数字
            Debug.Log("随机生成点" + randomNum);

            // 随机选择物体数组中的一个物体
            GameObject randomObject = objects[Random.Range(0, objects.Length)];
           // GameObject CreateItem = prefabs[Random.Range(0, prefabs.Length)];


            Debug.Log("随机出的道具是" + randomNumItemID);
            //Debug.Log("Random number chosen: " + CreateItem);

            // 在选中的物体位置生成预制体

            Vector3 TransformA=randomObject.transform.position;
             SetItem(TransformA,randomNumItemID,RandomTime);
            Debug.Log("创建了一个道具 " + randomNumItemID);

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


   public void useImem002(float PlayerSpeed,object gameObject)
    {

 
        
    }


    public void useImem003(Vector2 centerPosition, int team)
    {
        // 查找所有在范围内的物体
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(centerPosition, 10f);

        foreach (var hitCollider in hitColliders)
        {
            // 确保该物体有Player标签
            if (hitCollider.CompareTag("Player"))
            {
                // 获取该物体上的PlayerObject脚本
                 PlayerObjectXXX = hitCollider.GetComponent<PlayerObject>();
                      
                       Debug.LogWarning("确定生效的玩家是"+hitCollider);
                // 检查脚本是否存在
                if (PlayerObjectXXX != null)
                {
                    Debug.LogWarning("这个玩家的队伍是"+PlayerObjectXXX.team);
                    // 如果team值与传入team值相反，设置速度为0
                    if (PlayerObjectXXX.team != team)
                    {
                        PlayerObjectXXX.moveSpeed = 0;
                        PlayerObjectXXX.speedMultiplier=0f;
                        Debug.LogWarning("改变玩家速度"+speedMultiplier);
                        StartCoroutine(RestoreSpeedAfterDelay(PlayerObjectXXX, 3f));
                    }
                }
            }
        }
    }

    // 恢复速度的协程
    private IEnumerator RestoreSpeedAfterDelay(PlayerObject playerObjectReset, float delay)
    {
        // 等待指定时间（5秒）
        yield return new WaitForSeconds(delay);

        // 恢复速度为原始值（假设恢复前速度为原速度，这里可以修改为实际逻辑）
        playerObjectReset.moveSpeed = 50f; 
         playerObjectReset.speedMultiplier=1f;
        
        // 恢复原速度（这里假设原速度是5，实际应根据需要来设置）
    }



    public void SetItem(Vector3 position, int ItemID,float lifetime)
    {
  
      // 生成物体并设置位置
            GameObject newObject = Instantiate(prefabs[ItemID-1], position, Quaternion.identity);
            
            // 将物体添加到列表中
            objectList.Add(newObject);

        Debug.LogWarning("生成的道具是 "+ prefabs[ItemID-1]);

        // 启动计时器处理生命周期
        StartCoroutine(DestroyAfterTime(newObject, lifetime));
        

    }


// Coroutine 处理物体生命周期
    private IEnumerator DestroyAfterTime(GameObject obj, float lifetime)
    {
        // 等待生命周期结束
        yield return new WaitForSeconds(lifetime);
        
        // 销毁物体
        Destroy(obj);
        
        // 从列表中移除已销毁的物体
        objectList.Remove(obj);
    }



}