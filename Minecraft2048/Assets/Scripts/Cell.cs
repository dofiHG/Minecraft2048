using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Value { get; set; }
    public int Points => IsEmpty ? 0 : (int)Mathf.Pow(2, Value);
    
    public bool IsEmpty => Value == 0;
    public bool HasMerged { get; private set; }

    public const int MaxValue = 11;

    [SerializeField] private Image image;
    [SerializeField] private TMP_Text points;

    public void SetValue(int x, int y, int value)
    {
        X = x;
        Y = y;
        Value = value;
        UpdateCell();
    }

    public void IncreaseValue()
    {
        Value++;
        HasMerged = true;

        GameController.instance.AddPoints(Points);

        UpdateCell();
    }

    public void ResetFlags()
    {
        HasMerged = false;
    } 

    public void MergeWithCell(Cell otherCell)
    {
        otherCell.IncreaseValue();
        SetValue(X, Y, 0);

        UpdateCell();
    }

    public void MoveToCell(Cell target)
    {
        target.SetValue(target.X, target.Y, Value);
        SetValue(X, Y, 0);

        UpdateCell();
    }

    public void UpdateCell()
    {
        points.text = IsEmpty ? string.Empty: Points.ToString();
    }
}
