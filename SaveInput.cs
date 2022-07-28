using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using UnityEngine.EventSystems;

/// <summary>
/// This script holds the class that helps in saving user responses.
/// Implements methods to store each type of answer and write it to a file.
/// 
/// Author: Malavika Kalani
/// Date: July 29, 2022
/// </summary>
public class SaveInput : ReadText {

    public List<String> myResponses; //list to dynamically store the user responses
    Dictionary<string, string> QA; //dictionary to store the question(key) and user response(value)
    
    public string myInput; //string denoting the user response for short-answer questions.
    public string myMcAnswer; //string denoting the user response for multiple choice questions.
    public float mySliderVal; //float denoting the user response for likert questions.
    public string myOption;  //string denoting the user response for dropdown questions.

    /// <summary>
    /// Initialises a list of strings to store the user responses.
    /// Initialises a dictionary of string keys and values to store questions and responses.
    /// </summary>
    public void Start()
    {
        myResponses = new List<string>();
        QA = new Dictionary<string, string>();
    }

    /// <summary>
    /// Obtains the response entered in an inputfield for short-answer questions.
    /// Adds corresponding question and response to the dictionary.
    /// </summary>
    /// <param name="inputfield"></param> InputField gameObject where the user enters the answer.
    public void readInputField(InputField inputfield)
    {
        myInput = inputfield.text;
        myResponses.Add(myInput);
        QA.Add(questionBox.GetComponent<Text>().text, myInput);
        inputfield.text = "";
    }

    /// <summary>
    /// Obtains the text of the multiple choice option selected by user.
    /// Adds corresponding question and response to the dictionary.
    /// </summary>
    public void readSelectedToggle()
    {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();
        foreach (var t in toggles)
            if (t.isOn)
            {
                myMcAnswer = t.transform.GetChild(1).GetComponent<Text>().text;
                myResponses.Add(myMcAnswer);
                QA.Add(questionBox.GetComponent<Text>().text, myMcAnswer);
            }
    }

    /// <summary>
    /// Obtains the value dragged by user on the likert slider.
    /// Adds corresponding question and response to the dictionary.
    /// </summary>
    /// <param name="slider"></param> Slider gameobject where the user drags it to the chosen value. 
    public void getSliderValue(Slider slider)
    {

        mySliderVal = slider.value;
        myResponses.Add(mySliderVal.ToString());
        QA.Add(questionBox.GetComponent<Text>().text, mySliderVal.ToString());
        slider.value = 0;
    }

    /// <summary>
    /// Obtains the text f the option selected by user on the dropdown menu.
    /// Adds corresponding question and response to the dictionary.
    /// </summary>
    /// <param name="dropdown"></param> Dropdown gameobject where the user selects the option from the menu.
    public void getDropdownOption(Dropdown dropdown)
    {
        myOption = dropdown.options[dropdown.value].text;
        Debug.Log(myOption);
        myResponses.Add(myOption);
        QA.Add(questionBox.GetComponent<Text>().text, myOption);
    }

    /// <summary>
    /// Creates a file to store user responses.
    /// Writes the questions with the corresponding answers to the file.
    /// </summary>
    public void createFile()
    {
        
        //path of the file
        string name = myResponses[0];
        string path = Application.dataPath + "\\" + name + ".txt";
        foreach (KeyValuePair<string, string> pair in QA)
        {

            File.AppendAllText(path, pair.Key + "," + pair.Value + "\n");
            Debug.Log(pair.Key);
        }
    }

    /// <summary>
    /// Exits the playmode when the submit button is clicked to indicate that survey is completed.
    /// </summary>
    public void exitSurvey()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
