using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControllerObject : MonoBehaviour
{
    public int index;
    public int location;
    public bool isReady;
    public int textlocation;

    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode useSkillKey;

    public PlayerSelectionResult playerSelectionResult;
    public PlayerSelectionConfig playerSelectionConfig;
    private List<Location> controllerlocations;
    private List<Skill> skills;
    private List<PlayerController> playerControllers;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    void Start()
    {
        PlayerSelectionConfigData playerSelectionConfigData = playerSelectionConfig.configData; 
        controllerlocations = playerSelectionConfigData.controllerLocations;
        skills = playerSelectionConfigData.skills;
        playerControllers = playerSelectionConfigData.controllers;
    }

    void Update()
    {
        bool changeLocation = false;
        if (Input.GetKey(moveLeftKey) && location > 0 && !isReady && !isMovingLeft)
        {
            isMovingLeft = true;
            changeLocation = true;
            location--;
        }
        if (Input.GetKey(moveRightKey) && location < controllerlocations.Count()- 1 && !isReady && !isMovingRight)
        {
            isMovingRight = true;
            changeLocation = true;
            location++;
        }

        if (Input.GetKeyUp(moveLeftKey))
        {
            isMovingLeft = false;
        }

        if (Input.GetKeyUp(moveRightKey))
        {
            isMovingRight = false;
        }

        if (changeLocation)
        {
            UpdateLocation();
            changeLocation = false;
        }

        if (Input.GetKeyDown(useSkillKey))
        {
            isReady = SetReady();
        }
    }

    private bool SetReady()
    {
        string skill = skills.FirstOrDefault(s => s.location == location)?.skill;
        PlayerController controller = playerControllers[index];
        if (playerSelectionResult.Set(skill, controller))
        {
            Debug.Log("Set Skill Complete");
            return true;
        }
        return false;
    }

    private void UpdateLocation()
    {
        transform.position = new Vector3(controllerlocations[location].x + 0.5f * controllerlocations[location].width, controllerlocations[location].y + 0.5f * controllerlocations[location].height, -1);
        transform.localScale = new Vector3(controllerlocations[location].width, controllerlocations[location].height, 1);
    }
}