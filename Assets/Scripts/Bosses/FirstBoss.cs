 using UnityEngine;
using System.Collections;
using System;

public class FirstBoss : IAStates
{

    public FirstBoss() : base() { }


   

    public override void Initialize()
    {
        currentState = IABehaviour.IDLE;        
    }

   

    public override void StateTransition(IABehaviour state, Conditions cond)
    {
        foreach (Conditions c in conditions)
        {
            if (c.Equals(Conditions.ENEMYNEAR) || c.Equals(Conditions.ENEMYNEAR))
            {
                currentState = IABehaviour.ATTACK;
            }
            if(c.Equals(Conditions.LIFE))
            {
                currentState = IABehaviour.MOVE;
            }
            if (c.Equals(Conditions.ENEMYHIT))
                currentState = IABehaviour.IDLE;

        }

    }
}
