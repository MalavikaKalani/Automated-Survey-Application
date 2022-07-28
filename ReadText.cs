using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using System.IO;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine;


/// <summary>
/// The main script that reads in the file describing all the survey questions and generates it according to the order. 
/// This script also describes all the UI game-objects that will be produced in Unity to build the survey. 
/// 
/// Author: Malavika Kalani
/// Date: July 29, 2022
/// </summary>
public class ReadText : MonoBehaviour{

    public GameObject questionBox; //placeholder for all types of questions (UI Object type: Text)
    public GameObject answerBox; //placeholder for user answers (UI Object type: Input Field)
    public GameObject toggle; //placeholder for multiple choice options (UI Object type: Toggle)
    public GameObject slider; //placeholder for likert scale values (UI Object type: slider)
    public GameObject endPoint; //display the text of the high and low endpoints of likert slider (UI Object type: Text) 
    public GameObject dropdown; //placeholder for drop-down menu holding the options. (UI Object type: Dropdown) 
    public GameObject image; //placeholder for the image to be displayed. (UI Object type: Raw Image)
    public GameObject shortAnswerButton; //button to store the answer for short-answer questions. (UI Object type: Button)
    public GameObject multipleChoiceButton; //button to store the selected option for multiple-choice questions. (UI Object type: Button)
    public GameObject dropdownButton; //button to store the selcted menu option for dropdown questions. (UI Object type: Button)
    public GameObject likertButton; //button to store the slider value chosen by the user for likert questions. (UI Object type: Button)
    public GameObject submitButton; //button to write all the survey responses to a file and exit the survey. (UI Object type: Button)
   



    private int currentLine = 0;
    IEnumerable<string> allLines; //contains all the lines read in from the text file holdoing the questions.
    private List<Question> allQuestions; //stores each question as it reads it from the file. 
    public int lineCount; //keeps track of the number of questions read in. 
    public GameObject canvas; //parent game object for all other game objects used in the survey. 

    /// <summary>
    /// This shows the different types of questions that can be produced by this script.
    /// </summary>
    public enum QuestionType
    {
        Shortanswer,
        Multiplechoice,
        Likert,
        Dropdown
    }

    /// <summary>
    /// Reads in the corresponding file and 
    /// </summary>
    private void Start()
    {
        string filePath = @"C:\Users\makalani\Desktop\APP-textfile.txt";
        if (File.Exists(filePath))
        {
            allLines = File.ReadAllLines(filePath); //converting to a collection of lines
            lineCount = allLines.Count() - 2; //denotes the total number of questions 
            allQuestions = new List<Question>(); //array to store all questions
        }

    }

