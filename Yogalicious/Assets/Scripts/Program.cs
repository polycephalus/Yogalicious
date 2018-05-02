using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Program : MonoBehaviour {
    List<Exercise> exerciseList = new List<Exercise>();

    public Text nameText;
    public Text intensityText;
    public Text difficultyText;

    private bool custom;
    private string name;
    private int difficulty;
    private int intensity;

    public Program(bool custom, string name, Exercise[] e)
    {
        this.custom = custom;
        this.name = name;
        addExercises(e);
    }

    public string getName()
    {
        return name;
    }
    
    public int getDifficulty()
    {
        return difficulty;
    }

    public int getIntensity()
    {
        return intensity;
    }

    public void calcIntensity()
    {
        int sumOfIntensity = 0;
        for(int i = 0; i < exerciseList.Count; i++)
        {
            Exercise e = exerciseList[i];
            sumOfIntensity += e.getIntensity();
        }
        intensity = (int) sumOfIntensity / exerciseList.Count;
    }

    public void calcDifficulty()
    {
        int sumOfDifficulty = 0;
        for (int i = 0; i < exerciseList.Count; i++)
        {
            Exercise e = exerciseList[i];
            sumOfDifficulty += e.getDifficulty();
        }
        difficulty = (int)sumOfDifficulty / exerciseList.Count;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    private void addExercises(Exercise[] e)
    {
        for(int i = 0; i < e.Length; i++)
        {
            exerciseList.Add(e[i]);
        }
    }

    private string integerToString(int x)
    {
        return "" + x;
    }

    public void setText(string name, int intensity, int difficulty)
    {
        nameText.text = name;
        intensityText.text = integerToString(intensity);
        difficultyText.text = integerToString(difficulty);
    }
}
