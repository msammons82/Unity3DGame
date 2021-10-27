using UnityEngine;

// This script acts as a collection for all the individual Reactions
// that happen as a result of an interaction.

public class ReactionCollection : MonoBehaviour 
{
    // array of all the reactions to play when react is called
    public Reaction[] reactions = new Reaction[0];


    private void Start()
    {
        //Go through all the Reactions and call their Init function
        for (int i = 0; i < reactions.Length; i++)
        {
            //the DelayedReaction 'hides' the Reactions Init function with it's own
            // this means that we have to try to cast the Reaction to a DelayedReaction and then if it exists call it's Init function.
            DelayedReaction delayedReaction = reactions[i] as DelayedReaction;

            if(delayedReaction)
                delayedReaction.Init();
            else
            {
                reactions[i].Init();
            }
        
        
        }

    }
}