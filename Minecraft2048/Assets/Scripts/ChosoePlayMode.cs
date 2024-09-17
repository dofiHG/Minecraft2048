using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using YG;

public class ChosoePlayMode : MonoBehaviour
{
    public static ChosoePlayMode instance;
    public static int lvl;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ChangeGameMode()
    {
        lvl = Convert.ToInt32(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text.Substring(0, 1)); 
        if (lvl == 3 || lvl == 4) { YandexGame.savesData.cellSize = 180; YandexGame.savesData.spacing = 20; }
        else if (lvl == 5) { YandexGame.savesData.cellSize = 130; YandexGame.savesData.spacing = 20; }
        else { YandexGame.savesData.cellSize = 100; YandexGame.savesData.spacing = 7; }
        YandexGame.savesData.tempLvL = lvl;
        YandexGame.savesData.fieldSize = lvl;
        YandexGame.SaveProgress();

        Field.instance.fieldSize = YandexGame.savesData.fieldSize;
        Field.instance.cellSize = YandexGame.savesData.cellSize;
        Field.instance.spacing = YandexGame.savesData.spacing;

        GameController.instance.StartGame();
        PanelManager.instance.OkButton(PanelManager.instance.mainPanel.transform);
    }
}
