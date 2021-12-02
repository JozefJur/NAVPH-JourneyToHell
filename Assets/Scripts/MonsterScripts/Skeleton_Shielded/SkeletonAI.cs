using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonsterAI
{

    public bool IsShielded;
    public GameObject Shield;


    // Update is called once per frame
    protected override void Update()
    {

        if (IsShielded)
        {

        }
        else
        {
            base.Update();
        }
        
    }
}
