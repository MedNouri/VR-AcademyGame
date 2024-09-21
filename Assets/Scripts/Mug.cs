using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mug :Destructible
{
    
    
    
    private void OnCollisionEnter(Collision other)
    {
 
        if (other.relativeVelocity.magnitude>DameConstant)
        {
            Destrcut();
        }
    }
}
