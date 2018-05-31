using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseScreen : MonoBehaviour {

    public Text nameText;
    public Text descriptionText;
    public Text muscleGroupsText;
    public Text intensityText;
    public Text difficultyText;

    public Image intensitsdafpsdaof; //----------------------------------------------!!

    public void setText(string nameText, string descriptionText, string muscleGroupsText, string intensityText, string difficultyText)
    {
        this.nameText.text = nameText;
        this.descriptionText.text = descriptionText;
        this.muscleGroupsText.text = "Muscle Groups: " + muscleGroupsText;
        this.intensityText.text = "Intensity: " + intensityText;
        this.difficultyText.text = "Difficulty: " + difficultyText;
    }
}
