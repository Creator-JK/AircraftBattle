using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : Singleton<UImanager>
{
    private readonly Dictionary<string, GameObject> UI = new();
    private readonly string StartGameText = "开始游戏";
    private readonly string ContinueGametext = "继续游戏";
    private Text StartGame;

    protected override void Awake()
    {
        base.Awake();
        UI.Add("Button", transform.Find("Button").gameObject);
        UI.Add("StartGame", transform.Find("Button/StartGame").gameObject);
        UI.Add("QuitGame", transform.Find("Button/QuitGame").gameObject);
        StartGame = UI["StartGame"].transform.Find("StartGameText").GetComponent<Text>();
    }

    private void Update()
    {
        if (GameManager.Instance.isPlaying && !GameManager.Instance.isPaused)
            UI["Button"].SetActive(false);
        else
            UI["Button"].SetActive(true);

        if (GameManager.Instance.isPaused)
            StartGame.text = ContinueGametext;
        else
            StartGame.text = StartGameText;
    }
}
