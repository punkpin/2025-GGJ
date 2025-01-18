using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Keymap
{
    public string up;
    public string down;
    public string left;
    public string right;
    public string skill;
    public string item;
}

[Serializable]
public class PlayerController
{
    public string name;
    public Keymap keymap;
    public Location location;
    public int textlocation;
}

[Serializable]
public class PlayerSelectionConfigData
{
    public List<PlayerController> controllers;
    public List<Skill> skills;
}

[Serializable]
public class Skill
{
    public string skill;
    public Location location;

    public string skillMessage;
}

public class PlayerSelectionConfig : MonoBehaviour
{
    public PlayerSelectionConfigData configData;
    public string jsonFilePath;

    void Awake()
    {
        LoadConfig();
    }

    void LoadConfig()
    {
        string json = File.ReadAllText(jsonFilePath);
        configData = JsonUtility.FromJson<PlayerSelectionConfigData>(json);
    }
}