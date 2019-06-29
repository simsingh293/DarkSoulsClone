using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTimer : MonoBehaviour
{
    public List<ParticleSystem> columns = new List<ParticleSystem>();
    public GameObject[] columnsObjs;

    public float _timer = 5.0f;
    public int count = 0;

    public GameObject water;
    public ParticleSystem WaterPs;

    public bool isBurning = false;
    public GameObject damageTrigger;


    // Start is called before the first frame update
    void Start()
    {
        columnsObjs = GameObject.FindGameObjectsWithTag("Column");
        for (int i = 0; i < columnsObjs.Length; i++)
        {
            columns.Add(columnsObjs[i].GetComponentInChildren<ParticleSystem>());
            columns[i].Stop();
        }

        //WaterPs = GameObject.FindGameObjectWithTag("Water").GetComponentInChildren<ParticleSystem>();
        WaterPs.Stop();
        //StartTimer();        

        if(damageTrigger == null)
        {
            damageTrigger = GameObject.Find("DamageTrigger");
        }

        damageTrigger.SetActive(false);
    }

    

    public void Countdown()
    {
        Renderer test = columnsObjs[count].GetComponent<Renderer>();
        ParticleSystem ps = columns[count];


        test.material.color = Color.red;
        Debug.Log(count);
        ps.Play();

        if (count >= _timer)
        {
            WaterPs.Play();
            isBurning = true;
            damageTrigger.SetActive(true);
            CancelInvoke();
        }
        else
        {
            count++;

        }

    }

    public void StartTimer()
    {
        InvokeRepeating("Countdown", 1, 1);

    }
}
