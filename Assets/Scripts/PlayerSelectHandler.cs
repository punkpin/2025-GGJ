using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectHandler : MonoBehaviour
{
    public List<PlayerControllerObject> playerControllerObjects;
    public SkillSelectObject skillSelectObject;
    public string sceneName;
    private PlayerSelectionConfig playerSelectionConfig;
    private PlayerSelectionResult playerSelectionResult;

    void Start()
    {
        playerSelectionConfig = FindObjectOfType<PlayerSelectionConfig>();
        playerSelectionResult = FindObjectOfType<PlayerSelectionResult>();
        GeneratePlayers();
        GenerateSkills();
    }

    void Update()
    {
        if (playerSelectionResult.IsReady())
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void GeneratePlayers()
    {
        List<Location> controllerLocations = playerSelectionConfig.configData.controllerLocations;
        foreach (var controller in playerSelectionConfig.configData.controllers)
        {
            PlayerControllerObject playerControllerObject = playerControllerObjects.Find(p => p.name == controller.prefab);
            var playerObject = Instantiate(playerControllerObject, new Vector3(controllerLocations[controller.location].x, controllerLocations[controller.location].y, -1), Quaternion.identity);
            playerObject.transform.localScale = new Vector3(controllerLocations[controller.location].width, controllerLocations[controller.location].height, 1);

            playerObject.index = controller.location;
            playerObject.location = controller.location;
            playerObject.name = controller.name;
            playerObject.playerSelectionConfig = playerSelectionConfig;
            playerObject.playerSelectionResult = playerSelectionResult;

            playerObject.moveLeftKey = ParseKeyCode(controller.keymap.left);
            playerObject.moveRightKey = ParseKeyCode(controller.keymap.right);
            playerObject.useSkillKey = ParseKeyCode(controller.keymap.skill);
        }
    }
    private void GenerateSkills()
    {
        List<Location> skillLocations = playerSelectionConfig.configData.skillLocations;

        foreach (var skill in playerSelectionConfig.configData.skills)
        {
            var skillObject = Instantiate(skillSelectObject, new Vector3(skillLocations[skill.location].x, skillLocations[skill.location].y, -1), Quaternion.identity);
            skillObject.transform.localScale = new Vector3(skillLocations[skill.location].width, skillLocations[skill.location].height, 1);

            skillObject.location = skill.location;
            skillObject.skill = skill.skill;
            skillObject.skillMessage = skill.skillMessage;
        }
    }

    private KeyCode ParseKeyCode(string key)
    {
        switch (key)
        {
            case "-":
                return KeyCode.Minus;
            case "[":
                return KeyCode.LeftBracket;
            case "]":
                return KeyCode.RightBracket;
            case "=":
                return KeyCode.Equals;
            case ",":
                return KeyCode.Comma;
            case ".":
                return KeyCode.Period;
            case "/":
                return KeyCode.Slash;
            case ";":
                return KeyCode.Semicolon;
            case "'":
                return KeyCode.Quote;
            case "\\":
                return KeyCode.Backslash;
            case "`":
                return KeyCode.BackQuote;
            case "up":
                return KeyCode.UpArrow;
            case "down":
                return KeyCode.DownArrow;
            case "left":
                return KeyCode.LeftArrow;
            case "right":
                return KeyCode.RightArrow;
            case "right ctrl":
                return KeyCode.RightControl;
            case "right alt":
                return KeyCode.RightAlt;
            default:   
                break;
        }
        
        if (int.TryParse(key, out int number) && number >= 0 && number <= 9)
        {
            return KeyCode.Alpha0 + number;
        }

        if (System.Enum.TryParse(key.ToLower(), true, out KeyCode keyCode))
        {
            return keyCode;
        }
        return KeyCode.None;
    }
}
