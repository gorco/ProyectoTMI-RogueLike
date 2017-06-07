using UnityEngine;
using System.Collections;


public abstract class IAStates
{

    public enum IABehaviour { IDLE, ATTACK, MOVE, HEALTH };
    public enum Conditions { LIFE, ENEMYNEAR,ENEMYWEAK, ENEMYHIT}
    protected ArrayList conditions;
    protected ArrayList states;
    protected IABehaviour currentState;
   


   protected IAStates()
    {
        conditions = new ArrayList();
        states = new ArrayList();
        currentState = IABehaviour.IDLE;
    }

    public void AddCondition(Conditions cond)
    {
        conditions.Add(cond);
    }
    public bool ContainCondition(Conditions cond)
    {
        return conditions.Contains(cond);
    }
    public void EraseCondition(Conditions cond)
    {
        conditions.Remove(cond);
    }

    public void EraseAllConditions()
    {
        conditions.Clear();
    }

    private void RefreshConditions(Conditions cond)
    {
        if (conditions.Contains(cond))
            conditions.Insert(conditions.IndexOf(cond), cond);
       
    }

    public abstract void Initialize();

    public abstract void StateTransition(IABehaviour state, Conditions cond);

    
    
}


