using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusclesChoice : MonoBehaviour
{
    public string Muscle = "Muscles";

    public void Chest()
    {
        Muscle = "Chest";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Chest";
    }
    public void Core()
    {
        Muscle = "Core";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Core";
    }
    public void Arms()
    {
        Muscle = "Arms";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Arms";
    }
    public void Legs()
    {
        Muscle = "Legs";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Legs";
    }
    public void Back()
    {
        Muscle = "Back";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Back";
    }
    public void Glutes()
    {
        Muscle = "Glutes";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Glutes";
    }
    public void Any()
    {
        Muscle = "Any";
        GameObject.Find("TextMuscle").GetComponentInChildren<Text>().text = "Any";
    }

}

