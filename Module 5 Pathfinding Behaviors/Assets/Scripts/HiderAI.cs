using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiderAI : AIControl
{
    void Update()
    {
        if(InRange() && CanSeeTarget())
            CleverHide();
        else
            Wander();
    }
}
