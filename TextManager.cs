using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//This class is used to manage the text that is displayed on screen.
// In situations where many messages are triggered one after another
//It makes sure they are played in the correct order.

public class TextManager : MonoBehaviour 
{
    //This script encapsulates the messages that are sent for organization
    public struct Instruction 
    {
        //The body of the message
        public string message;
        // The color of the message
        public Color textColor;
        //the time the message should be shown
        public float startTime;
    }

    //reference to the Text component that will display the message
    public Text text;
    //The amound of time that each character in a message adds to the amount of time it is displayed for.
    public float displayTimePerCharacter = 0.1f;
    // The additional time that is added to the message is displayed for
    public float additionalDisplayTime = 0.5f;

    // Collection of instructions that are ordered by their startTime.
    private List<Instruction> instructions = new List<Instruction>();

    // the time at which there should no longer be any text on screen.
    private float clearTime;

    private void Update() 
    {
        //if there are instructions and the time is beyond the start time of the first instructions..
        if(instructions.Count > 0 && Time.time >= instructions[0].startTime)
        {
            //...set the text component to display the instructions message in the correct color. 
            text.text = instructions[0].message;
            text.color = instructions[0].textColor;

            // then remove the instruction
            instructions.RemoveAt (0);
        }
        // Otherwise, if the time is beyond the clear time, clear the text component's text.
        else if (Time.time >= clearTime)
        {
            text.text = string.Empty;
        }
    }

    // This function is called from TextReactions in order to display a message to the screen.
    public void DisplayMessage(string message, Color textColor, float delay) 
    {
        // the time when the message should start displaying is the current time offset by the delay.
        float startTime = Time.time + delay;

        // Calculate how long the message should be displayed for based on the number of characters
        float displayDuration = message.Length * displayTimePerCharacter + additionalDisplayTime;

        // create a new clear time..
        float newClearTime = startTime + displayDuration;

        //.. and if it is after the old clear time, replace the old clear time with then new
        if (newClearTime > clearTime)
            clearTime = newClearTime;

        //Create a new instruction
        Instruction newInstruction = new Instruction
        {
            message = message, 
            textColor = textColor,
            startTime = startTime
        };

        // Add the new instruction to the collection.
        instructions.Add (newInstructions);

        // order the instructions by their start time
        SortInstructions ();
    }

    // this function orders the instructions by start time using a bubble sort.
    private void SortInstructions ()
    {
        //Go through all the instructions..
        for (int i = 0; i < instructions.Count; i++)
        {
            //.. and create a flag to determine if any reordering has been done
            bool swapped = false;
            
            // for each instruction, go through all the instructions
            for (int j = 0; j < instructions.Count; j++)
            {
                //... and compare the instructions from the outer loop with this one.
                // if the outer loop's instruction has a later start time, swap their positions and set the flag to true. 
                if (instructions[i].startTime > instructions[j].startTime)
                {
                    Instructions temp = instructions[i];
                    instructions[i] = instructions[j];
                    instructions[j] = temp;
                    
                    swapped = true;
                }
            }

            //if for a single instruction, all other instructions are later then they are correctly ordered.
            if (!swapped)
                break;
        }
    }
}