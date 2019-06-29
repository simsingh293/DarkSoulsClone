using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHook : MonoBehaviour
{
    Animator _anim;
    StateManager states;

    public float rootMotionMultiplier;
    bool rolling;
    float roll_t;



    public void Init(StateManager st)
    {
        states = st;
        _anim = states._anim;

    }

    public void InitForRoll()
    {
        rolling = true;
        roll_t = 0;
    }

    public void CloseRoll()
    {
        if (!rolling)
        {
            return;
        }

        rootMotionMultiplier = 1;
        roll_t = 0;
        rolling = false;
    }

    private void OnAnimatorMove()
    {
        // to account for root motion in the animation
        if (states.canMove)
        {
            return;
        }

        states._rb.drag = 0;

        if(rootMotionMultiplier == 0)
        {
            rootMotionMultiplier = 1;
        }



        if (!rolling)
        {
            Vector3 delta = _anim.deltaPosition;
            delta.y = 0;
            Vector3 v = (delta * rootMotionMultiplier) / states.delta;
            states._rb.velocity = v; 
        }
        else
        {
            roll_t += states.delta;

            if(roll_t > 1)
            {
                roll_t = 1;
            }

            float zValue = states.roll_curve.Evaluate(roll_t);
            Vector3 v1 = Vector3.forward * zValue;
            Vector3 relative = transform.TransformDirection(v1);
            Vector3 v2 = (relative * rootMotionMultiplier);
            states._rb.velocity = v2;
        }
    }

    public void LateTick()
    {

    }
}
