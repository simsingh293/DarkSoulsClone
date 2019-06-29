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
            states.score++;
            states.scoreText.text = "Score: " + states.score;

        }

        if (obj.tag == "HealthPickup")
        {
            obj.SetActive(false);
            states.ChangeHealth(10);
        }

        if (obj.tag == "PowerUp")
        {
            obj.SetActive(false);
            states.poweredUp = true;
        }
    }






    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DoorTrigger")
        {
            other.gameObject.GetComponent<TriggerDoor>().ActivateDoor();
        }
        else if (other.gameObject.tag == "MultipleDoorTrigger")
        {
            if (other.GetComponent<Renderer>().material.color != Color.red)
            {
                other.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        else if (other.gameObject.tag == "BreakableTrigger")
        {
            other.gameObject.GetComponent<BreakDoorTrigger>().ActivateDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DoorTrigger")
        {
            other.gameObject.GetComponent<TriggerDoor>().CloseDoor();
        }
    }




}
