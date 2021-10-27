using UnityEngine;

public class PositionSaver : Saver 
{
    // Reference to the transform that will have its position saved from and loaded to. 
    public Transform transformToSave;

    protected override string SetKey() 
    {
        // Here the key will be based on the name of the transform, the transforms type and a unique identifier.
        return transformToSave.name + transformToSave.GetType().FullName + uniqueIdentifier;
    }

    protected override void Save() 
    {
        saveData.Save(key, transformToSave.position);
    }

    protected override void Load() 
    {
        // create a variable to be passed by reference to the Load function.
        Vector3 position = Vector3.zero;

        // If the load function returns true then the position can be set. 
        if (saveData.Load(key, ref position))
            transformToSave.position = position;
    }
}