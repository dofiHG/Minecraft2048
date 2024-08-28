using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text points;

    private float moveTime = .1f;
    private float appearTime = .1f;

    private Sequence sequence;

    public void Move(Cell from, Cell to, bool isMerging)
    {
        from.CancelAnimation();
        to.SetAnimation(this);

        image.sprite = SpritesManager.Instance.cellSprites[from.Value];
        points.text = from.Points.ToString();
        image.color = from.Value > 0 ? new Color32(255, 255, 255, 142) : new Color32(255, 153, 153, 255);

        transform.position = from.transform.position;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(to.transform.position, moveTime).SetEase(Ease.InOutQuad));

        if (isMerging)
        {
            sequence.AppendCallback(() =>
            {
                image.sprite = SpritesManager.Instance.cellSprites[to.Value];
                points.text = to.Points.ToString();
                image.color = to.Value > 0 ? new Color32(255, 255, 255, 255) : new Color32(255, 153, 153, 255);
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
        image.color = cell.Value > 0 ? new Color32(255, 255, 255, 255) : new Color32(255, 153, 153, 255);
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
    }

    public void Destroy()
    {
        sequence.Kill();
        Destroy(gameObject);
    }
}
