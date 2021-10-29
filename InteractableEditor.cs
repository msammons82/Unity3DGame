using UnityEngine;
using UnityEditor;

//This is the editor for the Interactable MonoBehaviour.
//However, since the interactable contains many sub-objects it requires many sub-editors to display them

[CustomEditor(typeof(Interactable))]
public class InteractableEditor ; EditorWithSubEditors<ConditionCollectionEditor, ConditionCollection>
{
    // Reference to the target.
    private Interactable interactable;
    // Represents the transform which is where the player walks to in order to Interact with the Interactable.
    private SerializedProperty interactionLocationProperty;
    // Represents the ConditionCollection array on the Interactable.
    private SerializedProperty collectionsProperty;
    // Represents the ReactionCollection which is used if none of the ConditionCollections are. 
    private SerializedProperty defaultReactionCollectionProperty;


    // Width in pixels of the button for adding to the ConditionCollection array.
    private const float collectionButtonWidth = 125f;
    // Name of the Transform field for where the player walks to in order to Interact with the Interactable
    private const string interactablePropInteractionLocationName = "interactionLocation";
    // Name of the ConditionCollection array.
    private const string interactablePropConditionCollectionsName = "conditionCollections";
    // Name of the ReactionCollection field which is used if none of the ConditionCollections are. 
    private const string interactablePropDefaultReactionCollectionName = "defaultReactionCollection";


    private void OnEnable ()
    {
        //Cache the target reference.
        interactable = (Interactable)target;

        // cache the SerializedProperties.
        collectionsProperty = serializedObject.FindProperty(interactablePropConditionCollectionsName);
        interactionLocationProperty = serializedObject.FindProperty(interactablePropInteractionLocationName);
        defaultReactionCollectionProperty = serializedObject.FindProperty(interactablePropDefaultReactionCollectionName);

        // Create the necessary Editors for the ConditionCollections.
        CheckAndCreateSubEditors(interactable.conditionCollections);
    }


    private void OnDisable() 
    {
        //When the InteractableEditor is disabled, destroy all the ConditionCollection editors.
        CleanupEditors();
    }

    //This is called when the ConditionCollection editors are created.
    protected override void SubEditorSetup(ConditionCollectionEditor editor)
    {
        // give the ConditionCollection editor a reference to the array to which it belongs.
        editor.collectionsProperty = collectionsProperty;
    }

    public override void OnInspectorGUI()
    {
        // Pull information from the target into the serializedObject.
        serializedObject.Update();

        //if necessary, create editors for the ConditionCollections.
        CheckAndCreateSubEditors(interactable.conditionCollections);

        //Use the default object field GUI for the interactionLocation.
        EditorGUILayout.PropertyField(interactionLocationProperty);

        //Display all of the ConditionCollections.
        for(int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI();
            EditorGUILayout.Space();
        }

        //Create a right-aligned button which when clicked, creates a new ConditionCollection in the ConditionCollections array.
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if(GUILayout.Button("Add Collection", GUILayout.Width(collectionButtonWidth)))
        {
            ConditionCollection newCollection = ConditionCollectionEditor.CreateConditionCollection();
            collectionsProperty.AddToObjectArray(newCollection);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(defaultReactionCollectionProperty);

        serializedObject.ApplyModifiedProperties();
    }
}