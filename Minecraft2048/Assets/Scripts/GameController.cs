using TMPro;
using TMPro.Examples;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static int Points { get; private set; }
    public static bool GameStarted { get; private set; }

    [SerializeField] private TMP_Text gameResult;
    [SerializeField] private TMP_Text pointsText;

    private void Start()
    {
        StartGame();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void StartGame()
    {
        gameResult.text = "";
        GameStarted = true;
        SetPoints(0);

        Field.instance.GenerateField();
    }

    public void Win()
    {
        GameStarted = false;
        gameResult.text = "You Win!";
    }

    public void Lose()
    {
        GameStarted = false;
        gameResult.text = "You Lose!";
    }

    public void AddPoints(int points)
    {
        SetPoints(Points + points);
    }

    private void SetPoints(int points)
    {
        Points = points;
        pointsText.text = Points.ToString();
    }
}
