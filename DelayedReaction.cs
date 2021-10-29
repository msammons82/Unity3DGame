using UnityEngine;
using System.Collections;

// this is a base class for Reactions that need to have a delay/
public abstract class DelayedReaction : Reaction
{
    public float delay;


    protected WaitForSeconds wait;


    // This function 'hides' the Init function from the Reaction Class
    // Hiding generally happens when the original function doesn't meet
    // the requirements for the function in the inheriting class.
    public new void Init() 
    {
        wait = new WaitForSeconds(delay);

        SpecificInit ();;
    }


    //This function 'hides' the React function from the Reaction class.
    // It replaces the functionality with starting a coroutine instead.
    public new void React(MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(ReactCoroutine());
    }


    protected IEnumerator ReactCoroutine()
    {
        //Wait for the specified time
        yield return wait;

        //Then call the ImmediateReaction function which must be defined in inheriting classes.
        ImmediateReaction();
    }
}