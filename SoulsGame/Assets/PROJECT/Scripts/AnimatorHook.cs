using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHook : MonoBehaviour
{
    Animator _anim;
    StateManager states;


    public void Init(StateManager st)
    {
        states = st;
        _anim = states._anim;
    }

    private void OnAnimatorMove()
    {
        // to account for root motion in the animation
        if (states.canMove)
        {
            return;
        }

        states._rb.drag = 0;
        float multiplier = 1;


        Vector3 delta = _anim.deltaPosition;
        delta.y = 0;
        Vector3 v = (delta * multiplier) / states.delta;
        states._rb.velocity = v;
    }

    public void LateTick()
    {

    }
}
