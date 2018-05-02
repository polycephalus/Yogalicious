using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exercise : MonoBehaviour {

    public Text nameText;
    public Text intensityText;
    public Text difficultyText;

    private int difficulty;
    private int intensity;
    private string description;
    private string muscleGroup;
    private string name;

    public Exercise(string name, string description, string muscleGroup, int intensity, int difficulty)
    {
        this.name = name;
        this.description = description;
        this.muscleGroup = muscleGroup;
        this.intensity = intensity;
        this.difficulty = difficulty;
    }

    public string getName()
    {
        return name;
    }
    public string getDescription()
    {
        return description;
    }
    public string getMuscleGroup()
    {
        return muscleGroup;
    }
    public int getIntensity()
    {
        return intensity;
    }
    public int getDifficulty()
    {
        return difficulty;
    }

    private string intToString(int x)
    {
        return "" + x;
    }

    public void setText(string name, int intensity, int difficulty)
    {
        nameText.text = name;
        intensityText.text = intToString(intensity);
        difficultyText.text = intToString(difficulty);
    }
}
