using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public List<Action> actionSlots = new List<Action>();

    public void Init()
    {
        
    }

    ActionManager()
    {
        for (int i = 0; i < 4; i++)
        {
            Action a = new Action();
            a.input = (ActionInput)i;
            actionSlots.Add(a);
        }
    }


    public ActionInput GetAction(StateManager state)
    {

        if (state.rb)
        {
            return ActionInput.rb;
        }

        if (state.lb)
        {
            return ActionInput.lb;
        }

        if (state.rt)
        {
            return ActionInput.rt;
        }

        if (state.lt)
        {
            return ActionInput.lt;
        }

        return ActionInput.rb;
    }
}

[System.Serializable]
public class Action
{
    public ActionInput input;
    public string targetAnimation;
}

public enum ActionInput
{
    rb, lb, rt, lt
}
