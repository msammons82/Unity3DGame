using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AllConditions))]
public class AllConditionsEditor : Editor
{
    //Property for accessing the descriptions for all the conditions.
    // this is used for the popups on the ConditionEditor
    public static string[] AllConditionDescriptions
    {
        get
        {
            //if the description array doesn't exist yet, set it.
            if (allConditionDescriptions == null)
            {
                SetAllConditionDescriptions();
            }
            return allConditionDescriptions;
        }
        private set { allConditionDescriptions = value;}
    }

    //Field to store the descriptions of all the conditions.
    private static string [] allConditionDescriptions;

    //all of the subEditors to display the conditions
    private ConditionEditor[] conditionEditors;
    //Reference to the target
    private AllConditions allConditions;
    //string to start off the naming of new conditions.
    private string newConditionDescription = "New Condition";

    private const string creationPath = "Asset/Resources/AllConditions.asset";

    //Width in pixels of the button to create conditions
    private const float buttonWidth = 30f;

    private void OnEnable()
    {
        // cache the reference to the target
        allConditions = (AllConditions)target;

        // if there aren't any conditions on the target, create an empty array of Conditions.
        if (allConditions.conditions == null)
            allConditions.conditions = new Condition[0];

        //if there aren't any editors, create them.
        if (conditionEditors == null)
        {
            CreateEditors();
        }
    
    }
    

    private void OnDisable() 
    {
        //Destroy all the editors.
        for (int i =0; i < conditionEditors.Length; i++)
        {
            DestroyImmediate(conditionEditors[i]);
        }
    }

    private static void SetAllConditionDescriptions ()
    {
        // Create a new array that has the same number of elements as there are conditions.
        AllConditionDescriptions = new string[TryGetConditionsLength()];

        // Go through the array and assign the description of the condition at the same index.
        for (int i = 0; i < AllConditionDescriptions.Length;i++)
        {
            AllConditionDescriptions[i] = TryGetConditionAt(i).description;
        }
    }

    public override void OnInspectorGUI()
    {
        // if there are different number of editors to Conditions, create them afresh.
        if (conditionEditors.Length != TryGetConditionsLength())
        {
            //Destroy all the old editors
            for (int i = 0; i < conditionEditors.Length; i++)
            {
                DestroyImmediate(conditionEditors[i]);
            }

            // Create new editors
            CreateEditors();
        }

        // Display all the conditions.
        for(int i = 0; i < conditionEditors.Length; i++)
        {
            conditionEditors[i].OnInspectorGUI();
        }

        //if there are conditions, add a gap.
        if(TryGetConditionsLength() > 0 )
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        EditorGUILayout.BeginHorizontal();

        // Get and display a string for the name of a new condition
        newConditionDescription = EditorGUILayout.TextField(GUIContent.none, newConditionDescription);

        // display a button that when clicked adds a new condition to the AllConditions asset and resets the new description string.
        if(GUILayout.Button("+", GUILayout.Width(buttonWidth)))
        {
            AddCondition(newConditionDescription);
        }
        EditorGUILayout.EndHorizontal();
    }


    private void CreateEditors()
    {
        // Create a new array for the editors which is the same length at the conditions array.
        conditionEditors = new ConditionEditor[allConditions.conditions.Length];

        //Go through all the empty array
        for (int i = 0; i < conditionEditors.Length; i++)
        {
            //...and create an editor with an editor type to display correctly.
            conditionEditors[i] = CreateEditor(TryGetConditionAt(i)) as ConditionEditor;
            conditionEditors[i].editorType = ConditionEditor.EditorType.AllConditionAsset;
        }
    }

    //Call this function when the menu item is selected.
    [MenuItem("Assets/Create/AllConditions")]
    private static void CreateAllConditionsAsset()
    {
    
        
        //If there's already an AllConditions asset, do nothing
        if(AllConditions.Instance)
            return;
        
        //Create an instance of the AllConditions object and make an asset for it. 
        AllConditions instancer = CreateInstance<AllConditions>();
        AssetDatabase.CreateAsset(instance, creationPath);

        //Set this as the singleton instance
        AllConditions.Instance = instance;

        // Create a new empty array of conditions
        instance.conditions = new Condition[0];  
    }


    private void AddCondition(string description)
    {
        //if there isn't an AllConditions instance yet, put a message in the console and return.
        if(!AllConditions.Instance)
        {
            Debug.LogError("AllConditions has not been created yet.");
                return;
        }

        //Create a condition based on the description
        Condition newCondition = ConditionEditor.CreateCondition(description);

        //the name is what is displayed by the asset so set that too
        newCondition.name = description;

        //Record all operations on the newConditions so they can be undone.
        Undo.RecordObject(newCondition, "Created new Condition");

        //Attach the condition to the AllConditions asset
        AssetDatabase.AddObjectToAsset(newCondition, AllConditions.Instance);

        //Import the asset so it is recognized as a joined asset
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newCondition));

        //Add the condition to the AllConditions array.
        ArrayUtility.Add(ref AllConditions.Instance.conditions, newCondition);

        // Mark the AllConditions assest as dirty so the editor knows to save changes to it when a project save happens.
        EditorUtility.SetDirty(AllConditions.Instance);

        // Recreate the condition description array with the new added condition
        SetAllConditionDescriptions();
    }

    public static int TryGetConditionIndex(Condition condition)
    {
        //Go through all the conditions
        for (int i = 0; i < TryGetConditionsLength(); i++)
        {
            //..and if one matches the given condition, return null
            if (TryGetConditionAt (i).hash == condition.hash)
                return i;
        }

        //If the condition wasn't found, return -1
        return -1;
    }

    public static Condition TryGetConditionAt(int index) 
    {
        //Cache the AllConditions array.
        Condition[] allConditions = AllConditions.Instance.conditions;

        //if it doesn't exist or there are null elements, return null.
        if(allConditions == null || allConditions[0] == null)
            return null;

        //if the given index is beyond the length of the array return the first element.
        if (index >= allConditions.Length)
            return allConditions[0];

        //Otherwise return the condition at the given index.
        return allConditions[index];
    }


    public static int TryGetConditionsLength() 
    {
        // if there is no conditions array, return a length of 0.
        if(AllConditions.Instance.conditions == null)
            return 0;

        // otherwise return the length of the array.
        return AllConditions.Instance.conditions.Length;
    }









}