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
public class Likert : Question {

    public GameObject mySlider; //UI slider to indicate the provided values
    public GameObject myEndPoint; //strings denoting the high and low endpoint on the slider 
    private GameObject myImage; //UI Raw Image to display an image associated with the question.
    //public SaveInput mySaveInput;

    /// <summary>
    /// Constructor to initialise the components associated with likert questions.
    /// </summary>
    /// <param name="question"></param> string holding the question.
    /// <param name="questionBox"></param> gameObject that will hold the text of the question.
    /// <param name="slider"></param> gameObject to display the slider values for the user to drag.
    /// <param name="endPoint"></param> string to denote the endpoints on the likert slider.
    public Likert(string question, GameObject questionBox, GameObject slider, GameObject endPoint)
        : base(question, questionBox)
    {
        mySlider = slider;
        myEndPoint = endPoint;
    }

    /// <summary>
    /// Constructor to initialise the components associated with likert questions when it includes an image.
    /// </summary>
    /// <param name="question"></param> string holding the question.
    /// <param name="questionBox"></param> gameObject that will hold the text of the question.
    /// <param name="slider"></param> gameObject to display the slider values for the user to drag.
    /// <param name="endPoint"></param> string to denote the endpoints on the likert slider.
    /// <param name="image"></param> gameObject to display the associated image.
    public Likert(string question, GameObject questionBox, GameObject slider, GameObject endPoint,GameObject image)
        : base(question, questionBox)
    {
        mySlider = slider;
        myEndPoint = endPoint;
        myImage = image;
    }

    /// <summary>
    /// Creates the UI slider for user to drag values.
    /// </summary>
    public override void createLikert()
    {
            mySlider.transform.localPosition = new Vector2(-35, 65);
            mySlider.SetActive(true);
    }

    /// <summary>
    ///  Writes the endpoints indicating the extremes of the likert slider.
    /// </summary>
    /// <param name="wordValues"></param> string array containing the endpoints that need to be written.
    public override void writeEndPoint(string[] wordValues)
    {
        GameObject newEndPointLow = Instantiate(myEndPoint);
        GameObject newEndPointHigh = Instantiate(myEndPoint);
        newEndPointLow.GetComponent<Text>().text = "Strongly Disagree";
        newEndPointHigh.GetComponent<Text>().text = "Strongly Agree";
        newEndPointLow.transform.SetParent(myEndPoint.transform.parent);
        newEndPointHigh.transform.SetParent(myEndPoint.transform.parent);
        newEndPointLow.transform.localPosition = new Vector2(-75, 75);
        newEndPointHigh.transform.localPosition = new Vector2(85, 75);
        newEndPointLow.SetActive(true);
        newEndPointHigh.SetActive(true);
    }

    
}





