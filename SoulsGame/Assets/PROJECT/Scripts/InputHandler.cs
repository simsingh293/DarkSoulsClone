using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    float vertical;
    float horizontal;
    bool runInput;

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
    }
}
