using UnityEngine;

public class BehaviourEnableStateSaver : Saver
{
    //Reference to the behaviour that will have its enabled state saved from and loaded to.
   public Behaviour behaviourToSave;  

   protected override string SetKey ()
   {
       // Here the key will be based on the name of the behaviour, the behaviours type and a unique identifier.
       return behaviourToSave.name + behaviourToSave.GetType().FullName + uniqueIdentifier;
   } 

   protected override void Save ()
   {
       saveData.Save(key, behaviourToSave.enabled);
   }

   protected override void Load()
   {
       //Create a variable to be passed by reference to the Load function.
       bool enabledState = false;

       //if the load function returns true then the enabled state can be set.
       if(saveData.Load(key, ref enabledState))
          behaviourToSave.enabled = enabledState;
   }
}