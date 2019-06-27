using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    [Range(0, 1)]
    public float vertical;


    public bool playAnim;
    public string[] oh_attacks;
    public string[] th_attacks;


    public bool twoHanded;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("TwoHanded", twoHanded);

        test();

        anim.SetFloat("Vertical", vertical);
    }

    void test()
    {
        if (playAnim)
        {
            string targetAnim;

            if (twoHanded)
            {
                int r = Random.Range(0, th_attacks.Length);
                targetAnim = th_attacks[r];
            }
            else
            {
                int r = Random.Range(0, oh_attacks.Length);
                targetAnim = oh_attacks[r];
            }
            vertical = 0;
            anim.CrossFade(targetAnim, 0.2f);
            playAnim = false;
        }
    }
}
