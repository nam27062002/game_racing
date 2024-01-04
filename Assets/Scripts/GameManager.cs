using System;
using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : Singleton<GameManager>
{
    public const float Range = 2f;
    public TextMeshProUGUI tapToPlayText;
    public TextMeshProUGUI countDownText;
    public GameObject player;
    public TextMeshProUGUI scoreText;
    public GameObject scoreObj;
    public GameObject endGame;
    public TextMeshProUGUI endGameScore;
    public TextMeshProUGUI vicText;
    public TextMeshProUGUI vicText2;
    public GameObject star;
    private int _score;
    public enum State
    {
        Waiting,
        PrepareStartGame,
        StartGame,
        Pause,
        Lose
    }
    
    public State gameStatus;
    
    
    protected override void Awake()
    {
        base.Awake();
        gameStatus = State.Waiting;
        countDownText.text = "";
        player.SetActive(false);
        _score = 0;
        SetScoreText();
        scoreObj.SetActive(false);
        endGame.SetActive(false);
    }
    
    private void Update()
    {
        switch (gameStatus)
        {
            case State.Waiting:
                if (Input.anyKeyDown)
                {
                    tapToPlayText.text = "";
                    gameStatus = State.PrepareStartGame;
                    StartCoroutine(SetCountdown());
                    player.SetActive(true);
                }
                break;
        }
    }

    private IEnumerator SetCountdown()
    {
        var countdown = 3;
        while (countdown > 0)
        {
            countDownText.text = countdown.ToString();
            countdown--;
            yield return new WaitForSeconds(1);
        }

        countDownText.text = "";
        gameStatus = State.StartGame;
        scoreObj.SetActive(true);
    }

    private void SetScoreText()
    {
        scoreText.text = "Score: " + _score;
    }

    public void IncreaseScore()
    {
        _score++;
        SetScoreText();
    }

    public void SetEndGameScore()
    {
        endGameScore.text = _score.ToString();
    }


    public void EndGameHandle()
    {
        Debug.Log(APIHandler.Instance.GetPromoByScore(_score).GetString);
        // if (_score >= 10)
        // {
        //     vicText.text = "VICTORY";
        //     star.SetActive(true);
        //     vicText2.text = "Congratulations on receiving the voucher";
        //     // StartCoroutine(CallApi());
        // }
        // else
        // {
        //     vicText.text = "LOSE";
        //     star.SetActive(false);
        //     vicText2.text = "Better luck next time!";
        // }
       
    }
    
    
}



