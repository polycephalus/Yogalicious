using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Program : MonoBehaviour {
    List<Exercise> exerciseList = new List<Exercise>();

    public bool viewMode;

    private bool isScreen;

    public Toggle isSelected;
    public Text nameText;
    public Text numOfExercises;
    public Text exerciseNameText;
    public Text descriptionText;
    public Text intensityText;
    public Text difficultyText;

    public GameObject button;
    public Transform listObject;

    public Button prevExerciseButton;
    public Button nextExerciseButton;

    private bool isCustom;
    private bool isFavourite = false;
    private string name;
    private int currentExerciseIndex = 0;

    public Program(bool custom, string name, List<Exercise> e)
    {
        this.isCustom = custom;
        this.name = name;
        addExercises(e);
        addButtons();
    }

    //GET-FUNKTIONER GET-FUNKTIONER GET-FUNKTIONER GET-FUNKTIONER GET-FUNKTIONER GET-FUNKTIONER 

    public List<Exercise> getExerciseList()
    {
        return exerciseList;
    }

    public VideoClip[] getVideoClips()
    {
        VideoClip[] vc = new VideoClip[exerciseList.Count];
        for (int i = 0; i < exerciseList.Count; i++)
        {
            Exercise e = exerciseList[i];
            vc[i] = e.getVideoClip();
        }
        return vc;
    }

    public void Update() {
        if (isScreen)
        {
            if (currentExerciseIndex == 0)
            {
                prevExerciseButton.interactable = false;
            }
            if (currentExerciseIndex == exerciseList.Count - 1)
            {
                nextExerciseButton.interactable = false;
            }

        }
    }

    public bool getContainsExercise(string name)
    {
        bool contains = false;
        foreach(Exercise e in exerciseList)
        {
            if (e.getName().Equals(name))
            {
                contains = true;
            }
        }
        return contains;
    }

    private string getConvertDifficultyToString(int difInt)
    {
        string dif = "";
        switch (difInt)
        {
            case 1:
                dif = "Beginner";
                break;
            case 2:
                dif = "Intermediate";
                break;
            case 3:
                dif = "Expert";
                break;
        }
        return dif;
    }

    private string getConvertIntensityToString(int intensityInt)
    {
        string intensity = "";
        switch (intensityInt)
        {
            case 1:
                intensity = "Low";
                break;
            case 2:
                intensity = "Medium";
                break;
            case 3:
                intensity = "High";
                break;
        }
        return intensity;
    }

    public int getDifficulty()
    {
        int sumOfDifficulty = 0;
        if(exerciseList.Count != 0)
        {
            for (int i = 0; i < exerciseList.Count; i++)
            {
                Exercise e = exerciseList[i];
                sumOfDifficulty += e.getDifficulty();
            }
        }
        return (int)sumOfDifficulty / exerciseList.Count;
    }

    public int getIntensity()
    {
        int sumOfIntensity = 0;
        if (exerciseList.Count != 0)
        {
            for (int i = 0; i < exerciseList.Count; i++)
            {
                Exercise e = exerciseList[i];
                sumOfIntensity += e.getIntensity();
            }
        }
        return (int)sumOfIntensity / exerciseList.Count;
    }

    public bool getIsCustom()
    {
        return isCustom;
    }

    public bool getIsFavourite()
    {
        return isFavourite;
    }

    public bool getIsSelected()
    {
        return isSelected.isOn;
    }

    public string getName()
    {
        return name;
    }

    //SET-FUNKTIONER SET-FUNKTIONER SET-FUNKTIONER SET-FUNKTIONER SET-FUNKTIONER SET-FUNKTIONER 

    public void setIsFavourite(bool b)
    {
        isFavourite = b;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    //Här ändras övningsindexet beroende på vilket knapp som aktiveras.------------------------------------------
    public void setNextExercise(bool fwd)
    {
        Debug.Log("PROG. INDEX: " + currentExerciseIndex);
        if (fwd && currentExerciseIndex < exerciseList.Count-1)
        {
            //prevExerciseButton.interactable = true;
            currentExerciseIndex++;
            updateText();
        }
        else if (!fwd && currentExerciseIndex >= 0)
        {
            //nextExerciseButton.interactable = true;
            currentExerciseIndex--;
            updateText();
        }
        else
        {
            Debug.Log("Out of index!");
        }
    }

    public void gotoPrevious() {
        nextExerciseButton.interactable = true;
        if (currentExerciseIndex > 0)
        {
            prevExerciseButton.interactable = true;
            currentExerciseIndex--;
            updateText();
        }
    }

    public void gotoNext() {
        prevExerciseButton.interactable = true;
        if (currentExerciseIndex < exerciseList.Count - 1)
        {
            nextExerciseButton.interactable = true;
            currentExerciseIndex++;
            updateText();
        }
    }

    public void setValues(bool isScreen, List<Exercise> eList, string name, string exerciseName, string intensity, string difficulty)
    {
        this.isScreen = isScreen;
        if (isScreen)
        {
            exerciseList = eList;
            nameText.text = name;
            exerciseNameText.text = exerciseName;
            intensityText.text = intensity;
            difficultyText.text = difficulty;
        }
        else
        {
            if (exerciseName.Equals(""))
            {
                exerciseList = eList;
                nameText.text = name;
            }
            else
            {
                nameText.text = name;
                //numOfExercises.text = "" + eList.Count;
                intensityText.text = intensity;
                difficultyText.text = difficulty;
            }
        }
    }

    //ÖVRIGA FUNKTIONER ÖVRIGA FUNKTIONER ÖVRIGA FUNKTIONER ÖVRIGA FUNKTIONER ÖVRIGA FUNKTIONER ÖVRIGA FUNKTIONER 

    //Här uppdateras övningsknappens text.
    private void updateText()
    {
        Exercise e = exerciseList[currentExerciseIndex];
        exerciseNameText.text = e.getName();
        descriptionText.text = e.getDescription();
        intensityText.text = getConvertIntensityToString(e.getIntensity());
        difficultyText.text = getConvertDifficultyToString(e.getDifficulty());
    }

    public void removeExercise(Exercise e)
    {
        for(int i = 0; i < exerciseList.Count; i++)
        {
            Exercise test = exerciseList[i];
            if (e.getName().Equals(test))
            {
                exerciseList.RemoveAt(i);
            }
        }
    }

    //ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER 

    //Här läggs programknappar till i den grafiska listan.
    public void addButtons()
    {
        if (viewMode)
        {
            for (int i = 0; i < exerciseList.Count; i++)
            {
                Exercise e = exerciseList[i];
                GameObject tempButton = button;
                tempButton.GetComponent<Exercise>().setText(e.getName(), getConvertIntensityToString(e.getIntensity()), getConvertDifficultyToString(e.getDifficulty()));
                tempButton = Instantiate<GameObject>(button, listObject);
            }
        }
    }

    public void addExercise(Exercise e)
    {
        if (!getContainsExercise(e.getName()))
        {
            exerciseList.Add(e);
        }
        else
        {
            Debug.Log("Finns redan!");
        }
    }

    //Här läggs mottagna övningar in i exerciselistan.
    private void addExercises(List<Exercise> e)
    {
        for(int i = 0; i < e.Count; i++)
        {
            Exercise eTemp = e[i];
            exerciseList.Add(eTemp);
        }
    }
}
