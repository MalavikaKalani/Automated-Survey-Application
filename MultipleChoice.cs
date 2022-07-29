using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

/// <summary>
/// This script holds the class that implements multiple-choice questions.
/// It inherits from the Question class.
/// 
/// Author: Malavika Kalani
/// Date: 07/29/2022
/// </summary>
public class MultipleChoice : Question {
        
        private int myNumOptions; //integer denoting the number of options with the question.
        private string[] myOptions; //string array holding the options the user can select from.
        private GameObject myToggle; //UI Toggle for the multiple choice options.
        private GameObject myImage; //UI Raw Image to display an image associated with the question.

        /// <summary>
        /// Constructor to initialise the components associated with multiple-choice questions.
        /// </summary>
        /// <param name="question"></param> string holding the question.
        /// <param name="questionBox"></param> gameObject that will hold the text of the question.
        /// <param name="numOptions"></param> integer denoting the number of options user can choose from.
        /// <param name="options"></param> string array holding the options for the question.
        /// <param name="toggle"></param> gameObject to display the options user can select.
        public MultipleChoice(string question, GameObject questionBox, int numOptions, string[] options, GameObject toggle)
                : base(question,questionBox)
         {
            
            myNumOptions = numOptions;
            myOptions = options;
            myToggle = toggle;
        
        }

        /// <summary>
        ///  Constructor to initialise the components associated with multiple-choice questions when it includes an image.
        /// </summary>
        /// <param name="question"></param> string holding the question.
        /// <param name="questionBox"></param> gameObject that will hold the text of the question.
        /// <param name="numOptions"></param> integer denoting the number of options user can choose from.
        /// <param name="options"></param> string array holding the options for the question.
        /// <param name="toggle"></param> gameObject to display the options user can select.
        /// <param name="image"></param> gameObject to display the associated image.
        public MultipleChoice(string question, GameObject questionBox, int numOptions, string[] options, GameObject toggle, GameObject image)
                : base(question, questionBox)
        {
                //Debug.Log("Second Constructor");
                myNumOptions = numOptions;
                myOptions = options;
                myToggle = toggle;
                myImage = image;
                //myTexture = texture;
        }


        /// <summary>
        /// Getter for the number of options the user can choose from.
        /// </summary>
        /// <returns></returns> integer denoting the number of options user can choose from. 
        public int getNumOptions()
        {
            return myNumOptions;
        }
        
        /// <summary>
        /// Setter for the number of options that you can choose from.
        /// </summary>
        /// <param name="numOptions"></param> integer denoting the number of options that you can choose from.
        public void setNumOptions(int numOptions)
        {
            //set non-negative requirements
            if (numOptions >= 1)
            {
            myNumOptions = numOptions;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] getOptions()
        {
            return myOptions;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public void setMyOptions(string[] options)
        {
            myOptions = options;
        }

        /// <summary>
        /// Creates the options for the multiple choice question.
        /// </summary>
        /// <param name="wordValues"></param> string array holding the question and options associated with it.
        /// <returns></returns> a string array of the options provided with the multiple choice question.
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
        ///Writes multiple choice options using Toggle UI object.
        /// </summary>
        public override void writeOptions()
        {
            for (int i = 0; i < myOptions.Length; i++)
            {
                GameObject newToggle = Instantiate(myToggle); //create as many toggle answers as the number of options
                newToggle.transform.GetChild(1).GetComponent<Text>().text = myOptions[i];
                newToggle.transform.SetParent(myToggle.transform.parent);
                newToggle.transform.localPosition = new Vector2(-75, 95-20*i);
                newToggle.SetActive(true);
            }
        }

        /// <summary>
        /// Displays the corresponding image using UI Raw Image.
        /// </summary>
        /// <param name="imageName"></param> string denoting the name of image file.
        public override void displayImage(string imageName)
        {
            Texture2D myTexture = Resources.Load(imageName) as Texture2D;
            GameObject newImage = Instantiate(myImage);
            newImage.transform.SetParent(myImage.transform.parent);
            newImage.GetComponent<RawImage>().texture = myTexture;
            newImage.transform.localPosition = new Vector2(-50, 4);
            newImage.SetActive(true);
        }
}


