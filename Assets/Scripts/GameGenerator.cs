using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameGenerator : MonoBehaviour
{
    public GroundObject groundPrefab;
    public BombObject bombPrefab;
    public PlayerObject playerPrefab;
    public ItemObject itemPrefab;
    public GameConfig gameConfig;
    public Dictionary<int, float> teamScore = new Dictionary<int, float>();
    public int groundObjectCount = 0;

    private Dictionary<Vector2, GroundObject> groundObjects;

    void Start()
    {
        teamScore.Clear();
        groundObjects = new Dictionary<Vector2, GroundObject>();
        GenerateGround();
        GenerateWalls();
        GenerateBombs();
        GeneratePlayers();
        GenerateItems();
        AdjustCamera();
        groundObjectCount = groundObjects.Values.Count(x => !x.isWall);
    }

    void Update()
    {
        var updatedScores = new Dictionary<int, float>();
        foreach (var score in teamScore)
        {
            updatedScores[score.Key] = groundObjects.Values.Count(x => x.team == score.Key && !x.isWall); 
            Debug.Log($"Team {score.Key}: {updatedScores[score.Key]} points");
        }
        teamScore = updatedScores;
    }

    void GenerateGround()
    {
        var ground = gameConfig.configData.ground;
        GameObject groundParent = new GameObject("GroundParent");
        for (int i = 0; i < ground.width; i++)
        {
            for (int j = 0; j < ground.height; j++)
            {
                Vector2 position = new Vector2(i, j);
                GroundObject groundObj = Instantiate(groundPrefab, new Vector3(i, j, 0), Quaternion.identity);
                groundObj.transform.parent = groundParent.transform;
                groundObj.position = position;
                groundObjects[position] = groundObj;
            }
        }
    }

    void GenerateWalls()
    {
        var walls = gameConfig.configData.walls;
        foreach (var wall in walls)
        {
            for (int i = 0; i < wall.location.width; i++)
            {
                for (int j = 0; j < wall.location.height; j++)
                {
                    Vector2 position = new Vector2(wall.location.x + i, wall.location.y + j);
                    if (groundObjects.TryGetValue(position, out GroundObject groundObject))
                    {
                        groundObject.SetWall(true, ColorUtility.TryParseHtmlString(wall.color, out Color color) ? color : Color.gray);
                    }
                }
            }
        }
    }

    void GenerateBombs()
    {
        var bombs = gameConfig.configData.bombs;
        if (bombs.over)
        {
            GameObject bombParent = new GameObject("BombParent");
            foreach (var location in bombs.location)
            {
                BombObject bomb = Instantiate(bombPrefab, new Vector3(location.x, location.y, -1), Quaternion.identity);
                bomb.transform.parent = bombParent.transform;
            }
        }
    }

    void GeneratePlayers()
    {
        var players = gameConfig.configData.players;
        GameObject playerParent = new GameObject("PlayerParent");
        foreach (var player in players)
        {
            PlayerObject playerObj = Instantiate(playerPrefab, new Vector3(player.location.x + 0.5f * player.location.width, player.location.y + 0.5f * player.location.width, -2), Quaternion.identity);
            playerObj.transform.localScale = new Vector3(player.location.width, player.location.height, 1);
            playerObj.transform.parent = playerParent.transform;

            playerObj.team = int.Parse(player.team);
            playerObj.color = ColorUtility.TryParseHtmlString(player.color, out Color color) ? color : Color.white;
            playerObj.skill = player.skill;

            playerObj.moveUpKey = ParseKeyCode(player.keymap.up);
            playerObj.moveDownKey = ParseKeyCode(player.keymap.down);
            playerObj.moveLeftKey = ParseKeyCode(player.keymap.left);
            playerObj.moveRightKey = ParseKeyCode(player.keymap.right);
            playerObj.useSkillKey = ParseKeyCode(player.keymap.skill);
            playerObj.useItemKey = ParseKeyCode(player.keymap.item);
        }
    }

    void GenerateItems()
    {
        var items = gameConfig.configData.items;
        GameObject itemParent = new GameObject("ItemParent");
        foreach (var item in items)
        {
            ItemObject itemObj = Instantiate(itemPrefab, new Vector3(item.location.x, item.location.y, -1), Quaternion.identity);
            itemObj.transform.parent = itemParent.transform;
            // Set item specific properties if needed
        }
    }

    private void AdjustCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            var ground = gameConfig.configData.ground;
            var halfGroundWidth = ground.width * 0.5f;
            var halfGroundHeight = ground.height * 0.5f;

            mainCamera.transform.position = new Vector3(halfGroundWidth - 0.25f, halfGroundHeight - 0.25f, mainCamera.transform.position.z);
            mainCamera.orthographicSize = halfGroundWidth + 6;
        }
    }

    private KeyCode ParseKeyCode(string key)
    {
        key = key.ToLower();
        if (System.Enum.TryParse(key, true, out KeyCode keyCode))
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