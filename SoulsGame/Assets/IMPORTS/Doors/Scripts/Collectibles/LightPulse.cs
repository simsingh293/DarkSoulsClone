using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightPulse : MonoBehaviour
{
    public float MaxRange = 35.0f;
    public float Duration;
    public Light lt;


    public bool increasing = true;

    

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        float pi = (Time.time / Duration) * 2 * Mathf.PI;
        float amplitude = Mathf.Cos(pi) * 0.5f + 0.5f;
        //lt.intensity += amplitude + 1;

        if(lt.intensity > MaxRange)
        {
            increasing = false;
            

        } 
        else if (lt.intensity < 10)
        {
            increasing = true;
            
        }

        if (increasing)
        {
            lt.intensity += amplitude + 1;
        }
        else
        {
            lt.intensity += (-amplitude) - 1;
        }
    }
}
