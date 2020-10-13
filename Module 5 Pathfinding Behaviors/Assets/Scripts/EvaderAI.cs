using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaderAI : AIControl
{
    void Update()
    {
        if(InRange())
            Evade();
        else
            Wander();
    }
}
