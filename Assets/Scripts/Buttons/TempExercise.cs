using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempExercise : MonoBehaviour {

    public Text nameText;
    
    private string name;

    public string getName()
    {
        return name;
    }

    public void setText(string name)
    {
        nameText.text = name;
    }
}
