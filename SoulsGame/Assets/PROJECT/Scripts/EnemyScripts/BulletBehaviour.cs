using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody _rb;
    public float _ShotForce = 10.0f;
    public Vector3 _BulletSize = new Vector3(.3f, .3f, .3f);

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        //transform.localScale = _BulletSize;

    }

    private void Start()
    {
        StopCoroutine(Disable());
        _rb.AddForce(transform.forward * _ShotForce, ForceMode.Impulse);
        StartCoroutine(Disable());
    }


    //private void OnEnable()
    //{
    //    StopCoroutine(Disable());
    //    _rb.AddForce(transform.forward * _ShotForce, ForceMode.Impulse);
    //    StartCoroutine(Disable());
    //}

    public void SetShotForce(float force)
    {
        _ShotForce = force;
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(2);
        Destroy(this);
        //transform.parent = GameObject.Find("Magazine").GetComponent<Transform>();
    }
}
