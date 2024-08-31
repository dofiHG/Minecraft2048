using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CellAnimation : MonoBehaviour
{
    public static bool isAnimation;

    [SerializeField] private Image image;
    [SerializeField] private TMP_Text points;

    private float moveTime = .1f;
    private float appearTime = .1f;

    private Sequence sequence;

    public void Move(Cell from, Cell to, bool isMerging)
    {
        isAnimation = true;
        from.CancelAnimation();
        to.SetAnimation(this);

        image.sprite = SpritesManager.Instance.cellSprites[from.Value];
        image.color = from.Value > 0 ? new Color32(255, 255, 255, 142) : new Color32(170, 170, 170, 255);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(Field.instance.cellSize, Field.instance.cellSize);
        image.GetComponentInChildren<TMP_Text>().GetComponent<RectTransform>().sizeDelta = new Vector2(Field.instance.cellSize - 15, Field.instance.cellSize - 15);
        points.font = Field.instance.fontAsset;
        points.text = from.Points.ToString();

        transform.position = from.transform.position;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(to.transform.position, moveTime).SetEase(Ease.InOutQuad));

        if (isMerging)
        {
            sequence.AppendCallback(() =>
            {
                image.sprite = SpritesManager.Instance.cellSprites[to.Value];
                points.text = to.Points.ToString();
                image.color = to.Value > 0 ? new Color32(255, 255, 255, 255) : new Color32(170, 170, 170, 255);
            });

            sequence.Append(transform.DOScale(1.2f, appearTime));
            sequence.Append(transform.DOScale(1, appearTime));
        }

        sequence.AppendCallback(() =>
        {
            to.UpdateCell();
            Destroy();
        });
    }

    public void Appear(Cell cell)
    {
        cell.CancelAnimation();
        cell.SetAnimation(this);

        image.sprite = SpritesManager.Instance.cellSprites[cell.Value];
        image.color = cell.Value > 0 ? new Color32(255, 255, 255, 255) : new Color32(170, 170, 170, 255);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(Field.instance.cellSize, Field.instance.cellSize);
        image.GetComponentInChildren<TMP_Text>().GetComponent<RectTransform>().sizeDelta = new Vector2(Field.instance.cellSize - 15, Field.instance.cellSize - 15);
        points.font = Field.instance.fontAsset;
        points.text = cell.Points.ToString();

        transform.position = cell.transform.position;
        transform.localScale = Vector2.zero;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(1.2f, appearTime * 2));
        sequence.Append(transform.DOScale(1f, appearTime * 2));
        sequence.AppendCallback(() =>
        {
            cell.UpdateCell();
            Destroy();
        });
        isAnimation = false;
    }

    public void Destroy()
    {
        sequence.Kill();
        Destroy(gameObject);
    }
}
