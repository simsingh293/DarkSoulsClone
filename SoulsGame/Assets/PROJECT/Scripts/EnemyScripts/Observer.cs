using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    EnemyController _Unit;

    public bool _TeammateAttacked = false;

    public GameObject _SentPlayerObj;

    // Start is called before the first frame update
    void Start()
    {
        _Unit = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        OnNotify();
    }

    void OnNotify()
    {
        if (_TeammateAttacked)
        {
            _Unit.SetTarget(_SentPlayerObj.transform.position);
        }
    }
}
