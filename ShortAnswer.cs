using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// This script holds the class to implement short-answer questions.
/// It inherits from the Question class.
/// 
/// Author: Malavika Kalani
/// Date: 07/29/2022
/// </summary>
public class ShortAnswer : Question {

    
    public GameObject myAnswerBox; //input field for the user
    public GameObject myImage; //placeholder for image

    /// <summary>
    /// Constructor to initialise the components associated with short-answer questions.
    /// </summary>
    /// <param name="question"></param> The string holding the question.
    /// <param name="questionBox"></param> The GameObject that will hold the text of the question. 
    /// <param name="answerBox"></param> The GameObject for user answers.
    public ShortAnswer(string question, GameObject questionBox, GameObject answerBox)
        :base(question,questionBox)
    {
        myAnswerBox = answerBox;
    }

    /// <summary>
    /// Constructor to initialise the components associated with short-answer questions when it includes an image.
    /// </summary>
    /// <param name="question"></param> The string holding the question.
    /// <param name="questionBox"></param> The GameObject that will hold the text of the question. 
    /// <param name="answerBox"></param> The GameObject for user answers.
    /// <param name="image"></param> gameObject to display the associated image.
    public ShortAnswer(string question, GameObject questionBox, GameObject answerBox, GameObject image)
        : base(question, questionBox)
    {
        myImage = image;
        myAnswerBox = answerBox;
    }

    /// <summary>
    /// Creates input field for user to enter answer.
    /// </summary>
    public override void createInputField()
    {
        //GameObject newAnswerBox = Instantiate(myAnswerBox);
        //myAnswerBox.transform.SetParent(myAnswerBox.transform.parent);
        myAnswerBox.transform.localPosition = new Vector2(-70, 100);
        myAnswerBox.SetActive(true);
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
    


