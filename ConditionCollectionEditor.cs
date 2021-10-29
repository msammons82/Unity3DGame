using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConditionCollection))]
public class ConditionCollectionEditor : EditorWithSubEditors<ConditionEditor, Condition>
{
    //Represents the array of ConditionCollections that the target belongs to.
    public SerializedProperty collectionsProperty;

    // Reference to the target
    private ConditionCollection conditionCollection;
    //Represents a a string description for the target
    private SerializedProperty descriptionProperty;
    // Represents an array of Conditions for the target.
    private SerializedProperty conditionsProperty;
    //Represents the ReactionCollection that is referenced by the target.
    private SerializedProperty reactionCollectionProperty;


    //Width of the button for adding a new condition.
    private const float conditionButtonWidth = 30f;
    //Width of the button fort removing the target from it's Interactable
    private const float collectionButtonWidth = 125f;
    // name of the field that represent a string description for target
    private const string conditionCollectionPropDescriptionName = "description";
    //name of the field that represents an array of Conditions for the target
    private const string conditionCollectionPropRequiredConditionsName = "requiredConditions";
    //name of the field that represent the reactionCollection that this is referenced by the target. 
    private const string conditionCollectionPropReactionCollectionName = "reactionCollection";


    private void OnEnable() 
    {
        //cache a reference to the target.
        conditionCollection = (ConditionCollection)target;

        //if this editor exists but isn't targeting anything destroy it.
        if(target == null)
        {
            DestroyImmediate(this);
            return;
        }

        //cache the SerializedProperties
        descriptionProperty = serializedObject.FindProperty(conditionCollectionPropDescriptionName);
        conditionsProperty = serializedObject.FindProperty(conditionCollectionPropRequiredConditionsName);
        reactionCollectionProperty = serializedObject.FindProperty(conditionCollectionPropReactionCollectionName);

        // Check if the editors for the conditions need creating and optionally create them.
        CheckAndCreateSubEditors(conditionCollection.requiredConditions);
    }


    private void OnDisable()
    {
        // when this editor ends, destroy all it's subEditors.
        CleanupEditors();
    }

    //This is called immediately when a subEditor is created.
    protected override void SubEditorSetup(ConditionEditor editor)
    {
        //set the editor type so that the correct GUI for condition is shown.
        editor.editorType = ConditionEditor.EditorType.ConditionCollection;

        //Assign the conditions property so that the ConditionEditor can remove its target if necessary.
        editor.conditionsProperty = conditionsProperty;
    }


    public override void OnInspectorGUI()
    {
        //Pull the info from the target into the serializedObject.
        serializedObject.Update();

        //check if the editors for the conditions need creating.
        CheckAndCreateSubEditors(conditionCollection.requiredConditions);

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();

        //use the isExpanded bool for the descriptiveProperty to store whether the foldout is open or closed.
        descriptionProperty.isExpanded = EditorGUILayout.Foldout(descriptionProperty.isExpanded, descriptionProperty.stringValue);

        // Display a button showing 'Remove Collection' which removes the target from the Interactable when clicked.
        if(GUILayout.Button("Remove Collection", GUILayout.Width(collectionButtonWidth)))
        {
            collectionsProperty.RemoveFromObjectArray(conditionCollection);
        }

        EditorGUILayout.EndHorizontal();

        //if the foldout is open show the expanded GUI.
        if (descriptionProperty.isExpanded)
        {
            ExpandedGUI();
        }

        ExpandedGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        //Push all changes made on the serializedObject back to the target.
        serializedObject.ApplyModifiedProperties();
    
    }
    
    
        private void ExpandedGUI()
        {
            EditorGUILayout.Space();

            //Display the description for editing.
            EditorGUILayout.PropertyField(descriptionProperty);

            EditorGUILayout.Space();

            //Display the Labels for the Conditions evenly split over the inspector.
            float space = EditorGUIUtility.currentViewWidth / 3f;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Condition", GUILayout.Width(space));
            EditorGUILayout.LabelField("Satisfied?", GUILayout.Width(space));
            EditorGUILayout.LabelField("Add/Remove", GUILayout.Width(space));
            EditorGUILayout.EndHorizontal();

            // Display each of the Conditions.
            EditorGUILayout.BeginVertical(GUI.skin.box);
            for(int i = 0; i < subEditors.Length; i++)
            {
                subEditors[i].OnInspectorGUI();
            }
            EditorGUILayout.EndHorizontal();

            //Display a right aligned button which when clicked adds a condition to the array.
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if(GUILayout.Button("+", GUILayout.Width(conditionButtonWidth)))
            {
                Condition newCondition = ConditionEditor.CreateCondition();
                conditionsProperty.AddToObjectArray(newCondition);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            //Display the reference to the ReactionCollection for editing.
            EditorGUILayout.PropertyField(reactionCollectionProperty);
        }

        //This function is static such that it can be called without an editor being instanced.
        public static ConditionCollection CreateConditionCollection()
        {
            //Create a new instance of ConditionCollection
            ConditionCollection newConditionCollection = CreateInstance<ConditionCollection>();

            // Give it a default description.
            newConditionCollection.description = " New condition collection";

            // give it a single default Condition.
            newConditionCollection.requiredConditions = new Conditon[1];
            newConditionCollection.requiredConditions[0] = ConditionEditor.CreateCondition();
            return newConditionCollection;
        }
        
        
    
    
    







}

