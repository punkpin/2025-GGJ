using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSelectionResult : MonoBehaviour
{
    // Start is called before the first frame update
    public List<PlayerSelection> playerSelections;
    public bool Set(string skill, PlayerController playerController)
    {
        Debug.Log($"Setting player selection for {playerController.name} with skill {skill}");
        if (playerSelections.Any(ps => ps.playerController.name == playerController.name || ps.skill == skill))
        {
            return false;
        }
        playerSelections.Add(new PlayerSelection(skill, playerController));
        return true;
    }

    public bool IsReady()
    {
        return playerSelections.Count == 4;
    }
}

[Serializable]
public class PlayerSelection
{
    public string skill;
    public PlayerController playerController;

    public PlayerSelection(string skill, PlayerController playerController)
    {
        this.skill = skill;
        this.playerController = playerController;
    }
}
