
using System;
using UnityEngine;

public class GameState<T> : StateMachineBehaviour
{
    private GameObject _state;

    public override void OnStateEnter(Animator controller, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(controller, stateInfo, layerIndex);
        _state = controller.getState(typeof(T).Name);
        _state.SetActive(true);
    }

    public override void OnStateExit(Animator controller, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(controller, stateInfo, layerIndex);
        _state.SetActive(false);
    }
}

public static class GameStateExtensions
{
    public static GameObject getState(this Animator controller, string stateName)
    {
        var obj = controller.transform.Find(stateName).gameObject;
        if (obj == null)
        {
            throw new NullReferenceException(stateName);
        }
        return obj;
    }
}

