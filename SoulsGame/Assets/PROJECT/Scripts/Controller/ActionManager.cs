using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public List<Action> actionSlots = new List<Action>();

    StateManager states;

    public void Init(StateManager state)
    {
        states = state;

        UpdateActionsOneHanded();
    }

    public void UpdateActionsOneHanded()
    {
        EmptyAllSlots();

        Weapon w = states.inventoryManager.curWeapon;


        for (int i = 0; i < w.actions.Count; i++)
        {
            Action a = GetAction(w.actions[i].input);
            a.targetAnimation = w.actions[i].targetAnimation;
        }
    }

    public void UpdateActionsTwoHanded()
    {
        EmptyAllSlots();

        Weapon w = states.inventoryManager.curWeapon;


        for (int i = 0; i < w.two_handedActions.Count; i++)
        {
            Action a = GetAction(w.two_handedActions[i].input);
            a.targetAnimation = w.two_handedActions[i].targetAnimation;
        }
    }

    void EmptyAllSlots()
    {
        for (int i = 0; i < 4; i++)
        {
            Action a = GetAction((ActionInput)i);
            a.targetAnimation = null;
        }
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

    public Action GetActionSlot(StateManager state)
    {
        ActionInput action = GetActionInput(state);

        return GetAction(action);
    }

    Action GetAction(ActionInput input)
    {
        for (int i = 0; i < actionSlots.Count; i++)
        {
            if(actionSlots[i].input == input)
            {
                return actionSlots[i];
            }
        }

        return null;
    }


    public ActionInput GetActionInput(StateManager state)
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
