using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    float vertical;
    float horizontal;

    // controller Variables
    bool runInput; // or B/O button
    bool a_Input;
    bool x_Input;
    bool y_Input;

    bool rb_Input;
    bool rt_Input;
    float rt_axis;
    bool lb_Input;
    bool lt_Input;
    float lt_axis;

    StateManager states;
    CameraManager cameraManager;

    float delta;

    // Start is called before the first frame update
    void Start()
    {
        states = GetComponent<StateManager>();
        states.Init();

        cameraManager = CameraManager.singleton;
        cameraManager.Init(this.transform);
    }


    private void FixedUpdate()
    {
        delta = Time.fixedDeltaTime;
        GetInput();
        UpdateStates();
        states.FixedTick(delta);
        cameraManager.Tick(delta);

    }

    void Update()
    {
        delta = Time.deltaTime;
        states.Tick(delta);
    }




    void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        runInput = Input.GetButton("RunInput");

        rb_Input = Input.GetButton("RB");

        lb_Input = Input.GetButton("LB");
        
        rt_Input = Input.GetButton("RT"); // for keyboard
        //rt_axis = Input.GetAxis("RT"); // for controller
        //if(rt_axis != 0)
        //{
        //    rt_Input = true;
        //}
        
        lt_Input = Input.GetButton("LT"); // for keyboard
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


        if (runInput)
        {
            states.run = (states.moveAmount > 0);
        }
        else
        {
            states.run = false;
        }

        
        states.rb = rb_Input;
        states.rt = rt_Input;
        states.lb = lb_Input;
        states.lt = lt_Input;


    }
}
