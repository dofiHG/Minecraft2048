using System;
using TMPro;
using UnityEngine;
using YG;

public class GameController : MonoBehaviour
{
    public AudioSource win;
    public AudioSource lose;

    public static GameController instance;
    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }

    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text bestPointsText;

    private void Start()
    {
        if (ChosoePlayMode.lvl == 0)
        {
            pointsText.text = "0";
            bestPointsText.text = "0";
            ChosoePlayMode.lvl = 4;
        }
        StartGame();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void StartGame()
    {
        bestPointsText.text = "0";
        SetPoints(0);
        switch (YandexGame.savesData.tempLvL)
        {
            case 0:
                bestPointsText.text = YandexGame.savesData.best4x4.ToString();
                break;
            case 3:
                bestPointsText.text = YandexGame.savesData.best3x3.ToString();
                break;
            case 4:
                bestPointsText.text = YandexGame.savesData.best4x4.ToString();
                break;
            case 5:
                bestPointsText.text = YandexGame.savesData.best5x5.ToString();
                break;
            case 7:
                bestPointsText.text = YandexGame.savesData.best7x7.ToString();
                break;
        }
        if (PanelManager.isRetry)
            PanelManager.instance.OkButton(GameObject.Find("StartEndPanel").GetComponent<Transform>());
        GameStarted = true;
        Field.instance.GenerateField();
        PanelManager.isRetry = false;
    }

    public void Win()
    {
        GameStarted = false;
        PanelManager.instance.WinEvent();
        win.Play();
        YandexGame.SaveProgress();
    }

    public void Lose()
    {
        GameStarted = false;
        PanelManager.instance.LoseEvent();
        lose.Play();
        YandexGame.SaveProgress();
    }

    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }

    private void SetPoints(int points)
    {
        Points = points;
        pointsText.text = Points.ToString();
        if (points > Convert.ToInt32(bestPointsText.text)) 
        {
            bestPointsText.text = points.ToString();
            switch (ChosoePlayMode.lvl)
            {
                case 3:
                    YandexGame.savesData.best3x3 = points;
                    break;
                case 4:
                    YandexGame.savesData.best4x4 = points;
                    break;
                case 5:
                    YandexGame.savesData.best5x5 = points;
                    break;
                case 7:
                    YandexGame.savesData.best7x7 = points;
                    break;
            }
            YandexGame.SaveProgress();
        }
    }
}
