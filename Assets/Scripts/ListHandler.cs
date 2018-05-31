using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ListHandler : MonoBehaviour {
    public bool isButton;
    public bool isExercise;
    public bool hasEditableProgram;
    public bool hasAddableExercise;
    public bool hasDeletableExercise;

    //Scener
    public bool isMainScene;
    public bool isExerciseView;
    public bool isExerciseScene;
    public bool isProgramEditView;
    public bool isProgramView;
    public bool isProgramScene;
    public bool isFavourites;
    public bool isSearchView;

    public InputField programNameField;
    public GameObject button;
    public GameObject screen;
    public Transform listObject;
    public Transform canvas;
    public VideoClip[] videoClips;

    private bool toggleExercise, toggleProgram;
    private bool screenActive = false;
    private static bool firstTimeActive = true;

    private int[] intensity = { 1, 2, 1, 1, 3, 1, 1, 1, 2 };
    private int[] difficulty = { 1, 3, 2, 1, 1, 1, 2, 1, 3 };                           
    private string[] description = { "Sit down with your legs bent in front of you. Lean back and lift your feet up until your legs are parallel to the floor. Keeping your knees bent, draw your thighs towards your chest and lift your chest towards the thighs. Squeeze your legs together. Extend your arms forward, parallel to the floor. Work your legs and engage your core. Draw the shoulder blades back and lift your chest. Stay in position for 20 seconds.", "Lie on your back with your knees bent and soles down on the floor. Press your lower back down. Lift your hips slowly up from the floor. Continue to roll slowly up, vertebrae by vertebrae and keep focus on keeping your lower back long. Tuck your shoulders underneath your chest. Place your arms either parallel to your body or interlace your hands under your uplifted body. Stay in position for 30 seconds to 1 minute.", "Come onto the floor on your hands and knees. Tuck your toes up and lift your hips towards the ceiling. Straighten your knees, draw your thighs back. If possible, put your heels down on the ground. Bring attention to your hands and make sure they are parallel to each other and parallel to the rest of your body. Spread your fingers. Your elbows should be straight. Breathe smoothly and stay in position for 30 seconds to 2 minutes.", "Sit down on the floor with your legs straight in front of you. Bend your knees and take your left leg under your right. Placing the left foot on the outside of your right hip and your right foot on the outside of your left leg. Place your right hand behind you and your left upper arm on the outside of your right knee. Your left palm should now be facing outwards along with your upper body. Stay in position for 30 seconds to 1 minute and repeat on the left side.", "Stand up and put your hands on your hips and spread your legs wide apart. Turn your right foot out. Align your heels with each other. Bend your right knee so it’s directly above your ankle. Extend your arms away from each other and keep them align. Move your head so you can gaze towards your right fingers. Stay in position from 30 seconds to 1 minute. Then reverse your feet and repeat for the same length of time on the left side.", "Start in upright position. Take a step back with your right foot and rotate it outwards. Bend your left knee and bring your arms up towards the ceiling and engage your core. Keep your arms shoulder-width apart and stay in position for 30 seconds up to 1 minute. Repeat on the other side", "Sit down with your legs stretched out straight in front of you. Slowly bring your feet towards your body with your heels as close to your pelvis as you possibly can. Always try to keep the outer edges of your feet firmly on the floor. Keep your back straight and stay in position for 30 seconds up to 1 minute.", "Stand on your hands and knees shoulder-width apart. Slowly lean back with your knees bent in a praying-like pose while keeping your arms stretched out at the starting position. Stay in position for 30 seconds up to 1 minute.", "Start in the Downward dog position with your arms shoulder-width apart. Swing your right leg up towards the ceiling and in one swift motion, bend the leg and bring it in under your body with the knee between your hands. Lean over your leg and stretch your arms forward parallel to the floor. For a more advanced level, bring yourself up in a sitting position and grab your left foot with your left hand.Stay in position for 30 seconds up to 1 minute. Repeat on the other leg." };
    private string[] muscleGroup = { "Core", "Glutes", "Arms", "Back", "Legs", "Legs", "Core", "Glutes", "Legs"};
    private string[] names = { "Boat Pose", "Bridge", "Downward Dog", "Half Lord of the Fishes", "Warrior II", "Warrior I", "Bound Angle", "Child Pose", "Pigeon Pose"};
    //Program-namn
    private string[] programName = { "Yo-Go-Gal!", "Meditation", "Stretch", "Hardcore", "Hidden Dragon" };

    private static Exercise selectedExercise;
    private static Exercise selectedExerciseEdit;
    private static Program selectedProgram;

    private static List<Exercise> exerciseList = new List<Exercise>();
    private static List<Program> programList = new List<Program>();
    //Exerciselistan som skickas till program-objektet. Max 50 övningar per program
    private static List<Exercise> tempProgram = new List<Exercise>();
    private static List<Exercise> tempProgramAdded = new List<Exercise>();

    //Här skapas inbyggda program och övningar.
    private void Awake()
    {
        if (!isButton)
        {        
            if (firstTimeActive)
            {
                addExercisesAndPrograms();
                firstTimeActive = false;
            }
            if (isExerciseView)
            {
                screen.GetComponent<ExerciseScreen>().setText(selectedExercise.getName(), selectedExercise.getDescription(), selectedExercise.getMuscleGroup(), getConvertIntensityToString(selectedExercise.getIntensity()), getConvertDifficultyToString(selectedExercise.getDifficulty()));
                Debug.Log("ExerciseView");
            }
            if (isExerciseScene)
            {
                addButtonsToContent();
                Debug.Log("ExerciseScene");
            }
            if (isProgramEditView)
            {
                Program p = selectedProgram;
                screen.GetComponent<Program>().setValues(false, selectedProgram.getExerciseList(), selectedProgram.getName(), "", getConvertIntensityToString(selectedProgram.getIntensity()), getConvertDifficultyToString(selectedProgram.getDifficulty()));
                screen.GetComponent<Program>().addButtons();
                Debug.Log("ProgramEditView");
            }
            if (isProgramView)
            {
                Exercise e = selectedProgram.getExerciseList()[0];
                screen.GetComponent<Program>().setValues(true, selectedProgram.getExerciseList(), selectedProgram.getName(), e.getName(), getConvertIntensityToString(selectedProgram.getIntensity()), getConvertDifficultyToString(selectedProgram.getDifficulty()));
                screen.GetComponent<Program>().addButtons();
                Debug.Log("ProgramView");
            }
            if (isProgramScene)
            {
                addButtonsToContent();
                Debug.Log("ProgramScene");
            }
            if (isFavourites)
            {
                addFavouriteButtons();
                Debug.Log("FavouritesScene");
            }
        }
    }

    //GET-FUNKTIONER GET-FUNKTIONER GET-FUNKTIONER GET-FUNKTIONER GET-FUNKTIONER GET-FUNKTIONER GET-FUNKTIONER GET-FUNKTIONER GET-FUNKTIONER 

    public Exercise getSelectedExercise()
    {
        return selectedExercise;
    }

    public Exercise getSelectedExerciseEdit()
    {
        return selectedExerciseEdit;
    }

    public Program getSelectedProgram()
    {
        return selectedProgram;
    }

    public List<Exercise> getExerciseList()
    {
        return exerciseList;
    }

    public List<Program> getProgramList()
    {
        return programList;
    }

    //Här returneras ett övningsobjekt.
    private Exercise getExerciseByName(string name)
    {
        Exercise e = null;
        for (int i = 0; i < exerciseList.Count; i++)
        {
            Exercise temp = exerciseList[i];
            if (name.Equals(temp.getName()))
            {
                e = temp;
            }
        }
        return e;
    }

    //Här returneras ett programobjekt.
    private Program getProgramByName(string name)
    {
        Program p = null;
        for (int i = 0; i < programList.Count; i++)
        {
            Program temp = programList[i];
            if (name.Equals(temp.getName()))
            {
                p = temp;
            }
        }
        return p;
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

    private bool getNameExists(string name)
    {
        if (programList.Count == 0)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < programList.Count; i++)
            {
                Program p = programList[i];
                if (name.Equals(p.getName()))
                {
                    return true;
                }
            }
            return false;
        }
    }


    //SET-FUNKTIONER SET-FUNKTIONER SET-FUNKTIONER SET-FUNKTIONER SET-FUNKTIONER SET-FUNKTIONER SET-FUNKTIONER SET-FUNKTIONER SET-FUNKTIONER 



    //Här ska exercise-knappens textobjekt för namnet i Unity anges i parametrarna. Sedan sätts selectedExercise till den övning med det angivna namnet.
    public void setSelectedExercise(Text txt)
    {
        selectedExercise = getExerciseByName(txt.text);
    }

    public void setSelectedExerciseEdit(Text txt)
    {
        selectedExerciseEdit = getExerciseByName(txt.text);
    }

    //Samma som övre fast för program.
    public void setSelectedProgram(Text txt)
    {
        selectedProgram = getProgramByName(txt.text);
    }

    public void setExerciseAsFavourite()
    {
        if (selectedExercise.getIsFavourite())
        {
            selectedExercise.setIsFavourite(false);
            Debug.Log("No Fav");
        }
        else if (!selectedExercise.getIsFavourite())
        {
            selectedExercise.setIsFavourite(true);
            Debug.Log("Fav");
        }
    }

    public void setProgramAsFavourite()
    {
        if (selectedProgram.getIsFavourite())
        {
            selectedProgram.setIsFavourite(false);
            Debug.Log("No Fav");
        }
        else if (!selectedProgram.getIsFavourite())
        {
            selectedProgram.setIsFavourite(true);
            Debug.Log("Fav");
        }
    }

    //ÖVRIGA FUNKTIONER ÖVRIGA FUNKTIONER ÖVRIGA FUNKTIONER ÖVRIGA FUNKTIONER ÖVRIGA FUNKTIONER ÖVRIGA FUNKTIONER ÖVRIGA FUNKTIONER 

    public void deleteExerciseFromTempProgram(Text str)
    {
        for(int i = 0; i < tempProgram.Count; i++)
        {
            Exercise e = tempProgram[i];
            if (e.getName().Equals(str.text))
            {
                tempProgram.RemoveAt(i);
            }
        }
    }

    public void deleteExerciseFromProgram(Text str)
    {
        Exercise e = getExerciseByName(str.text);
        for(int i = 0; i < programList.Count; i++)
        {
            Program p = programList[i];
            if(p == selectedProgram)
            {
                programList[i].removeExercise(e);
            }
        }
    }

    public void eraseProgram(Text str)
    {
        string name = str.text;
        Program p = getProgramByName(name);
        for(int i = 0; i < programList.Count; i++)
        {
            Program test = programList[i];
            if (p.getName().Equals(test.getName()))
            {
                programList.RemoveAt(i);
            }
        }
    }

    //Här tas det temporära programmet bort.
    private void eraseArray()
    {
        while (tempProgram.Count > 0)
        {
            tempProgram.RemoveAt(tempProgram.Count - 1);
        }
        while (tempProgramAdded.Count > 0)
        {
            tempProgram.RemoveAt(tempProgramAdded.Count - 1);
        }
    }


    //ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER ADD-FUNKTIONER 

    //Här läggs endast ett program till direkt efter det har skapats.
    private void addButton(Program p)
    {
        GameObject tempButton = button;
        tempButton.GetComponent<Program>().setValues(false, null, p.getName(), "", getConvertIntensityToString(p.getIntensity()), getConvertDifficultyToString(p.getDifficulty()));
        tempButton = Instantiate<GameObject>(button, listObject);
    }

    //Här läggs både program- och övningsknappar till i de grafiska listorna.
    private void addButtonsToContent()
    {
        if (hasAddableExercise)
        {
            for (int i = 0; i < exerciseList.Count; i++)
            {
                Exercise e = exerciseList[i];
                GameObject tempButton = button;
                tempButton.GetComponent<Exercise>().setText(e.getName(), getConvertIntensityToString(e.getIntensity()), getConvertDifficultyToString(e.getDifficulty()));
                tempButton = Instantiate<GameObject>(button, listObject);
            }
        }
        else if (hasDeletableExercise)
        {
            for (int i = 0; i < tempProgram.Count; i++)
            {
                Exercise e = tempProgram[i];
                GameObject tempButton = button;
                tempButton.GetComponent<Exercise>().setText(e.getName(), getConvertIntensityToString(e.getIntensity()), getConvertDifficultyToString(e.getDifficulty()));
                tempButton = Instantiate<GameObject>(button, listObject);
            }
        }
        else if (hasEditableProgram)
        {
            for (int i = 0; i < programList.Count; i++)
            {
                Program p = programList[i];
                if (p.getIsCustom())
                {
                    GameObject tempButton = button;
                    tempButton.GetComponent<Program>().setValues(false, null, p.getName(), "", getConvertIntensityToString(p.getIntensity()), getConvertDifficultyToString(p.getDifficulty()));
                    tempButton = Instantiate<GameObject>(button, listObject);
                }
            }
        }
        else
        {
            if (isExercise)
            {
                for (int i = 0; i < exerciseList.Count; i++)
                {
                    Exercise e = exerciseList[i];
                    GameObject tempButton = button;
                    tempButton.GetComponent<Exercise>().setText(e.getName(), getConvertIntensityToString(e.getIntensity()), getConvertDifficultyToString(e.getDifficulty()));
                    tempButton = Instantiate<GameObject>(button, listObject);
                }
            }
            else
            {
                for (int i = 0; i < programList.Count; i++)
                {
                    Program p = programList[i];
                    GameObject tempButton = button;
                    tempButton.GetComponent<Program>().setValues(false, null, p.getName(), "", getConvertIntensityToString(p.getIntensity()), getConvertDifficultyToString(p.getDifficulty()));
                    tempButton = Instantiate<GameObject>(button, listObject);
                }
            }
        }
    }

    public void addNewDeleteableExerciseButtonsToOld()
    {
        for (int i = 0; i < tempProgram.Count; i++)
        {
            Exercise e = tempProgram[i];
            tempProgramAdded.Add(e);
        }
        for(int i = 0; i < tempProgram.Count; i++)
        {
            tempProgram.RemoveAt(i);
        }
    }

    public void addEditProgramButtonsToContent()
    {
        for (int i = 0; i < programList.Count; i++)
        {
            Program p = programList[i];
            if (p.getIsCustom() && !p.getContainsExercise(selectedExercise.getName()))
            {
                GameObject tempButton = button;
                tempButton.GetComponent<Program>().setValues(false, null, p.getName(), "", getConvertIntensityToString(p.getIntensity()), getConvertDifficultyToString(p.getDifficulty()));
                tempButton = Instantiate<GameObject>(button, listObject);
            }
        }
    }

    //Här läggs de inbyggda övningarna in i Exerciselistan.
    private void addExercisesToList()
    {
        for (int i = 0; i < names.Length; i++)
        {
            exerciseList.Add(new Exercise(names[i], description[i], muscleGroup[i], intensity[i], difficulty[i], videoClips[i]));
        }
    }

    //Här läggs basprogram och basövningar till.
    private void addExercisesAndPrograms()
    {
        addExercisesToList();
        addExerciseToArrayByStr("Boat Pose");
        addExerciseToArrayByStr("Half Lord of the Fishes");
        addExerciseToArrayByStr("Downward Dog");
        addProgramsToList(tempProgram, programName[0]);
        addExerciseToArrayByStr("Bridge");
        addProgramsToList(tempProgram, programName[1]);
        addExerciseToArrayByStr("Warrior II");
        addExerciseToArrayByStr("Bridge");
        addExerciseToArrayByStr("Half Lord of the Fishes");
        addExerciseToArrayByStr("Warrior I");
        addExerciseToArrayByStr("Bound Angle");
        addExerciseToArrayByStr("Child Pose");
        addExerciseToArrayByStr("Pigeon Pose");
        addProgramsToList(tempProgram, programName[2]);
        addExerciseToArrayByStr("Warrior I");
        addExerciseToArrayByStr("Bound Angle");
        addExerciseToArrayByStr("Child Pose");
        addExerciseToArrayByStr("Pigeon Pose");
        addProgramsToList(tempProgram, programName[3]);
        addExerciseToArrayByStr("Half Lord of the Fishes");
        addExerciseToArrayByStr("Warrior I");
        addExerciseToArrayByStr("Child Pose");
        addExerciseToArrayByStr("Bound Angle");
        addProgramsToList(tempProgram, programName[4]);
    }

    //Här hittas en övning efter knapp och läggs till i det temporära programmet.
    public void addExerciseToArrayByBtn(Text txt)
    {
        string str = txt.text;
        for (int i = 0; i < exerciseList.Count; i++)
        {
            Exercise temp = exerciseList[i];
            if (str.Equals(temp.getName()))
            {
                tempProgram.Add(temp);
            }
        }
    }

    //Här hittas en övning efter namn och läggs till i det temporära programmet.
    private void addExerciseToArrayByStr(string str)
    {
        for (int i = 0; i < exerciseList.Count; i++)
        {
            Exercise temp = exerciseList[i];
            if (str.Equals(temp.getName()))
            {
                tempProgram.Add(temp);
            }
        }
    }

    public void addExerciseToProgram()
    {
        selectedProgram.addExercise(selectedExercise);
    }

    public void addFavouriteButtons()
    {
        if (isExercise)
        {
            foreach (Exercise e in exerciseList)
            {
                if (e.getIsFavourite())
                {
                    GameObject tempButton = button;
                    tempButton.GetComponent<Exercise>().setText(e.getName(), getConvertIntensityToString(e.getIntensity()), getConvertDifficultyToString(e.getDifficulty()));
                    tempButton = Instantiate<GameObject>(button, listObject);
                }
            }
        }
        else
        {
            foreach (Program p in programList)
            {
                if (p.getIsFavourite())
                {
                    GameObject tempButton = button;
                    tempButton.GetComponent<Program>().setValues(false, null, p.getName(), "", getConvertIntensityToString(p.getIntensity()), getConvertDifficultyToString(p.getDifficulty()));
                    tempButton = Instantiate<GameObject>(button, listObject);
                }
            }
        }
    }

    public void addNewProgram()
    {
        string name = programNameField.text;
        if (!(name.Equals("")))
        {
            if (tempProgram.Count > 0)
            {
                if (!getNameExists(name))
                {
                    Program p = new Program(true, name, tempProgram);
                    programList.Add(new Program(true, programNameField.text, tempProgram));
                    addButton(p);
                    eraseArray();
                }
            }
            else
            {
                Debug.Log("Övningar fattas");
            }
        }
        else
        {
            Debug.Log("Namn fattas");
        }
    }

    //Här tas en övningslista samt ett program-namn emot. De läggs sedan till i programlistan. (OBS!) Endast inbyggda program.
    private void addProgramsToList(List<Exercise> e, string name)
    {
        programList.Add(new Program(false, name, e));
        eraseArray();
    }

    public void addSearchButtons(List<Exercise> eList, List<Program> pList)
    {
        if (toggleExercise)
        {
            for (int i = 0; i < eList.Count; i++)
            {
                Exercise e = eList[i];
                GameObject tempButton = button;
                tempButton.GetComponent<Exercise>().setText(e.getName(), getConvertIntensityToString(e.getIntensity()), getConvertDifficultyToString(e.getDifficulty()));
                tempButton = Instantiate<GameObject>(button, listObject);
            }
        }
        if (toggleProgram)
        {
            for (int i = 0; i < pList.Count; i++)
            {
                Program p = pList[i];
                GameObject tempButton = button;
                tempButton.GetComponent<Program>().setValues(false, null, p.getName(), "", getConvertIntensityToString(p.getIntensity()), getConvertDifficultyToString(p.getDifficulty()));
                tempButton = Instantiate<GameObject>(button, listObject);
            }
        }
    }
}
