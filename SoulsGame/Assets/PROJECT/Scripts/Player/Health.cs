using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int Max_Health = 100;
    public int Current_Health;



    void Start()
    {
        Current_Health = Max_Health;
    }


    public void ChangeHealth(int value)
    {
        if(Current_Health + value > Max_Health)
        {
            Current_Health = Max_Health;
        } 
        else if(Current_Health - value < 0)
        {
            Current_Health = 0;
        } else
        {
            Current_Health += value;
        }
    }
}
