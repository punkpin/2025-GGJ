using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class SkillSelectObject : MonoBehaviour
{
    public int location;
    public string skill;
    public string skillMessage;

    private TextMeshPro skillMessageText;

    void Start()
    {
        skillMessageText = transform.Find("SkillMessageText").GetComponent<TextMeshPro>();

        if (skillMessageText != null)
        {
            skillMessageText.text = $"{skillMessage}";
        }
    }
}