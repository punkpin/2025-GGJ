using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : MonoBehaviour
{
    public float team1Score;
    public float team2Score;
    public string team1Status;
    public string team2Status;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
