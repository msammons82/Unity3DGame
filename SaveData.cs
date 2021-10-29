using System;
using UnityEngine;
using System.Collections.Generic;

//Instance of this class can be created as assets.Each instance contains collections
// of data from the Saver monoBehaviours they have been referenced by.
// Since assets exist outside of the scene, the data will persist ready to be reloaded next time.

[CreateAssetMenu]
public class SaveData : ResettableScriptableObject
{
    //This nested class is a lighter replacement for Dictionaries. This is required because Dictionaries are not serializable.
    [Serializable]
    public class KeyValuePairLists<T>
    {
        public List<string> keys = new List<string> ();
        public List<T> values = new List<T>();


        public void Clear()
        {
            keys.Clear();
            values.Clear();
        }


        public void TrySetValue(string key, T value)
        {
            //Find the index of the keys and values based on the given key.
            int index = keys.FindIndex(x => x == key);

            // If the index is positive...
            if(index > -1)
            {
                //..set the value at that index  to the given value.
                values[index] = value;
            }
            else
            {
                //Otherwise add a new key and a new value to the collection.
                keys.Add(key);
                values.Add(value);
            }
        }


        public bool TryGetValue (string key, ref T value)
        {
            //Find the index of the keys and values based on the given key.
            int index = keys.FindIndex(x => x == key);

            // if the index is positive...
            if(index > -1)
            {
                //...set the reference value to the value at that index and return that the value was found.
                value = values[index];
                return true;
            }

            // otherwise, return that the value was not found.
            return false;
        }
    }


    // These are collection for various different data types.
    public KeyValuePairLists<bool> boolKeyValuePairLists = new KeyValuePairLists<bool> ();
    public KeyValuePairLists<int> intKeyValuePairLists = new KeyValuePairLists<int> ();
    public KeyValuePairLists<string> stringKeyValuePairLists = new KeyValuePairLists<string> ();
    public KeyValuePairLists<Vector3> vector3KeyValuePairLists = new KeyValuePairLists<Vector3>();
    public KeyValuePairLists<Quaternion> quaternionKeyValuePairLists = new KeyValuePairLists<Quaternion>();


    public override void Reset() 
    {
        boolKeyValuePairLists.Clear();
        intKeyValuePairLists.Clear();
        stringKeyValuePairLists.Clear();
        vector3KeyValuePairLists.Clear();
        quaternionKeyValuePairLists.Clear();
    }


    //This is the generic version of the Save function which takes a 
    //collection and value of the same type and then tries to set a value.
    private void Save<T>(KeyValuePairLists<T> lists, string key, T value)
    {
        lists.TrySetValue(key, value);
    }


    //This is similar to the generic Save function, it tries to get a value. 
    private bool Load<T>(KeyValuePairLists<T> lists, string key, ref T value)
    {
        return lists.TryGetValue(key, ref value);
    }


    // This is a public overload for the Save Function that specifically
    //chooses the generic type and calls the generic version.
    public void Save(string Key, bool value)
    {
        Save(boolKeyValuePairLists, key, value);
    }


    public void Save(string key, int value)
    {
        Save(intKeyValuePairLists, key, value);
    }

    public void Save(string key, string value)
    {
        Save(stringKeyValuePairLists, key, value);
    }

    public void Save(string key, Vector3 value)
    {
        Save(vector3KeyValuePairLists, key, value);
    }

    public void Save(string key, Quaternion value)
    {
        Save(quaternionKeyValuePairLists, key, value);
    }


    // this works the same as the public Save overloads except it calls the generic Load function.
    public bool Load(string key, ref bool value)
    {
        return Load(boolKeyValuePairLists, key, ref value);
    }

    public bool Load(string key, ref int value)
    {
        return Load(intKeyValuePairLists, key, ref value);
    }

    public bool Load(string key, ref Vector3 value)
    {
        return Load(vector3KeyValuePairLists, key, ref value);
    }

    public bool Load(string key, ref Quaternion value)
    {
        return Load(quaternionKeyValuePairLists, key, ref value);
    }
}