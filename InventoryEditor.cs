using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    //Whether the GUI for each item slot is expanded.
    private bool[] showItemSlots = new bool[Inventory.numItemSlots];
    // Represents the array of Image components to display the Items.
    private SerializedProperty itemImagesProperty;
    // Represents the array of Items.
    private SerializedProperty itemsProperty;

    // the name of the field that is an array of Image components.
    private const string inventoryPropItemImagesName = "itemImages";
    // the name of the field that is an array of Items.
    private const string inventoryPropItemsName = "items";


    private void OnEnable ()
    {
        // Cache the SerializedProperties
        itemImagesProperty = serializedObject.FindProperty(inventoryPropItemImagesName);
        itemsProperty = serializedObject.FindProperty(inventoryPropItemsName);
    }


    public override void OnInspectorGUI ()
    {
        //Pull all the information from the target into the serializedObjects
        serializedObject.Update();

        //Display GUI for each Item slot.
        for(int i = 0; i < Inventory.numItemSlots; i++)
        {
            ItemSlotGUI (i);
        }

        // Push all the information from the serializedObject back into the target.
        serializedObject.ApplyModifiedProperties();
    }

    private void ItemSlotGUI(int index) 
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        //Display a foldout to determine whether the GUI should be shown or not. 
        showItemSlots[index] = EditorGUILayout.Foldout (showItemSlots[index], "Item slot " + index);

        // if the foldout is open then display default GUI for the specific elements in each array.
        if(showItemSlots[index])
        {
            EditorGUILayout.PropertyField(itemImagesProperty.GetArrayElementAtIndex(index));
            EditorGUILayout.PropertyField(itemsProperty.GetArrayElementAtIndex(index));
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }
}