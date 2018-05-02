using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ListHandler : MonoBehaviour {
    List<Exercise> exerciseList = new List<Exercise>();
    List<Program> programList = new List<Program>();
    //List<ExerciseButton> eButtonList = new List<ExerciseButton>();
    //List<ProgramButton> pButtonList = new List<ProgramButton>();

    public GameObject exerciseButton;
    public GameObject programButton;
    public Transform exerciseListObject;
    public Transform programListObject;

    //max 50 övningar per program
    Exercise[] tempProgram = new Exercise[50];

    int nextEmpty = 0;

    int[] difficulty = { 1, 1, 1, 1, 1};
    int[] intensity = { 2, 2, 2, 1, 2};
    string[] description = { "Sit down with your legs bent in front of you. Lean back and lift your feet up until your legs are parallel to the floor. Keeping your knees bent, draw your thighs towards your chest and lift your chest towards the thighs. Squeeze your legs together. Extend your arms forward, parallel to the floor. Work your legs and engage your core. Draw the shoulder blades back and lift your chest. Stay in position for 20 seconds.", "Lie on your back with your knees bent and soles down on the floor. Press your lower back down. Lift your hips slowly up from the floor. Continue to roll slowly up, vertebrae by vertebrae and keep focus on keeping your lower back long. Tuck your shoulders underneath your chest. Place your arms either parallel to your body or interlace your hands under your uplifted body. Stay in position for 30 seconds to 1 minute.", "Come onto the floor on your hands and knees. Tuck your toes up and lift your hips towards the ceiling. Straighten your knees, draw your thighs back. If possible, put your heels down on the ground. Bring attention to your hands and make sure they are parallel to each other and parallel to the rest of your body. Spread your fingers. Your elbows should be straight. Breathe smoothly and stay in position for 30 seconds to 2 minutes.", "Sit down on the floor with your legs straight in front of you. Bend your knees and take your left leg under your right. Placing the left foot on the outside of your right hip and your right foot on the outside of your left leg. Place your right hand behind you and your left upper arm on the outside of your right knee. Your left palm should now be facing outwards along with your upper body. Stay in position for 30 seconds to 1 minute and repeat on the left side.", "Stand up and put your hands on your hips and spread your legs wide apart. Turn your right foot out. Align your heels with each other. Bend your right knee so it’s directly above your ankle. Extend your arms away from each other and keep them align. Move your head so you can gaze towards your right fingers. Stay in position from 30 seconds to 1 minute. Then reverse your feet and repeat for the same length of time on the left side."};
    string[] muscleGroup = { "Core", "Glutes", "Arms", "Back", "Legs"};
    string[] name = { "Boat Pose", "Bridge", "Downward Dog", "Half lord of the fishes", "Warrior II"};
    string[] programName = { "Wow", "Yes", "Sova" };

    private void Start()
    {
        addExerciseToArray("Boat Pose");
        addExerciseToArray("Bridge");
        addExerciseToArray("Downward Dog");
        addProgramsAndExercisesToList(tempProgram);
        addButtons();
    }

    private void addExerciseToArray(string name)
    {
        for(int i = 0; i < exerciseList.Count; i++)
        {
            Exercise temp = exerciseList[i];
            if (name.Equals(temp.getName()))
            {
                tempProgram[nextEmpty] = temp;
            }
        }
    }

    private void addProgramsAndExercisesToList(Exercise[] e)
    {
        string programName = "Program 1";
        programList.Add(new Program(programName, e));
        for (int i = 0; i < name.Length; i++)
        {
            exerciseList.Add(new Exercise(name[i], description[i], muscleGroup[i], intensity[i], difficulty[i]));
        }
    }

    private void addButtons()
    {
        for (int i = 0; i < name.Length; i++)
        {
            GameObject tempButton = exerciseButton;
            tempButton.GetComponent<ExerciseButton>().setText(name[i], description[i], muscleGroup[i], intensity[i], difficulty[i]);
            tempButton = Instantiate<GameObject>(exerciseButton, exerciseListObject);
        }
        for (int i = 0; i < programList.Count; i++)
        {
            Program p = programList[i];
            GameObject tempButton = programButton;
            tempButton.GetComponent<ProgramButton>().setText(p.getName(), 1, 1);
            tempButton = Instantiate<GameObject>(programButton, programListObject);
        }
    }

    private void eraseArray()
    {
        for(int i = 0; i < tempProgram.Length; i++)
        {
            tempProgram[i] = null;
        }
    }

    private void findNextEmpty()
    {
        bool found = false;
        for(int i = 0; i < tempProgram.Length; i++)
        {
            if (tempProgram.Equals(null) && found == false)
            {
                found = true;
                nextEmpty = i;
            }
        }
    }

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

    private Program getProgramByName(string name)
    {
        Program p = null;
        for(int i = 0; i < programList.Count; i++)
        {
            Program temp = programList[i];
            if (name.Equals(temp.getName()))
            {
                p = temp;
            }
        }
        return p;
    }

    //Tar in input från sökrutan in Main panelen
    public void textChanged(string newText)
    {
        Debug.Log("hello from ListHandler. ");
        Debug.Log("entered: " + newText);
    }
}
