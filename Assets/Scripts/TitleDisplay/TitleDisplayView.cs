using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [Serializable]
    public enum TitleDisplayState
    {
        Hide = 0,
        Main,
        LocalMain,
        ConfirmClearProgress,
        MsgProgressCleared,
        PickLevel,
        Num
    }
public class TitleDisplayView : MonoBehaviour
{
    [SerializeField] GameObject MainPanelObj;
    [SerializeField] GameObject LocalMainPanelObj;
    [SerializeField] GameObject ConfirmClearProgressPanelObj;
    [SerializeField] GameObject MsgProgressClearedPanelObj;
    [SerializeField] GameObject PickLevelPanelObj;
    [SerializeField] GameObject levelGridRoot;
    [SerializeField] GameObject levelGridChildPrefab;
    List<GameObject> panels = new List<GameObject>();
    private StateMachine stateMachine;

    public void SetState(TitleDisplayState state) => stateMachine.SetState((int)state);

    public void InitStateMachine()
    {
        panels.Add(MainPanelObj);
        panels.Add(LocalMainPanelObj);
        panels.Add(ConfirmClearProgressPanelObj);
        panels.Add(MsgProgressClearedPanelObj);
        panels.Add(PickLevelPanelObj);

        stateMachine = new StateMachine((int)TitleDisplayState.Num);

        stateMachine.SetStateFunctions((int)TitleDisplayState.Hide, EnterHideState, () => { }, () => { });
        stateMachine.SetStateFunctions((int)TitleDisplayState.Main, EnterMainState, () => { }, ExitMainState);
        stateMachine.SetStateFunctions((int)TitleDisplayState.LocalMain, EnterLocalMainState, () => { }, ExitLocalMainState);
        stateMachine.SetStateFunctions((int)TitleDisplayState.ConfirmClearProgress, EnterConfirmClearProgressState, () => { }, ExitConfirmClearProgressState);
        stateMachine.SetStateFunctions((int)TitleDisplayState.MsgProgressCleared, EnterMsgProgressCleared, () => { }, ExitMsgProgressCleared);
        stateMachine.SetStateFunctions((int)TitleDisplayState.PickLevel, EnterPickLevelState, () => { }, ExitPickLevelState);
    }
    private void HideAllPanel()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
    private void EnterHideState() => HideAllPanel();
    private void EnterMainState() => MainPanelObj.SetActive(true);
    private void ExitMainState() => MainPanelObj.SetActive(false);
    private void EnterLocalMainState()
    {
        GameManager.instance.IsLocal = true;
        if (GameDataProcessor.HasProgress()) LocalMainPanelObj.SetActive(true);
        else SetState(TitleDisplayState.PickLevel);
    }
    private void ExitLocalMainState() => LocalMainPanelObj.SetActive(false);
    private void EnterConfirmClearProgressState() => ConfirmClearProgressPanelObj.SetActive(true);
    private void ExitConfirmClearProgressState() => ConfirmClearProgressPanelObj.SetActive(false);
    private void EnterMsgProgressCleared()
    {
        GameManager.instance.ClearProgress();
        MsgProgressClearedPanelObj.SetActive(true);
    }
    private void ExitMsgProgressCleared() => MsgProgressClearedPanelObj.SetActive(false);
    private void EnterPickLevelState() => StartCoroutine(GenerateLevelGrid());
    private void ExitPickLevelState()
    {
        StopCoroutine(GenerateLevelGrid());
        PickLevelPanelObj.SetActive(false);
    }
    private IEnumerator GenerateLevelGrid()
    {
            Debug.Log($"Create button");

        for (int i = 0; i < levelGridRoot.transform.childCount; i++)
        {
            Destroy(levelGridRoot.transform.GetChild(i).gameObject);
        }
        yield return new WaitForEndOfFrame();
        GameData gameData = GameManager.instance.CurrentGameData;
        for (int i = 0; i < GameManager.instance.levelInfos.Count; i++)
        {
            Debug.Log($"Create button i ={i}");
            LevelInfo levelInfo = GameManager.instance.levelInfos[i];
            LevelButton levelButton = Instantiate(levelGridChildPrefab, levelGridRoot.transform).GetComponent<LevelButton>();
            if (i == 0) levelButton.Init(levelInfo, i, false);
            else levelButton.Init(levelInfo, i, !gameData.levelProgressList[i - 1].isCleared);
        }

        PickLevelPanelObj.SetActive(true);
    }
    // Update is called once per frame
    void Update() => stateMachine.UpdateState();
}

