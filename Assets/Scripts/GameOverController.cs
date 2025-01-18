using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public GameResult gameResult;

    public float animationTime = 3.0f;

    public TextMeshProUGUI team1Status;
    public TextMeshProUGUI team1Score;

    public TextMeshProUGUI team2Status;
    public TextMeshProUGUI team2Score;

    public TextMeshProUGUI hint;

    public GameObject bar;

    private float targetBarPosition;
    private float countdown;
     
    void Start()
    {
        countdown = 2.0f * animationTime;
        gameResult = FindObjectOfType<GameResult>();
        targetBarPosition = (gameResult.team1Score - gameResult.team2Score) / 100.0f * 160.0f;
        team1Status.text = "";
        team2Status.text = "";
        team1Score.text = "";
        team2Score.text = "";
        hint.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown > animationTime)
        {
            float t = 1 - (countdown - animationTime) / animationTime;

            // Update bar position
            RectTransform barRectTransform = bar.GetComponent<RectTransform>();
            Vector3 barPosition = barRectTransform.anchoredPosition;
            barPosition.x = Mathf.Lerp(0, targetBarPosition, t);
            barRectTransform.anchoredPosition = barPosition;

            // Update team scores
            team1Score.text = Mathf.Lerp(50, gameResult.team1Score, t).ToString("0");
            team2Score.text = Mathf.Lerp(50, gameResult.team2Score, t).ToString("0");
            Debug.Log("team1Score: " + team1Score.text);
            Debug.Log("team2Score: " + team2Score.text);
            Debug.Log("barPosition: " + barPosition.x);
        }
        else if (countdown > 0)
        {
            team1Status.text = gameResult.team1Status;
            team2Status.text = gameResult.team2Status;
        }
        else 
        {
            hint.text = "Press any key to continue";
            if (Input.anyKeyDown)
            {
                Destroy(gameResult.gameObject);
                SceneManager.LoadScene("PlayerSelection");
            }
        }
    }
}
