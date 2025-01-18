using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Ground
{
    public int width;
    public int height;
}

[Serializable]
public class Location
{
    public int x;
    public int y;
    public int width;
    public int height;
}

[Serializable]
public class Wall
{
    public string color;
    public Location location;
}

[Serializable]
public class Bomb
{
    public Location location;
}

[Serializable]
public class Player
{
    public string name;
    public int team;
    public string color;
    public string skill;
    public Location location;
}

[Serializable]
public class SpawnTime
{
    public int min;
    public int max;
}

[Serializable]
public class Item
{
    public bool over;
    public string name;
    public List<SpawnTime> spawntime;
    public int lifetime;
    public Location location;
}

[Serializable]
public class GameConfigData
{
    public int gametime;
    public Ground ground;
    public List<Wall> walls;
    public List<Bomb> bombs;
    public List<Player> players;
    public List<Item> items;
}

public class GameConfig : MonoBehaviour
{
    public GameConfigData configData;
    public string jsonFilePath;

    void Awake()
    {
        LoadConfig();
    }

    void LoadConfig()
    {
        string json = File.ReadAllText(jsonFilePath);
        configData = JsonUtility.FromJson<GameConfigData>(json);
    }
}