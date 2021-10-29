using UnityEngine;

//This class is used to determine whether or not Reactions should happen.
//Instances of Condition exist in two places: as assets which are part
// of the AllConditions asset and as part of ConditionCollections.
// The Conditions that are part of the AllConditions asset are those that are set by
// Reactions and reflect the state of the game.

public class Condition : ScriptableObject
{
    public string description;   // a description of the condition
    public bool satisfied;      // whether or not the Condition has been satisfied
    public int hash;            // A number which represents the description
}