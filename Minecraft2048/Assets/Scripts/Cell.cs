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

    private CellAnimation currentAnimation;
    public void SetValue(int x, int y, int value, bool updateUI = true)
    {
        X = x;
        Y = y;
        Value = value;

        if (updateUI)
            UpdateCell();
    }

    public void IncreaseValue()
    {
        Value++;
        HasMerged = true;

        GameController.instance.AddPoints(Points);
    }

    public void ResetFlags()
    {
        HasMerged = false;
    } 

    public void MergeWithCell(Cell otherCell)
    {
        CellAnimationController.Instance.SmoothTrasition(this, otherCell, true);

        otherCell.IncreaseValue();
        SetValue(X, Y, 0);
    }

    public void MoveToCell(Cell target)
    {
        CellAnimationController.Instance.SmoothTrasition(this, target, false);

        target.SetValue(target.X, target.Y, Value, false);
        SetValue(X, Y, 0);
    }

    public void UpdateCell()
    {
        points.text = IsEmpty ? string.Empty: Points.ToString();

        image.color = Value > 0 ? new Color32(255, 255, 255, 255) : new Color32(170, 170, 170, 255);
        image.sprite = SpritesManager.Instance.cellSprites[Value];
    }

    public void SetAnimation(CellAnimation animation)
    {
        currentAnimation = animation;
    }

    public void CancelAnimation()
    {
        if (currentAnimation != null)
            currentAnimation.Destroy();
    }
}
