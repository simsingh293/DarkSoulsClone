using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    EnemyController _Unit;

    GameObject[] _EnemyOBJ;
    public List<Observer> _EnemyObservers = new List<Observer>();

    public bool _IsAttacked = false;

    public GameObject _Player;

    // Start is called before the first frame update
    void Start()
    {
        _Unit = GetComponent<EnemyController>();

        AddObservers();
    }

    // Update is called once per frame
    void Update()
    {
        Notify();
    }

    void Notify()
    {
        if (_IsAttacked)
        {
            for (int i = 0; i < _EnemyObservers.Count; i++)
            {
                _EnemyObservers[i]._TeammateAttacked = true;
                _EnemyObservers[i]._SentPlayerObj = _Player;
            }
        }
    }


    void AddObservers()
    {
        _EnemyOBJ = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < _EnemyOBJ.Length; i++)
        {
            _EnemyObservers.Add(_EnemyOBJ[i].GetComponent<Observer>());
        }
    }
}
