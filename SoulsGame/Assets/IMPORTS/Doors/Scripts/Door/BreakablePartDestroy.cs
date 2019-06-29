using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePartDestroy : MonoBehaviour
{
    public Rigidbody _rb;

    public bool _InMotion = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_rb.velocity != Vector3.zero)
        {
            _InMotion = true;
        }

        if(_rb.velocity == Vector3.zero && _InMotion)
        {
            Destroy(gameObject, 2);
        }
    }
}
