using UnityEngine;

public class SpritesManager : MonoBehaviour
{
    public static SpritesManager Instance;

    public Sprite[] cellSprites;

    public Color pointsDarkColor;
    public Color pointsLightColor;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
