using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectHandler : MonoBehaviour
{
    public PlayerControllerObject playerControllerObject;
    public SkillSelectObject skillSelectObject;
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
        // if (playerSelectionResult.IsReady())
        // {
        //     SceneManager.LoadScene("MainScene");
        // }
    }

    private void GeneratePlayers()
    {
        foreach (var controller in playerSelectionConfig.configData.controllers)
        {
            var playerObject = Instantiate(playerControllerObject, new Vector3(controller.location.x + 0.5f * controller.location.width, controller.location.y + 0.5f * controller.location.height, -1), Quaternion.identity);
            playerObject.transform.localScale = new Vector3(controller.location.width, controller.location.height, 1);

            playerObject.locationX = controller.location.x;
            playerObject.locationY = controller.location.y;
            playerObject.name = controller.name;
            playerObject.textlocation = controller.textlocation;

            playerObject.moveLeftKey = ParseKeyCode(controller.keymap.left);
            playerObject.moveRightKey = ParseKeyCode(controller.keymap.right);
        }
    }
    private void GenerateSkills()
    {
        foreach (var skill in playerSelectionConfig.configData.skills)
        {
            var skillObject = Instantiate(skillSelectObject, new Vector3(skill.location.x + 0.5f * skill.location.width, skill.location.y + 0.5f * skill.location.height, -0.9f), Quaternion.identity);
            skillObject.transform.localScale = new Vector3(skill.location.width, skill.location.height, 1.0f);
            
            skillObject.locationX = skill.location.x;
            skillObject.locationY = skill.location.y;
            skillObject.skill = skill.skill;
            skillObject.skillMessage = skill.skillMessage;
        }
    }

    private KeyCode ParseKeyCode(string key)
    {
        if (int.TryParse(key, out int number) && number >= 0 && number <= 9)
        {
            return KeyCode.Alpha0 + number;
        }

        if (System.Enum.TryParse(key.ToLower(), true, out KeyCode keyCode))
        {
            return keyCode;
        }

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
            default:              
                throw new System.ArgumentException($"Unsupported key: {key}");
        }
    }
}
