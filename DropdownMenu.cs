using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

/// <summary>
/// This script holds the class to implement dropdown questions.
/// It inherits from the Question class.
/// 
/// Author: Malavika Kalani
/// Date: July 29, 2022
/// </summary>
public class DropdownMenu : Question {

    private int myNumOptions; //number of options on the drop down menu
    private GameObject myMenu; //placeholder for drop down menu
    private string[] myOptions; //stores the options on the menu

    /// <summary>
    /// Constructor to initialise the components associated with dropdown questions.
    /// </summary>
    /// <param name="question"></param> string holding the question.
    /// <param name="questionBox"></param> gameObject that will hold the text of the question.
    /// <param name="menu"></param> gameObject to display the dropdown menu with its options.
    /// <param name="options"></param> array of strings denotiong the options on the menu.
    /// <param name="numOptions"></param> number of options provided to the user. 
    public DropdownMenu(string question, GameObject questionBox, GameObject menu, string[] options, int numOptions)
        : base(question, questionBox)
    {
        myMenu = menu;
        myOptions = options;
        myNumOptions = numOptions;
    }

    /// <summary>
    /// Constructor to initialise the components associated with dropdown questions when it includes an image.
    /// </summary>
    /// <param name="question"></param> string holding the question.
    /// <param name="questionBox"></param> gameObject that will hold the text of the question.
    /// <param name="menu"></param> gameObject to display the dropdown menu with its options.
    /// <param name="options"></param> array of strings denotiong the options on the menu.
    /// <param name="numOptions"></param> number of options provided to the user. 
    /// <param name="image"></param>  gameObject to display the associated image.
    public DropdownMenu(string question, GameObject questionBox, GameObject menu, string[] options, int numOptions,GameObject image)
        : base(question, questionBox)
    {
        myMenu = menu;
        myOptions = options;
        myNumOptions = numOptions;
    }



    /// <summary>
    /// Creates the options displayed on the dropdown menu.
    /// </summary>
    /// <param name="wordValues"></param> string array holding the question and options associated with it.
    /// <returns></returns> a string array holding the options that need to be written on the menu.
    public static string[] createOptions(string[] wordValues)
    {
        int numOptions = Int16.Parse(wordValues[4]);
        string[] options = new string[numOptions];
        for (int i = 0; i < numOptions; i++)
        {
            options[i] = wordValues[i + 5]; //filling in the options for the question
        }
        return options;
    }

    /// <summary>
    /// Creates the dropdown menu and writes its options from the options array.
    /// </summary>
    public override void createMenu()
    {
        GameObject newMenu = Instantiate(myMenu);
        newMenu.transform.SetParent(myMenu.transform.parent);
        //var newMenu = transform.GetComponent<Dropdown>();
        //newMenu.Options.Clear();
        for (int i = 0; i < myOptions.Length; i++)
        {
            //creating each option on the menu
            newMenu.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = myOptions[i] });
        }
        newMenu.transform.localPosition = new Vector2(-40,80);
        newMenu.SetActive(true);
    } 
}
