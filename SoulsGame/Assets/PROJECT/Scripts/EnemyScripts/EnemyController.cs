using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent _agent;

    private Vector3 _StartPosition;
    public Vector3 _TargetPosition;

    public bool _IsAttacked = false;
    public bool _IsNotified = false;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _StartPosition = transform.position;
        _TargetPosition = _StartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
    }

    void MoveTo()
    {
        _agent.destination = _TargetPosition;
    }

    public void SetTarget(Vector3 vector3)
    {
        _TargetPosition = vector3;
    }
}
