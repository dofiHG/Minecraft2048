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
        bestPointsText.text = YandexGame.savesData.best.ToString();
        StartGame();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void StartGame()
    {
        if (PanelManager.isRetry)
            PanelManager.instance.OkButton(GameObject.Find("StartEndPanel").GetComponent<Transform>());
        GameStarted = true;
        SetPoints(0);
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
        if (points > YandexGame.savesData.best) { YandexGame.savesData.best = points; bestPointsText.text = points.ToString(); }
    }
}
