using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIManager : MonoBehaviour
{
 

    public static CharacterUIManager Instance;

// 假设UI是Image类型的数组，每个UI对应一个角色的显示图片
    public Image[] UIArray = new Image[4];  // 角色的UI数组
    public Image[] scrollbarArray = new Image[4];  // Scrollbar数组


    // PlayerOBJ二维数组 [角色代号, 道具类型]
    public int[,]   PlayerOBJ = new int[4, 3];  // 每个角色代号和对应的道具类型

    // UI图片A/B/C/D 可以是资源路径，或者直接赋值预设的Sprite对象
    public Sprite itemA;
    public Sprite itemB;
    public Sprite itemC;
    public Sprite noItem;

    // 方法：根据角色代号和道具类型修改UI和Scrollbar
    public void UpdateUI(int playerID, int itemType)
    {
        if (playerID < 0 || playerID >= 4)
        {
            Debug.LogError("Player ID is out of range!");
            return;
        }

        // 更新PlayerOBJ数组的道具类型
        PlayerOBJ[playerID, 1] = itemType;

        // 根据传入的道具类型设置UI显示
        switch (itemType)
        {
            case 1:
                UIArray[playerID].sprite = itemA;  // 显示A
                break;
            case 2:
                UIArray[playerID].sprite = itemB;  // 显示B
                break;
            case 3:
                UIArray[playerID].sprite = itemC;  // 显示C
                break;
            default:
                UIArray[playerID].sprite = noItem;  // 不显示任何图片
                break;
        }

        

    }


    public void UpdateScrool(int playerID)

    {

    StartCoroutine(ChangeScrollbarValue(playerID));

Debug.LogWarning("开始机能CD转圈: " + playerID);
    }

// 协程：在 5 秒内将Scrollbar的值从 1 渐变到 0
    private IEnumerator ChangeScrollbarValue(int X)
    {
        float duration = 5f;  // 持续时间为 5 秒
        float startValue = 1f; // 初始值为 1
        float targetValue = 0f; // 目标值为 0
        float elapsedTime = 0f; // 经过的时间

         Debug.LogWarning("机能CD转圈转的是: " + scrollbarArray[X].fillAmount );
    
        // 让Scrollbar值从1渐变到0
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // 计时
            scrollbarArray[X].fillAmount  = Mathf.Lerp(startValue, targetValue, elapsedTime / duration); // 按时间插值
            yield return null; // 等待下一帧
        }

        // 等到 5 秒结束后，恢复Scrollbar的值为 1
        scrollbarArray[X].fillAmount  = 1f;
    }



    // 可选：初始化 PlayerOBJ 数组（示例）
     void Start()
    {
        // 假设角色0没有道具，角色1持有道具类型3，角色2持有道具类型2
        PlayerOBJ[0, 0] = 0; PlayerOBJ[0, 1] = 0;  // 角色0, 无道具
        PlayerOBJ[1, 0] = 0; PlayerOBJ[1, 1] = 0;  // 角色1, 道具类型3
        PlayerOBJ[2, 0] = 0; PlayerOBJ[2, 1] = 0;  // 角色2, 道具类型2
        PlayerOBJ[3, 0] = 0; PlayerOBJ[3, 1] = 0;  // 角色3, 道具类型1

        // 更新UI初始显示
        UpdateUI(0, PlayerOBJ[0, 1]);
        UpdateUI(1, PlayerOBJ[1, 1]);
        UpdateUI(2, PlayerOBJ[2, 1]);
        UpdateUI(3, PlayerOBJ[3, 1]);
    }
}