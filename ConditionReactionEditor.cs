using UnityEditor;

[CustomEditor(typeof(ConditionReaction))]
public class ConditionReactionEditor : ReactionEditor
{
    // Represents the Condition that will be changed.
    private SerializedProperty conditionProperty;
    // Represents the value that the Condition's satisfied flag will be set to.
    private SerializedProperty satisfiedProperty;

    //name of the field which is the Condition that will be changed.
    private const string conditionReactionPropConditionName = "condition";
    //name of the bool field which is the value the condition will get.
    private const string conditionReactionPropSatisfiedName = "satisfied";


    protected override void Init()
    {
        //Cache the SerializedProperties
        conditionProperty = serializedObject.FindProperty(conditionReactionPropConditionName);
        satisfiedProperty = serializedObject.FindProperty(conditionReactionPropSatisfiedName);
    }


    protected override void DrawReaction()
    {
        // if there's isn't a condition yet, set it to the first Condition from the AllConditions array.
        if (conditionProperty.objectReferenceValue == null)
            conditionProperty.objectReferenceValue = AllConditionsEditor.TryGetConditionAt(0);

        // get the index of the Condition in the AllConditions array.
        int index = AllConditionsEditor.TryGetConditionIndex((Condition)conditionProperty.objectReferenceValue);

        // use and set that index based on a popup of all the description of the conditions.
        index = EditorGUILayout.Popup(index, AllConditionsEditor.AllConditionDescriptions);

        //set the Condition based on the new index from the AllConditions array.
        conditionProperty.objectReferenceValue = AllConditionsEditor.TryGetConditionAt(index);

        //Use default toggle GUI for the satisfied field.
        EditorGUILayout.PropertyField(satisfiedProperty);
    }

    protected override string GetFoldoutLabel ()
    {
        return "Condition Reaction";
    }
}