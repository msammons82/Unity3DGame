using UnityEngine;

// This is the Reaction to change the satisfied state of a Condition.
// The condition here is a reference to one on the AllConditions asset. That means by
// changing the Condition here, the global game Condition will change. 
public class ConditionReaction : Reaction
{
    public Condition condition;
    public bool satisfied;


    protected override void ImmediateReaction()
    {
        condition.satisfied = satisfied;
    }
}