using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTriggers : MonoBehaviour
{
    private GameObject[] _TriggerArray;
    public List<GameObject> _Triggers = new List<GameObject>();

    public GameObject Door;
    public Animator _anim;

    public bool _AllActive = false;
    // Start is called before the first frame update
    void Start()
    {
        _TriggerArray = GameObject.FindGameObjectsWithTag("MultipleDoorTrigger");
        for (int i = 0; i < _TriggerArray.Length; i++)
        {
            _Triggers.Add(_TriggerArray[i]);
        }

        _anim = Door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTriggers();
    }


    public void ActivateDoor()
    {
        _anim.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        _anim.SetBool("Open", false);
    }


    void CheckTriggers()
    {
        int count = 0;
        for (int i = 0; i < _Triggers.Count; i++)
        {
            if (_Triggers[i].GetComponent<Renderer>().material.color == Color.red)
            {
                count++;
            }
        }

        if(count == 3)
        {
            _AllActive = true;  
            ActivateDoor();
        }
    }
}
