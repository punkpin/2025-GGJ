using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameGenerator : MonoBehaviour
{
    public GroundObject groundPrefab;
    public BombObject bombPrefab;
    public List<PlayerObject> playerPrefabs;
    public GameConfig gameConfig;
    public GameResult gameResult;
    public TextMeshProUGUI showtimeText;
    public TextMeshProUGUI teamScoreListText;
    public bool globalFreeze = true;

    [SerializeField]
    private List<TeamScore> teamScoreList = new List<TeamScore>();
    private float countdownTime = 3.0f;
    private float showTime;
    public int groundObjectCount = 0;
    private float remainingTime;
    private PlayerSelectionResult playerSelectionResult;
    private Dictionary<Vector2, GroundObject> groundObjects;

    void Start()
    {
        groundObjects = new Dictionary<Vector2, GroundObject>();
        playerSelectionResult = FindObjectOfType<PlayerSelectionResult>();
        GenerateGround();
        GenerateWalls();
        GeneratePlayers();
        AdjustCamera();
        groundObjectCount = groundObjects.Values.Count(x => !x.isWall);
        remainingTime = gameConfig.configData.gametime + countdownTime;
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime < gameConfig.configData.gametime)
            {
                showTime = remainingTime;
                globalFreeze = false;
                UpdateTeamScores();
            }
            else
            {
                showTime = remainingTime - gameConfig.configData.gametime;
            }
        }
        else
        {
            EndGame();
        }
        UpdateUI();
    }
    void UpdateTeamScores()
    {
        foreach (var teamScore in teamScoreList)
        {
            teamScore.score = groundObjects.Values.Count(x => x.team == teamScore.teamId && !x.isWall);
        }
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
                groundObj.unOccupiedColor = ColorUtility.TryParseHtmlString(ground.color, out Color color) ? color : new Color(1, 1, 1, 0.5f);
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

    void GeneratePlayers()
    {
        var players = gameConfig.configData.players;
        List<PlayerSelection> playerSelections = playerSelectionResult.playerSelections;
        GameObject playerParent = new GameObject("PlayerParent");
        foreach (var player in players)
        {
            PlayerSelection playerSelection = playerSelections.FirstOrDefault(ps => ps.skill == player.skill);
            PlayerObject playerPrefab = playerPrefabs.FirstOrDefault(p => p.skill == player.skill);
            PlayerObject playerObj = Instantiate(playerPrefab, new Vector3(player.location.x + 0.5f * player.location.width, player.location.y + 0.5f * player.location.width, -2), Quaternion.identity);
            playerObj.name = playerSelection.playerController.name;
            playerObj.transform.localScale = new Vector3(player.location.width, player.location.height, 1);
            playerObj.transform.parent = playerParent.transform;

            playerObj.team = playerSelection.playerController.team;
            playerObj.color = ColorUtility.TryParseHtmlString(playerSelection.playerController.color, out Color color) ? color : Color.white;
            playerObj.skill = player.skill;

            playerObj.moveUpKey = ParseKeyCode(playerSelection.playerController.keymap.up);
            playerObj.moveDownKey = ParseKeyCode(playerSelection.playerController.keymap.down);
            playerObj.moveLeftKey = ParseKeyCode(playerSelection.playerController.keymap.left);
            playerObj.moveRightKey = ParseKeyCode(playerSelection.playerController.keymap.right);
            playerObj.useSkillKey = ParseKeyCode(playerSelection.playerController.keymap.skill);
            playerObj.useItemKey = ParseKeyCode(playerSelection.playerController.keymap.item);

            if (!teamScoreList.Any(ts => ts.teamId == playerObj.team))
            {
                teamScoreList.Add(new TeamScore { teamId = playerObj.team, score = 0 });
            }
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over!");
        globalFreeze = true;
        GameResult gameResultObject = Instantiate(gameResult, new Vector3(0, 0, 0), Quaternion.identity);
        int team1Score = teamScoreList.FirstOrDefault(ts => ts.teamId == 1)?.score ?? 0;
        int team2Score = teamScoreList.FirstOrDefault(ts => ts.teamId == 2)?.score ?? 0;

        gameResultObject.team1Score = (float)team1Score / (team1Score + team2Score) * 100;
        gameResultObject.team2Score = (float)team2Score / (team1Score + team2Score) * 100;
        gameResultObject.team1Status = team1Score > team2Score ? "Victory" : "Defeat";
        gameResultObject.team2Status = team1Score < team2Score ? "Victory" : "Defeat";

        Destroy(playerSelectionResult.gameObject);
        SceneManager.LoadScene("GameOver");
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
            mainCamera.orthographicSize = halfGroundHeight + 12;
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

    void UpdateUI()
    {
        // Update showtime text
        showtimeText.text = $"{showTime:F0}";

        // Process and update teamScoreList text
        string processedTeamScores = ProcessTeamScores(teamScoreList);
        teamScoreListText.text = $"{processedTeamScores}";
    }

    string ProcessTeamScores(List<TeamScore> scores)
    {
        int team1Score = scores.FirstOrDefault(ts => ts.teamId == 1)?.score ?? 0;
        int team2Score = scores.FirstOrDefault(ts => ts.teamId == 2)?.score ?? 0;
        if (team1Score == 0 && team2Score == 0)
        {
            return "0 VS 0";
        }
        float team1Percentage = (float)team1Score / (team1Score + team2Score) * 100;
        float team2Percentage = (float)team2Score / (team1Score + team2Score) * 100;
        return $"{team1Percentage:F0} vs {team2Percentage:F0}";
    }
}

[System.Serializable]
public class TeamScore
{
    public int teamId;
    public int score;
}