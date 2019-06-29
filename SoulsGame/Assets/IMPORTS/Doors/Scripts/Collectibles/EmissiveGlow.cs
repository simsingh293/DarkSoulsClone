using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveGlow : MonoBehaviour
{
    public Material _mat;

    public float Duration = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        _mat = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float pi = (Time.time / Duration) * 2 * Mathf.PI;
        float amplitude = Mathf.Cos(pi) * 0.5f + 0.5f;
        float R = amplitude;
        float B = amplitude;


        _mat.SetColor("_EmissionColor", new Color(0, R, 0));
    }
}
