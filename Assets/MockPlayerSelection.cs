using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockPlayerSelection : MonoBehaviour
{
    public PlayerSelectionResult playerSelectionResult;
    public PlayerControllerConfig playerControllerConfig;
    void Start()
    {
        List<string> skills = new List<string> { "fly", "large", "claw", "fast" };
        for (int i = 0; i < playerControllerConfig.configData.controllers.Count; i++)
        {
            Debug.Log($"Setting player selection for {playerControllerConfig.configData.controllers[i].name} with skill {skills[i]}");
            playerSelectionResult.Set(skills[i], playerControllerConfig.configData.controllers[i]);
        }
    }
}
