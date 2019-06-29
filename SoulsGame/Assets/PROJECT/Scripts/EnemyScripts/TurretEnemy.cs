using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public GameObject Player;
    public Transform playerTransform;

    public GameObject gun;
    public Transform shootingPosition;
    public GameObject bulletPrefab;

    float delta;
    float shoot_timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        delta = Time.deltaTime;

        float dist = Vector3.Distance(transform.position, playerTransform.position);
        if(dist < 15 && dist > 5)
        {
            gun.transform.LookAt(playerTransform);
            if(shoot_timer > 2)
            {
                Instantiate(bulletPrefab, shootingPosition.position, Quaternion.identity);
                shoot_timer = 0;
            }
            shoot_timer += delta;
        }
        else
        {
            shoot_timer = 0;
        }
    }
}
