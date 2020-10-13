using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuerAI : AIControl
{
    void Update() 
    {
        if(InRange())
            Pursue();
        else
            Wander();
    }
}
