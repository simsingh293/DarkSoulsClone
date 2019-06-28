using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    float vertical;
    float horizontal;

    // controller Variables
    bool b_Input; // or B/O button
    bool a_Input;
    bool x_Input;
    bool y_Input;

    bool rb_Input;
    bool rt_Input;
    float rt_axis;
    bool lb_Input;
    bool lt_Input;
    float lt_axis;

    bool leftAxis_down;
    bool rightAxis_down;

    float b_timer;
    float rt_timer;
    float lt_timer;

    StateManager states;
    CameraManager cameraManager;

    float delta;

    // Start is called before the first frame update
    void Start()
    {
        states = GetComponent<StateManager>();
        states.Init();

        cameraManager = CameraManager.singleton;
        cameraManager.Init(states);
    }


    private void FixedUpdate()
    {
        delta = Time.fixedDeltaTime;
        UpdateStates();
        states.FixedTick(delta);
        cameraManager.Tick(delta);

        
        ResetInputAndStates();
    }

    void Update()
    {
        delta = Time.deltaTime;
        states.Tick(delta);
        GetInput();

        Debug.Log(b_timer);
    }




    void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");



        a_Input = Input.GetButton("A");
        b_Input = Input.GetButton("B");
        x_Input = Input.GetButton("X");
        y_Input = Input.GetButtonUp("Y");

        rb_Input = Input.GetButton("RB");
        lb_Input = Input.GetButton("LB");
        rt_Input = Input.GetButton("RT"); // for keyboard
        lt_Input = Input.GetButton("LT"); // for keyboard

        rightAxis_down = Input.GetButtonUp("LockOn");

        if (b_Input)
        {
            b_timer += delta;
        }

        //rt_axis = Input.GetAxis("RT"); // for controller
        //if(rt_axis != 0)
        //{
        //    rt_Input = true;
        //}
        
        //lt_axis = Input.GetAxis("LT"); // for controller
        //if (lt_axis != 0)
        //{
        //    lt_Input = true;
        //}

        //Debug.Log(rb_Input);
    }

    void UpdateStates()
    {
        states.horizontal = horizontal;
        states.vertical = vertical;

        Vector3 v = vertical * cameraManager.transform.forward;
        Vector3 h = horizontal * cameraManager.transform.right;
        states.moveDir = (v + h).normalized;

        float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        states.moveAmount = Mathf.Clamp01(m);

        //states.dodgeInput = b_Input;


        if (b_Input && b_timer > 0.5f)
        {
            states.run = (states.moveAmount > 0);
        }

        if (!b_Input && b_timer > 0 && b_timer < 0.5f)
        {
            states.dodgeInput = true;
        }        

        
        states.rb = rb_Input;
        states.rt = rt_Input;
        states.lb = lb_Input;
        states.lt = lt_Input;


        if (y_Input)
        {
            states.isTwoHanded = !states.isTwoHanded;
            states.HandleTwoHanded();
            y_Input = false;
        }

        if (rightAxis_down)
        {
            
            states.lockOn = !states.lockOn;

            if(states.lockOnTarget == null)
            {
                states.lockOn = false;
            }

            cameraManager.lockOnTarget = states.lockOnTarget;
            states.lockOnTransform = cameraManager.lockOnTransform;
            cameraManager.lockon = states.lockOn;

            

            rightAxis_down = false;
        }
    }

    void ResetInputAndStates()
    {
        if (!b_Input)
        {
            b_timer = 0;
        }

        if (states.dodgeInput)
        {
            states.dodgeInput = false;
        }

        if (states.run)
        {
            states.run = false;
        }
    }
}
