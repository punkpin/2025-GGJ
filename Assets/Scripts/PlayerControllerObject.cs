using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerObject : MonoBehaviour
{
    public int locationX;
    public int locationY;
    public bool isReady;
    public int textlocation;

    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode useSkillKey = KeyCode.Q;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(moveLeftKey) && locationX > 0 && !isReady)
        {
            locationX--;
        }
        if (Input.GetKey(moveRightKey) && locationX < 4 && !isReady)
        {
            locationX++;
        }

        if (Input.GetKeyDown(useSkillKey))
        {
            isReady = SetReady();
        }
    }

    private bool SetReady()
    {
        return false;
    }
}