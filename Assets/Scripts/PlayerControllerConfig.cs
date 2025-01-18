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
}

[Serializable]
public class PlayerControllerConfigData
{
    public List<PlayerController> controllers;
}

public class PlayerControllerConfig : MonoBehaviour
{
    public PlayerControllerConfigData configData;
    public string jsonFilePath;

    void Awake()
    {
        LoadConfig();
    }

    void LoadConfig()
    {
        string json = File.ReadAllText(jsonFilePath);
        configData = JsonUtility.FromJson<PlayerControllerConfigData>(json);
    }
}