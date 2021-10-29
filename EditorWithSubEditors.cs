using UnityEngine;
using UnityEditor;

// this class acts as a base class for editors that have editors nested within them
// Interactable Editor has an array of conditionCollectionEditors
// its generic types represent the type of editor array that are nested within this editor

public abstract class EditorWithSubEditors<TEditor, TTarget> : Editor 
    where TEditor : Editor
    where TTarget : Object
{
    // array of editors nested within this editor
    protected TEditor[] subEditors;

    // This should be called in OnEnable and at the start of OnInspectorGUI.
    protected void CheckAndCreateSubEditors(TTarget[] subEditorTargets)
    {
        //if there are the correct number of subEditors then do nothing.
        if(subEditors != null && subEditors.Length == subEditorTargets.Length)

        // otherwise get rid of the editors.
        CleanupEditors();

        //Create an array of the subEditor type that is the right length for the targets
        subEditors = new TEditor[subEditorTargets.Length];

        //Populate the array and setup each editor
        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i] = CreateEditor(subEditorTargets[i]) as TEditor;
            SubEditorSetup (subEditors[i]);
        }
    }

    //This should be called in OnDisable
    protected void CleanupEditors()
    {
        //if there are no subEditors do nothing
        if(subEditors == null)
            return;

        //otherwise destroy all the subEditors
        for (int i = 0; i < subEditors.Length; i++)
        {
            DestroyImmediate(subEditors[i]);
        }

        // null the array so its GCed
        subEditors = null;
    }

    //This must be overdidden to provide any setup the subEditor needs when it is first created
    protected abstract void SubEditorSetup ( TEditor editor);
}