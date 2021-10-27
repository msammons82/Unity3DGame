using UnityEngine;

// simple script represents Items that can be picked up
//The inventory system is done using this script instead of just sprites
// to ensure that items are extensible
[CreateAssestMenu]
public class Item : ScriptableObject 
{
    public Sprite sprite;
}