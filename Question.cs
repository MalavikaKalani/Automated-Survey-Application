using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;


/// <summary>
/// This scripts holds the parent class for all different question types.
/// It denotes the components that is required for all question types. 
/// It contains the functions to write the question to the unity screen.
/// It also holds override methods that are used by child classes.
/// 
/// Author: Malavika Kalani
/// Date: July 29, 2022
/// </summary>
public class Question : MonoBehaviour
{
    public string myWording; //what the question is asking
    public GameObject myQuestionBox; //placeholder for the question
    public GameObject canvas; //parent game object for all other game objects used in the survey.
    public GameObject shortAnswerNext; //button to store the answer for short-answer questions. 
    public GameObject likertNext; //button to store the slider value chosen by the user for likert questions.
    public GameObject multipleChoiceNext; //button to store the selected option for multiple-choice questions.
    public GameObject dropdownNext; //button to store the selcted menu option for dropdown questions.
    public GameObject mySubmitButton; //button to write all the survey responses to a file and exit the survey.


    /// <summary>
    /// Constructor to initialise the wording and placeholder for the question.
    /// </summary>
    /// <param name="wording"></param> The string holding the question.
    /// <param name="questionBox"></param> The GameObject that will hold the text of the question. 
    public Question(string wording, GameObject questionBox)
    {
        myWording = wording;
        myQuestionBox = questionBox;
    }


    /// <summary>
    /// Setter for the text of the question. 
    /// </summary>
    /// <param name="questionWording"></param> The string holding the question.
    public void setQuestionWording(string wording)
    {
        myWording = wording;
    }

    /// <summary>
    /// Getter for the text of the question.
    /// </summary>
    /// <returns></returns>  The string holding the question.
    public string getQuestionWording()
    {
        return myWording;
    }

    /// <summary>
    /// Writes the question text to the Unity Screen
    /// Creates the Text GameObject to hold the question. 
    /// </summary>
    public void writeQuestion()
    {
        myQuestionBox.GetComponent<Text>().text = myWording;
        myQuestionBox.transform.SetParent(myQuestionBox.transform.parent);
        myQuestionBox.transform.localPosition = new Vector2(-15, 90);
        myQuestionBox.SetActive(true);
    }


    /// <summary>
    /// Creates the appropriate button to store each type of answer.
    /// </summary>
    /// <param name="questionType"></param> string denoting the type of question.
    /// <param name="button"></param> gameobject to create buttons to generate questions.
    public void addButton(string questionType,GameObject button)
    {
        switch (questionType)
        {
            //creates button to store selected multiple choice toggle and generate next question.
            case "multiple-choice":
                button.transform.SetParent(myQuestionBox.transform.parent);
                button.transform.localPosition = new Vector2(-15, -150);
                button.transform.GetChild(0).GetComponent<Text>().text = "NEXT";
                button.SetActive(true);
                break;
            
            //creates button to store user input and generate next question.
            case "short-answer":
                button.transform.SetParent(myQuestionBox.transform.parent);
                button.transform.localPosition = new Vector2(-15, -150);
                button.transform.GetChild(0).GetComponent<Text>().text = "NEXT";
                button.SetActive(true);
                break;

            //creates button to store value on likert slider and generate next question.
            case "likert":
                button.transform.SetParent(myQuestionBox.transform.parent);
                button.transform.localPosition = new Vector2(-15, -150);
                button.transform.GetChild(0).GetComponent<Text>().text = "NEXT";
                button.SetActive(true);
                break;
            
            //creates button to store selected option from dropdown menu and generate next question.
            case "dropdown":
                button.transform.SetParent(myQuestionBox.transform.parent);
                button.transform.localPosition = new Vector2(-15, -150);
                button.transform.GetChild(0).GetComponent<Text>().text = "NEXT";
                button.SetActive(true);
                break;
        }
    }


    /// <summary>
    /// Override method called by Short Answer object.
    /// Creates input field for user to enter answer.
    /// </summary>
    public virtual void createInputField()
    {
    }

    /// <summary>
    /// Override method called by Multiple Choice object.
    /// Writes multiple choice options using Toggle UI object.
    /// </summary>
    public virtual void writeOptions()
    {
    }

    
    /// <summary>
    /// Override method called by Likert object.
    /// Creates the slider for user to drag values.
    /// </summary>
    public virtual void createLikert()
    {
    }

    /// <summary>
    /// Override method called by Likert object.
    /// Writes the endpoints on the likert slider.
    /// </summary>
    /// <param name="wordValues"></param> array holding the endpoint strings.
    public virtual void writeEndPoint(string[] wordValues)
    {

    }

    /// <summary>
    /// Override method called by Dropdown object. 
    /// Creates the dropdown menu and writes its options.
    /// </summary>
    public virtual void createMenu()
    {

    }

   
    /// <summary>
    /// Override method called when the question contains an image.
    /// Displays the corresponding image using UI Raw Image.
    /// </summary>
    /// <param name="imageName"></param> string denoting the name of image file.
    public virtual void displayImage(string imageName)
    {

    }

  
    /// <summary>
    /// This function refreshes the screen before generating a new question. 
    /// </summary>
    /// <param name="canvas"></param> parent game object for all other game objects.
    /// <param name="final"></param> boolean to denote if we have reached the last question.
    public static void refreshScreen(GameObject canvas, bool final)
    {
        if (final)
        {
            foreach (Transform child in canvas.transform)
            {
                child.gameObject.SetActive(false);
            }

        }
        else
        {
            int x = 0; //to skip the next button
            foreach (Transform child in canvas.transform)
            {
                if (x > 0)
                {
                    child.gameObject.SetActive(false);
                }
                x++;
            }
        }
    }

    

}

    

