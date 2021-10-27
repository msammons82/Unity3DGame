using UnityEngine;

// this script is used to reset scriptable objects back to default values
// useful in the editor when serialized data can persist between entering and exiting play mode. 
// also when game needs to reset without being closed. 

public class DataResetter : MonoBehaviour 
{
    // All of the scriptable object assets that should be reset at the start of the game.
    public ResettableScriptableObject[] resettableScriptableObjects;


    private void Awake ()
    {
        // go through all the scriptable objects and call their Reset function
        for( int i = 0; i < resettableScriptableObjects.Length; i++)
        {
            resettableScriptableObjects[i].Reset ();
        }
    }
}
