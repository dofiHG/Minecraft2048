using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public Sprite[] sprites;
    public AudioSource music;
    public AudioSource effects;
    public AudioSource win;
    public AudioSource lose;
    public Image imageMusic;
    public Image imageEffects;

    public void ChangeMusicSound()
    {
        music.volume = music.volume == 1 ? 0 : 1;

        if (imageMusic.sprite == sprites[1]) { imageMusic.sprite = sprites[0]; }
        else { imageMusic.sprite = sprites[1]; }
    }

    public void ChangeEffectsSound()
    {
        effects.volume = effects.volume == 1 ? 0 : 1;
        win.volume = win.volume == 1 ? 0 : 1;
        lose.volume = lose.volume == 1 ? 0 : 1;

        if (imageEffects.sprite == sprites[1]) { imageEffects.sprite = sprites[0]; }
        else { imageEffects.sprite = sprites[1]; }
    }
}