    /// <summary>
    /// This function is responsible for reading all the contents of the file and storing it into a list split with every new line.
    /// The switch statements help generate the different type of questions as they are read in. 
    /// </summary>
    public void generate()
    {
            if (allLines.Count() - 1 == currentLine) //if you have generated all questions -- time to end the survey. 
            {
                Question.refreshScreen(canvas, true); //creates a fresh screen
                //displays the text to indicate that the survey has ended
                questionBox.GetComponent<Text>().text = "Thank you for completing the survey!"; 
                questionBox.transform.localPosition = new Vector2(-15, 90);
                questionBox.SetActive(true);
                //create the submit button for the user to click to end the survey
                submitButton.transform.SetParent(questionBox.transform.parent); 
                submitButton.transform.localPosition = new Vector2(-15, 60);
                submitButton.transform.GetChild(0).GetComponent<Text>().text = "SUBMIT";
                submitButton.SetActive(true);
            }
            else
            {
                string line = allLines.ElementAt(currentLine); //retrieve the current line 
                string[] wordValues = line.Split(','); //use a string array to store each component of the line separated by commas as formatted in the file
                string questionType = wordValues[0]; //denotes the type of question you need to produce
                switch (questionType)
                {
                    //multiple choice questions -- holds the question, the number of options and the option wording. 
                    case "multiple-choice":
                        //if an image is provided with the question 
                        if (wordValues[2] == "image included")
                        {
                            string[] answers = MultipleChoice.createOptions(wordValues); //array that stores the options to choose from
                            int numOptions = Int16.Parse(wordValues[4]); //number of multiple choice options
                            Question mc = new MultipleChoice(wordValues[1], questionBox,numOptions, answers, toggle,image);
                            mc.myWording = wordValues[1]; //what the question is asking
                            allQuestions.Add(mc); //store questions
                            //Write the question along with its components to the Unity Screen
                            Question.refreshScreen(canvas, false); //cretaes a fresh screen
                            mc.writeQuestion(); //writes question to screen
                            mc.writeOptions(); //writes multiple choice options to screen
                            string imageName = wordValues[3]; 
                            mc.displayImage(imageName); //image is displayed
                            mc.addButton(questionType,multipleChoiceButton); //button to store the answer and move to the next question that is generated
                        }
                        else
                        {
                            string[] answers = MultipleChoice.createOptions(wordValues); //array that stores the options to choose from
                            int numOptions = Int16.Parse(wordValues[4]); //number of multiple choice options
                            Question mc = new MultipleChoice(wordValues[1], questionBox,numOptions, answers, toggle, image);
                            mc.myWording = wordValues[1]; //what the question is asking
                            allQuestions.Add(mc); //store questions
                            //Write the question along with its components to the Unity Screen
                            Question.refreshScreen(canvas, false);  //creates a fresh screen
                            mc.writeQuestion(); //writes question to screen
                            mc.writeOptions(); //writes multiple choice options to screen
                            mc.addButton(questionType, multipleChoiceButton); //button to store the answer and move to the next question that is generated
                        }
                        break;

                    //short-answer questions -- holds the question and an input field for the user to enter the answer
                    case "short-answer":
                        //if an image is provided with the question 
                        if (wordValues[2] == "image included")
                        {
                            Question sa = new ShortAnswer(wordValues[1], questionBox,answerBox,image);
                            sa.myWording = wordValues[1]; //what the question is asking
                            allQuestions.Add(sa); //store questions
                            //Write the question along with its components to the Unity Screen
                            Question.refreshScreen(canvas, false); //creates a fresh screen
                            sa.writeQuestion(); //writes question to screen
                            sa.createInputField(); //creates input field for user to enter answer
                            string imageName = wordValues[3];
                            sa.displayImage(imageName); //image is displayed
                            sa.addButton(questionType, shortAnswerButton); //button to store the answer and move to the next question that is generated  

                        }
                        else
                        {
                            Question sa = new ShortAnswer(wordValues[1], questionBox,answerBox);
                            sa.myWording = wordValues[1]; //what the question is asking
                            allQuestions.Add(sa); //store questions
                            //Write the question along with its components to the Unity Screen
                            Question.refreshScreen(canvas, false); //creates a fresh screen
                            sa.writeQuestion(); //writes question to screen
                            sa.createInputField(); //creates input field for user to enter answer
                            sa.addButton(questionType, shortAnswerButton); //button to store the answer and move to the next question that is generated 

                        }
                        break;

                    //likert questions -- holds the question and the slider for user to drag the value accordingly
                    case "likert":
                        //if an image is provided with the question
                        if (wordValues[2] == "image included")
                        {
                            Question li = new Likert(wordValues[1], questionBox, slider, endPoint);
                            li.myWording = wordValues[1]; //what the question is asking
                            allQuestions.Add(li); //store questions
                            //Write the question along with its components to the Unity Screen
                            Question.refreshScreen(canvas, false); //creates a fresh screen
                            li.writeQuestion(); //writes question to screen
                            li.createLikert(); //creates the slider for the user to answer the question
                            li.writeEndPoint(wordValues); //indicates the end points on the slider
                            string imageName = wordValues[3];
                            li.displayImage(imageName); //image is displayed
                            li.addButton(questionType, likertButton); //button to store the answer and move to the next question that is generated 
                        }
                        else
                        {
                            Question li = new Likert(wordValues[1], questionBox, slider, endPoint);
                            li.myWording = wordValues[1]; //what the question is asking
                            allQuestions.Add(li); //store questions
                            //Write the question along with its components to the Unity Screen
                            Question.refreshScreen(canvas, false); //creates a fresh screen
                            li.writeQuestion(); //writes question to screen
                            li.createLikert(); //creates the slider for the user to answer the question
                            li.writeEndPoint(wordValues); //indicates the end points on the slider
                            li.addButton(questionType, likertButton); //button to store the answer and move to the next question that is generated 
                        }
                        break;

                    //dropdown questions -- holds the question and then menu options for the user to pick from
                    case "dropdown":
                        //if an image is provided with the question
                        if (wordValues[2] == "image included")
                        {
                            string[] options = DropdownMenu.createOptions(wordValues); //array that stores the options to choose from on the dropdown menu
                            int numMenuOptions = Int16.Parse(wordValues[4]); //number of options on the dropdown menu
                            Question dr = new DropdownMenu(wordValues[1], questionBox, dropdown, options, numMenuOptions,image);
                            dr.myWording = wordValues[1]; //what the question is asking
                            allQuestions.Add(dr); //store questions
                            //Write the question along with its components to the Unity Screen
                            Question.refreshScreen(canvas, false); //creates a fresh screen
                            dr.writeQuestion(); //writes question to screen
                            dr.createMenu(); //creates dropdown menu and writes its options
                            string imageName = wordValues[3];
                            dr.displayImage(imageName); //image is displayed
                            dr.addButton(questionType, dropdownButton); //button to store the answer and move to the next question that is generated 
                        }
                        else
                        {
                            string[] options = DropdownMenu.createOptions(wordValues); //array that stores the options to choose from on the dropdown menu
                            int numMenuOptions = Int16.Parse(wordValues[4]); //number of options on the dropdown menu
                            Question dr = new DropdownMenu(wordValues[1], questionBox, dropdown, options, numMenuOptions);
                            dr.myWording = wordValues[1]; //what the question is asking
                            allQuestions.Add(dr); //store questions
                            //Write the question along with its components to the Unity Screen
                            Question.refreshScreen(canvas, false); //creates a fresh screen
                            dr.writeQuestion(); //writes question to screen
                            dr.createMenu(); //creates dropdown menu and writes its options 
                            dr.addButton(questionType, dropdownButton); //button to store the answer and move to the next question that is generated 
                        }
                        break;

                }
                currentLine++; //move to next line to generate the next question in the file
                
        }
        }
}

    

    

    



