using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class PanelManager : MonoBehaviour
{
    public static PanelManager instance;

    public GameObject mainPanel;
    public GameObject modesPanel;
    public GameObject settingsPanel;
    public TMP_Text adviceText;
    public TMP_Text loseText;
    public TMP_Text winText;
    public TMP_Text musicText;
    public TMP_Text effectsText;
    public TMP_Text retryText;
    public Button musicButton;
    public Button effectsButton;
    public Button retryButton;
    public Image musicImage;
    public Image effectsImage;
    public Image retryImage;

    public static bool isRetry;

    private void Awake()
    {
        if (instance == null) { instance = this; }

        mainPanel.SetActive(true);
        modesPanel.SetActive(true);
        foreach (Transform child in modesPanel.transform)
        {
            child.gameObject.SetActive(true);
            foreach (Transform child2 in child)
                child2.gameObject.SetActive(true);
        }
        adviceText.gameObject.SetActive(true);
        YandexGame.savesData.isFirstSession = false;
        YandexGame.SaveProgress();
    }

    public void WinEvent()
    {
        mainPanel.SetActive(true);
        modesPanel.SetActive(true);
        foreach (Transform child in modesPanel.transform)
        {
            child.gameObject.SetActive(true);
            foreach (Transform child2 in child)
                child2.gameObject.SetActive(true);
        }
        winText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        retryImage.gameObject.SetActive(true);
        retryText.gameObject.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void LoseEvent()
    {
        mainPanel.SetActive(true);
        modesPanel.SetActive(true);
        foreach (Transform child in modesPanel.transform)
        {
            child.gameObject.SetActive(true);
            foreach (Transform child2 in child)
                child2.gameObject.SetActive(true);
        }
        loseText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        retryImage.gameObject.SetActive(true);
        retryText.gameObject.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void OkButton(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(false);
            OkButton(child);
        }
        
        parent.gameObject.SetActive(false);

        musicButton.gameObject.SetActive(true);
        effectsButton.gameObject.SetActive(true);
        musicImage.gameObject.SetActive(true);
        effectsImage.gameObject.SetActive(true);
        effectsText.gameObject.SetActive(true);
        musicText.gameObject.SetActive(true);
    }

    public void RetryButton()
    {
        isRetry = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
