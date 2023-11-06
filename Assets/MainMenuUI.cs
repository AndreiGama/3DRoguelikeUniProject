using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    UIDocument mainMenuUI;
    VisualElement root;
    VisualElement ve_Difficulty;
    VisualElement ve_MainMenuButtons;
    Button b_Play;
    Button b_Quit;
    Button b_Easy;
    Button b_Hard;

    private void Awake() {
        mainMenuUI = GetComponent<UIDocument>();
        root = mainMenuUI.rootVisualElement;
        ve_Difficulty = root.Q("VE_Difficulty");
        ve_MainMenuButtons = root.Q("VE_MainMenuButtons");
        b_Play = root.Q<Button>("B_Play");
        b_Quit = root.Q<Button>("B_Quit");
        b_Easy = root.Q<Button>("B_Easy");
        b_Hard = root.Q<Button>("B_Hard");
    }
    private void Start() {
        b_Play.clicked += () => ShowDifficultyScreen();
    }

    //https://github.com/mdotstrange/MdotsCustomPlaymakerActions/blob/master/KnockbackAction.cs
    void ShowDifficultyScreen() {
        ve_MainMenuButtons.style.display = DisplayStyle.None;
        ve_Difficulty.style.display = DisplayStyle.Flex;
    }
}
