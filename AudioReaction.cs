using UnityEngine;

//This reaction is used to play sounds through a given AudioSource.
// Since the AudioSource itself handles delay, this is a Reaction
// rather than an DelayedReaction.
public class AudioReaction : Reaction
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float delay;


    protected override void ImmediateReaction()
    {
        //Set the AudioSource's clip to the given one and play with the given delay.
        audioSource = audioClip;
        audioSource.PlayDelayed(delay);
    }
}