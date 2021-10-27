using UnityEngine;

// This is an abstract MonoBehaviour that is the base class for all classes 
// that want to save data to persist between scene loads and unloads

public abstract class Saver : MonoBehaviour 
{
    // A unique string set by a scene designer to identify what is being saved.
    public string uniqueIdentifier;
    // Reference to the SaveData scriptable object where the data is stored. 
    public SaveData saveData;
    
    // A string to identify what is being saved
    protected string key;

    // Reference to the SceneController so that this can subscribe to events that happen before and after scene loads
    private SceneController sceneController;

    private void Awake() 
    {
        //Find the scene controller and store a reference to it
        sceneController = FindObjectOfType<SceneController>();

        //If the scene controller couldn't be found throw an exception so it can be added.
        if(!sceneController)
            throw new UnityException("Scene Contoller could not be found, ensure that it exists in the Persistent scene.");

        // set the key based on information in inheriting classes.
        key = SetKey();
    }

    private void OnDisable() 
    {
        //Unsubscribe the save function from the BeforeSceneUnload event
        sceneController.BeforeSceneUnload -= Save;
        // unsubscribe the load function from the AfterSceneLoad event
        sceneController.AfterSceneLoad -= Load;
    }

    // This function will be called in awake and must return the intended key
    // the key must be totally unique across all save scripts.
    protected abstract string SetKey();

    // this function will be called just before a scene is unloaded
    // It must call saveData.Save and pass in the key and the relevant data.
    protected abstract void Save();

    //This function will be called just after a scene is finished loading.
    // It must call saveData.Load with a ref parameter to get the data out.
    protected abstract void Load();
}