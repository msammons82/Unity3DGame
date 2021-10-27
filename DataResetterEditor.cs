using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataResetter))]
public class DataResetterEditor : Editor
{
    // Reference to the target of this editor
    private DataResetter dataResetter;
    // represent the only field in the target
    private SerializedProperty resettersProperty;

    // width in pixels of the add and remove buttons
    private const float buttonWidth = 30f; 
    // the name of the field to be represented
    private const string dataResetterPropResettableScriptableObjectsName = "resettableScriptableObjects";

    private void OnEnable() 
    {
        //Cache the property and target.
        resettersProperty = serializedObject.FindProperty(dataResetterPropResettableScriptableObjectsName);

        dataResetter = (DataResetter)target;

        // if the array is null, initialize it to prevent null refs
        if( dataResetter.ResettableScriptableObject == null)
        {
            dataRestter.ResettableScriptableObject = new ResettableScriptableObject[0];
        }
    }


    public override void OnInspectorGUI ()
    {
        // update the state of the serializedObject to the current values of the target
        serializedObject.Update();
        // go through all the resetters and create GUI appropriate for them.
        for( int i = 0; i < resettersProperty.arraySize; i++)
        {
            SerializedProperty resettableProperty = resettableProperty.GetArrayElementAtIndex (i);

            EditorGUILayout.PropertyField(resettableProperty);
        }
        
        EditorGUILayout.BeginHorizontal();
        
        //Create a button with a '+' and if it's clicked, add an element to the end of the array
        if(GUILayout.Button("+", GUILayout.Width(buttonWidth)))
        {
            resettableProperty.InsertArrayElementAtIndex(resettableProperty.arraySize);
        }

        //Create a button with a '-' and if it's clicked remove the last element of the array.
        if(GUILayout.Button ("-", GUILayout.Width(buttonWidth))
        {
            if(resettersProperty.GetArrayElementAtIndex(resettersProperty.arraySize -1).objectReferenceValue)
                resettableProperty.DeleteArrayElementAtIndex(resettableProperty.arraySize -1);
            resettableProperty.DeleteArrayElementAtIndex(resettableProperty.arraySize -1);  
        }

        EditorGUILayout.EndHorizontal();

        //push the values from the serializedObject back to the target
        serializedObject.ApplyModifiedProperties();

    }
}