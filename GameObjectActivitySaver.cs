using UnityEngine;

public class GameObjectActivitySaver : Saver 
{
    // Reference to the GameObject that will have its activity saved from and to.
    public GameObject gameObjectToSave;

    protected override string SetKey()
    {
        // here the key will be based on the name of the gameobject, the gameobjects type and a unique identifier. 
        return gameObjectToSave.name + gameObjectToSave.GetType().FullName + uniqueIdentifier;
    }

    protected override void Save()  
    {
        saveData.Save(key, gameObjectToSave.activeSelf);
    }

    protected override void Load() 
    {
        //Create a variable to be passed by reference to the Load Function.
        bool activeState = false;

        // If the load function returns true then the activity can be set. 
        if(saveData.Load(key, ref activeState))
            gameObjectToSave.SetActive (activeState);
    }
}