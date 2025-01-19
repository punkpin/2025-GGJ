using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro; 

public class PlayerControllerObject : MonoBehaviour
{
    public string name;
    public int index;
    public int location;
    public bool isReady;
    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode useSkillKey;

    private AnimationController animationController;
    private TextMeshPro playerText;

    public PlayerSelectionResult playerSelectionResult;
    public PlayerSelectionConfig playerSelectionConfig;
    private List<Location> controllerlocations;
    private List<Skill> skills;
    private PlayerController playerController;
    private Color textColor;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    void Start()
    {
        animationController = GetComponent<AnimationController>();
        PlayerSelectionConfigData playerSelectionConfigData = playerSelectionConfig.configData; 
        controllerlocations = playerSelectionConfigData.controllerLocations;
        skills = playerSelectionConfigData.skills;
        playerController = playerSelectionConfigData.controllers[index];
        textColor = ColorUtility.TryParseHtmlString(playerController.color, out Color color) ? color : Color.white;
        Keymap keymap = playerController.keymap;
        playerText = GetComponentInChildren<TextMeshPro>();
        if (playerText != null)
        {
            playerText.text = $"Player {playerController.name}\n上: {keymap.up.ToUpper()} 下: {keymap.down.ToUpper()}\n左: {keymap.left.ToUpper()} 右: {keymap.right.ToUpper()}\n技能: {keymap.skill.ToUpper()}\n道具: {keymap.item.ToUpper()}";
            playerText.color = textColor;
        }
    }

    void Update()
    {
        if (isReady)
        {
            return;
        }

        // bool changeLocation = false;

        // if (Input.GetKey(moveLeftKey) && location > 0 && !isReady && !isMovingLeft)
        // {
        //     isMovingLeft = true;
        //     changeLocation = true;
        //     location--;
        // }
        // if (Input.GetKey(moveRightKey) && location < controllerlocations.Count()- 1 && !isReady && !isMovingRight)
        // {
        //     isMovingRight = true;
        //     changeLocation = true;
        //     location++;
        // }

        // if (Input.GetKeyUp(moveLeftKey))
        // {
        //     isMovingLeft = false;
        // }

        // if (Input.GetKeyUp(moveRightKey))
        // {
        //     isMovingRight = false;
        // }

        // if (changeLocation)
        // {
        //     UpdateLocation();
        //     changeLocation = false;
        // }

        if (Input.GetKeyDown(useSkillKey))
        {
            isReady = SetReady();
        }
    }

    private bool SetReady()
    {
        string skill = skills.FirstOrDefault(s => s.location == location)?.skill;
        if (playerSelectionResult.Set(skill, playerController))
        {
            playerText.text = $"\nPlayer {playerController.name}\n\nREADY";
            playerText.fontSize *= 1.2f;
            animationController.UseSkill();
            return true;
        }
        return false;
    }

    private void UpdateLocation()
    {
        transform.position = new Vector3(controllerlocations[location].x, controllerlocations[location].y, -1);
        transform.localScale = new Vector3(controllerlocations[location].width, controllerlocations[location].height, 1);
    }
}