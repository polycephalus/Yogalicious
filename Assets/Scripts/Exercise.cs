using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Exercise : MonoBehaviour {

    public Text nameText;
    public Text intensityText;
    public Text difficultyText;

    private VideoClip videoClip;

    public Image intensityImage;

    private bool screenActive = false;
    private bool isFavourite;
    private int difficulty;
    private int intensity;
    private string description;
    private string muscleGroup;
    private string names;

    public Exercise(string name, string description, string muscleGroup, int intensity, int difficulty, VideoClip video)
    {
        this.names = name;
        this.description = description;
        this.muscleGroup = muscleGroup;
        this.intensity = intensity;
        this.difficulty = difficulty;
        videoClip = video;
    }

    public bool getIsFavourite()
    {
        return isFavourite;
    }
    public string getName()
    {
        return names;
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
    public VideoClip getVideoClip()
    {
        return videoClip;
    }

    public void setScreenActive(bool isActive)
    {
        screenActive = isActive;
    }
    public void setIsFavourite(bool b)
    {
        isFavourite = b;
    }

    private string integerToString(int x)
    {
        return "" + x;
    }

    public void setText(string name, string intensity, string difficulty)
    {
        nameText.text = name;
        intensityText.text = intensity;
        difficultyText.text = difficulty;
    }
}
