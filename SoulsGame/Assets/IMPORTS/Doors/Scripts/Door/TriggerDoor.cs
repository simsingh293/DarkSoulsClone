using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    public GameObject Door;
    public Animator _anim;



    // Start is called before the first frame update
    void Start()
    {
        _anim = Door.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateDoor()
    {
        _anim.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        _anim.SetBool("Open", false);
    }
}
