using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollisions : MonoBehaviour
{
    StateManager states;

    public void Init(StateManager state)
    {
        states = state;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;


        if (collision.gameObject.tag == "Enemy" && !states._anim.GetBool("CanMove"))
        {
            obj.SetActive(false);
            states.ChangeHealth(-10);
        }
    }
}
