using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusclesChoice : MonoBehaviour
{
    public static string chosenMuscle = "Any";

    public string getChosenMuscle()
    {
        return chosenMuscle;
    }

    public void Chest()
    {
        chosenMuscle = "Chest";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Chest";
    }
    public void Core()
    {
        chosenMuscle = "Core";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Core";
    }
    public void Arms()
    {
        chosenMuscle = "Arms";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Arms";
    }
    public void Legs()
    {
        chosenMuscle = "Legs";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Legs";
    }
    public void Back()
    {
        chosenMuscle = "Back";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Back";
    }
    public void Glutes()
    {
        chosenMuscle = "Glutes";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Glutes";
    }
    public void Any()
    {
        chosenMuscle = "Any";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Any";
    }


}