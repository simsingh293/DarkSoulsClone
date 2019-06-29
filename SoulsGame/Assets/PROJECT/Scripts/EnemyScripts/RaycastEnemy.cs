using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastEnemy : MonoBehaviour
{
    RaycastHit hit;
    Rigidbody _rb;

    bool playerSighted = false;
    bool ray;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(0, 9);
    }

    // Update is called once per frame
    void Update()
    {
        ray = Physics.Raycast(transform.position, transform.forward, out hit, 10, (1 << 8));

        Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow);

        if (ray)
        {
            playerSighted = true;
            
            _rb.velocity = _rb.velocity + transform.forward;
        }
        else
        {
            playerSighted = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + 1, 0));

        }
    }

    
}
