using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{
    //The Image components that display the items
    public Image[] itemImages = new Image[numItemSlots];
    //The Items that are carried by the Player.
    public Item[] items = new Item[numItemSlots];

    //the number of items that can be carried
    public const int numItemSlots = 4;

    // this function is called by the PickedUpItemReaction in order to add an item to the inventory
    public void AddItem(Item itemToAdd)
    {
        //Go through all the item slots
        for(int i = 0; i < items.Length; i++)
        {
            //...if the item slot is empty
            if(items[i]== null)
            {
                //..set it to the picked up item and set the image component to display the item's sprite
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                return;
            }
        }
    }


    // this function is called by the LostItemReaction in order to remove an item from the inventory
    public void RemoveItem ( Item itemToRemove)
    {
        // go through all the item slots..
        for (int i =0; i < items.Length; i++)
        {
            //..if the item slot has the item to be removed
            if(items[i]== itemToRemove)
            {
                //set the item slot to null and set the item component to display nothing.
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i]= false;
                return;
            }
        }
    }
}