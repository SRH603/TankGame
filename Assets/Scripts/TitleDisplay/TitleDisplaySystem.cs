using UnityEngine;
using System;

public class TitleDisplaySystem : MonoBehaviour
{
    [SerializeField] TitleDisplayView view;

    public void SetViewState(TitleDisplayState state)
    {
        view.SetState(state);
    }
    public void SetViewStateToHide()
    {
        view.SetState(TitleDisplayState.Hide);
    }
    public void SetViewStateToMain()
    {
        view.SetState(TitleDisplayState.Main);
    }
    public void SetViewStateToLocalMain()
    {
        view.SetState(TitleDisplayState.LocalMain);
    }
    public void SetViewStateToConfirmClearProgress()
    {
        view.SetState(TitleDisplayState.ConfirmClearProgress);
    }
    public void SetViewStateToMsgProgressCleared()
    {
        view.SetState(TitleDisplayState.MsgProgressCleared);
    }
    public void SetViewStateToPickLevel()
    {
        view.SetState(TitleDisplayState.PickLevel);
    }

    void Start()
    {
        view.InitStateMachine();
        SetViewState(TitleDisplayState.Hide);
        SetViewState(TitleDisplayState.Main);
    }
}


