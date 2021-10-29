using System;
using UnityEngine;
using UnityEditor;

public abstract class ReactionEditor : Editor
{
    //is the reaction editor expanded?
    public bool showReaction;
    //Represents the SerializedProperty of the array the target belongs to.
    public SerializedProperty reactionsProperty;

    // The Target reaction
    private Reaction reaction;

    //Width in pixels of the button
    private const float buttonWidth = 30f;


    private void OnEnable() 
    {
        //cache the target reference.
        reaction = (Reaction)target;

        //Call an initialization method for inheriting classes.
        Init ();
    }


    //This Function should be overridden by inheriting classes that need initialization.
    protected virtual void Init() {}


    public override void OnInspectorGUI()
    {
        //pull data from the target into the serializedObject.
        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();

        //Display a foldout for the Reaction with a custom label.
        showReaction = EditorGUILayout.Foldout(showReaction, GetFoldoutLabel());

        // show a button which, if clicked, will remove this reaction from the ReactionCollection.
        if(GUILayout.Button("-", GUILayout.Width(buttonWidth)))
        {
            reactionsProperty.RemoveFromObjectArray(reaction);
        }
        EditorGUILayout.EndHorizontal();

        //if the foldout is open, draw the GUI specific to the inheriting ReactionEditor.
        if(showReaction)
        {
            DrawReaction();
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        // Push data back from the serializedObject to the target. 
        serializedObject.ApplyModifiedProperties();
    }

    public static Reaction CreateReaction(Type reactionType)
    {
        //Create a reaction of a given type.
        return (Reaction)CreateInstance(reactionType);
    }


    protected virtual void DrawReaction()
    {
        //This function can be overridden by inheriting classes, but if it isn't , draw the default for it's properties.
        DrawDefaultInspector();
    }

    //The inheriting class must override this function to create the label of the foldout.
    protected abstract string GetFoldoutLabel();
}