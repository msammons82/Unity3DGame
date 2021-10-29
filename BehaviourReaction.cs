using UnityEngine;

//this reaction is for turning Behaviour on and off.Behaviour are a subset of Components 
// which have the enabled property.
public class BehaviourReaction : DelayedReaction
{
    public Behaviour behaviour;
    public bool enabledState;


    protected override void ImmediateReaction()
    {
        behaviour.enabled = enabledState;
    }
}