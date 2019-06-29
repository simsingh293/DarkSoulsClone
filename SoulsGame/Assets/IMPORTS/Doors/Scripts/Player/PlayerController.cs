using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;

    float _InputZ;
    float _InputX;

    Vector3 _MoveForce;
    public float _MoveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        _InputX = Input.GetAxis("Horizontal");
        _InputZ = Input.GetAxis("Vertical");

        _MoveForce = new Vector3(_InputX, 0, _InputZ);
        _MoveForce = _MoveForce * _MoveSpeed * Time.deltaTime;

        _rb.MovePosition(transform.position + _MoveForce);
    }
}
