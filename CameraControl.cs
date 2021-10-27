using System.Collections;
using UnityEngine;


public class CameraControl : MonoBehaviour 
{
    //This states whether the camera should be moved by this script
    public bool moveCamera = true;
    //Smoothing applied during Slerp, higher is smoother but slower.
    public float smoothing = 7f;
    // The offset from the players position that the camera aims at
    public Vector3 offset = new Vector3 (0f,1.5f,0f);
    // Reference to the players transform to aim at
    public Transform playerPosition;

    private IEnumerator Start ()
    {
        //If the camera should move or not
        if(!moveCamera)
            yield break;

        // Wait a single frame to ensure all other Starts are called first
        yield return null;

        // Set the rotation of the camera to look at the players position with a given offset. 
        transform.rotation = Quaternion.LookRotation(playerPosition.position - transform.position + offset);
    }

    //LateUpdate is used so that all position updates have happened before the camera aims. 
    private void LateUpdate() 
    {
        // if the camera should move or not
        if(!moveCamera)
            return;

        //find a new rotation aimed at the player's position with a given offset.
        Quaternion newRotation = Quaternion.LookRotation( playerPosition.position - transform.position + offset);
        
        // Spherically interpolate between the camera's current rotation and the new rotation
        transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * smoothing);
    }
}