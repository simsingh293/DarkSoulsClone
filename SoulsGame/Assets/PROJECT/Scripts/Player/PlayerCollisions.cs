using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    StateManager states;

    public void Init(StateManager state)
    {
        states = state;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;


        if (obj.tag == "Collectible")
        {
            obj.SetActive(false);
            states.ChangeHealth(-30);

        }

        if (obj.tag == "HealthPickup")
        {
            obj.SetActive(false);
            states.ChangeHealth(10);
        }
    }
}
