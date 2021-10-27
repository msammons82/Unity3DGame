using UnityEngine;

public class RotationSaver : Saver 
{
    // Reference to the transform that will have its rotation saved from and loaded to . 
    public Transform transformToSave;

    protected override string SetKey()  
    {
        //Here the key will be based on the name of the transform, the transform's type and a unique identifier. 
        return transformToSave.name + transformToSave.GetType().FullName + uniqueIdentifier;
    }

    protected override void Save() 
    {
        saveData.Save(key, transformToSave.rotation);
    }

    protected override void Load()
    {
        // Create a variable to be passed by reference to the Load function.
        Quaternion rotation = Quaternion.identity;

        // If the load function returns true then the rotation can be set.
        if(saveData.Load(key, ref rotation))
            transformToSave.rotation = rotation;
    }
}