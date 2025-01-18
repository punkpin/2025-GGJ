using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemManager : MonoBehaviour
{
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



    public void useImem001()
    {



    }
   public void useImem002()
    {



    }
   public void useImem003()
    {



    }
}