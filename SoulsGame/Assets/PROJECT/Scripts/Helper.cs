using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    [Range(-1, 1)]
    public float vertical;
    [Range(-1, 1)]
    public float horizontal;


    public bool playAnim;
    public string[] oh_attacks;
    public string[] th_attacks;


    public bool twoHanded;
    public bool enableRootMotion;
    public bool useItem;
    public bool interacting;
    public bool lockon;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        enableRootMotion = !anim.GetBool("CanMove");
        anim.applyRootMotion = enableRootMotion;

        interacting = anim.GetBool("Interacting");

        if (!lockon)
        {
            horizontal = 0;
            vertical = Mathf.Clamp01(vertical);
        }

        anim.SetBool("LockOn", lockon);

        if (enableRootMotion)
        {
            return;
        }

        if (useItem)
        {
            anim.Play("Fist Pump");
            useItem = false;
        }

        if (interacting)
        {
            playAnim = false;
            vertical = Mathf.Clamp(vertical, 0, 0.5f); 
        }

        anim.SetBool("TwoHanded", twoHanded);

        test();

        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);

        
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

                if(vertical > 0.5f)
                {
                    targetAnim = "OH_Slash 3";
                }
            }
            else
            {
                int r = Random.Range(0, oh_attacks.Length);
                targetAnim = oh_attacks[r];

                if (vertical > 0.5f)
                {
                    targetAnim = "OH_Slash 3";
                }
            }
            vertical = 0;
            anim.CrossFade(targetAnim, 0.2f);
            anim.SetBool("CanMove", false);
            enableRootMotion = true;
            playAnim = false;
        }
    }
}
