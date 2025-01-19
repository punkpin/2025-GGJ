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
    public string prefab;
    public string color;
    public int team;
    public Keymap keymap;
    public int location;
}

[Serializable]
public class PlayerSelectionConfigData
{
    public List<Location> controllerLocations;
    public List<PlayerController> controllers;
    public List<Location> skillLocations;
    public List<Skill> skills;
}

[Serializable]
public class Skill
{
    public string skill;
    public int location;
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