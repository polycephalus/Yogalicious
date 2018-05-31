using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedSearch : MonoBehaviour {

    public Image search;
    public Button searchButton;
    public ListHandler listHandler;
    public ExerciseList exerciseListObject;
    public ProgramList programListObject;

    List<Exercise> exerciseList;
    List<Program> programList;
    List<Exercise> searchMatchesList = new List<Exercise>(); //sökning med endast exercises för tilfället
    List<Program> searchMatchesListProgram = new List<Program>(); //sökning med endast exercises för tilfället

    //dropdown
    List<string> optionsIntensity = new List<string>() { "Any", "Low", "Medium", "High" };
    List<string> optionsDifficulty = new List<string>() { "Any", "Beginner", "Intermediate", "Expert" };
    public Dropdown dropdownIntensity; 
    public Dropdown dropdownDifficutly;

    public string lowerNewText;
    public int selectedIntensityIndex;
    public int selectedDifficultyIndex;

    //display results
    public GameObject itemTemplate; //resultat knapp
    public GameObject content; //scroll view content

    //Fredriks skit
    private bool isProgram = true;
    private bool isExercise = true;

    public MusclesChoice mC;

    public GameObject noMatches;
    public GameObject programButton;
    public GameObject exerciseButton;
    public Transform list;

    public void Start()
    {
        listHandler = FindObjectOfType<ListHandler>();
        exerciseList = listHandler.getExerciseList();
        programList = listHandler.getProgramList();

        PopulateDropdowns();
    }

    public void Update()
    {
        //searchExercises(); //button trigger
    }

    public void PopulateDropdowns()
    {
        dropdownIntensity.AddOptions(optionsIntensity);
        dropdownDifficutly.AddOptions(optionsDifficulty);
    }

    public void intensityIndexChanged(int index)
    {
        selectedIntensityIndex = index;
    }

    public void difficultyIntexChanged(int index) {
        selectedDifficultyIndex = index;
    }

    public void textChanged(string newText)
    {
        lowerNewText = newText.ToLower();
    }

    public void toggleProgram()
    {
        if (isProgram)
        {
            isProgram = false;
        }
        else
        {
            isProgram = true;
        }
    }

    public void toggleExercise()
    {
        if (isExercise)
        {
            isExercise = false;
        }
        else
        {
            isExercise = true;
        }
    }

    //FREIDKRIEKRI
    public void sörchExercises()
    {
        bool found = false;
        if (isProgram)
        {
            for (int i = 0; i < programList.Count; i++)
            {               
                Program p = programList[i];
                int intensityIndex = p.getIntensity();
                int difficultyIndex = p.getDifficulty();

                if (p.getName().ToLower().Contains(lowerNewText)
                    && (intensityIndex == selectedIntensityIndex || selectedIntensityIndex == 0)
                    && (difficultyIndex == selectedDifficultyIndex || selectedDifficultyIndex == 0))
                {
                    found = true;
                    GameObject tempButton = programButton;
                    tempButton.GetComponent<Program>().setValues(false, null, p.getName(), "", getConvertIntensityToString(p.getIntensity()), getConvertDifficultyToString(p.getDifficulty()));
                    tempButton = Instantiate<GameObject>(programButton, list);
                }
            }
        }
        if (isExercise)
        {
            for (int i = 0; i < exerciseList.Count; i++)
            {
                Exercise e = exerciseList[i];
                int intensityIndex = e.getIntensity();
                int difficultyIndex = e.getDifficulty();

                if (e.getName().ToLower().Contains(lowerNewText)
                    && (intensityIndex == selectedIntensityIndex || selectedIntensityIndex == 0)
                    && (difficultyIndex == selectedDifficultyIndex || selectedDifficultyIndex == 0)
                    && (mC.GetComponent<MusclesChoice>().getChosenMuscle().Equals(e.getMuscleGroup())
                    || mC.GetComponent<MusclesChoice>().getChosenMuscle().Equals("Any")))
                {
                    found = true;
                    GameObject tempButton = exerciseButton;
                    tempButton.GetComponent<Exercise>().setText(e.getName(), getConvertIntensityToString(e.getIntensity()), getConvertDifficultyToString(e.getDifficulty()));
                    tempButton = Instantiate<GameObject>(exerciseButton, list);
                }
            }
        }
        if (!found)
        {
            noMatches.SetActive(true);
        }
        else
        {
            noMatches.SetActive(false);
        }
    }

    public void clearList()
    {
        for (int i = 0; i < list.transform.childCount; i++)
        {
            Destroy(list.transform.GetChild(i).gameObject);
        }
    }

    //sammansatt sokfunktion
    public void searchExercises()
    {
        Debug.Log("SEARCH RES.: ///////////////////////////////////////////////////");
        searchMatchesList.Clear(); //reset

        foreach (var exercise in exerciseList)
        {
            string lowerName = exercise.getName().ToLower();
            int intensityIndex = exercise.getIntensity();
            int difficultyIndex = exercise.getDifficulty();
            if (lowerName.Contains(lowerNewText) 
                && (intensityIndex == selectedIntensityIndex || selectedIntensityIndex==0)
                && (difficultyIndex == selectedDifficultyIndex || selectedDifficultyIndex == 0))
            {
                //Debug.Log("search res exer.: " + exercise.getName() + ", int.: "+ intensityIndex + ", diff.: "+difficultyIndex);
                searchMatchesList.Add(exercise);
            }
        }

        //reset content (scrollview object)
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }

        //list test - DELETE
        foreach (var item in searchMatchesList) {
            Debug.Log("Matches list: " + item.getName());
            //display panel
            var copy = Instantiate(itemTemplate); //instantiating "prefab"
            copy.transform.SetParent(content.transform); //= content.transform; //adding to scroll view

            //copy.GetComponentInChildren<Text>().text = item.getName();

            //copy.GetComponent<Button>().onClick.AddListener(

            //    () => //anonymous function
            //    {
            //    //search result item button functionality
            //    Debug.Log("result: " + item.getName());
            //    }
            //    );
        }

        //foreach (var program in programList)
        //{
        //    string lowerName = program.getName().ToLower();
        //    if (lowerName.Contains(lowerNewText))
        //    {
        //        Debug.Log("search res prog.: " + program.getName());
        //    }
        //}

    }

    public void resetResults() {
        searchMatchesList.Clear(); //reset
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

}
