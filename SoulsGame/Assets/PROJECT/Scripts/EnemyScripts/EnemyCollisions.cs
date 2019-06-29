using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    public int DamageValue = 10;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag == "Player")
        {
            StateManager states = obj.GetComponent<StateManager>();

            states.ChangeHealth(-DamageValue);
            this.gameObject.SetActive(false);
        }
    }
}
