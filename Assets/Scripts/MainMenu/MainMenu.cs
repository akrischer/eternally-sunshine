using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MenuInputManager))]
public class MainMenu : MonoBehaviour {

    private GameObject playerObject;
    private RectTransform start;
    private RectTransform quit;
    private RectTransform levelSelect;
    private MenuOption quitMenuOption;
    private MenuOption startMenuOption;
    private MenuOption levelSelectOption;
    private ContinuousWalking continuousWalking;
    private MenuInputManager menuInputManager;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        continuousWalking = playerObject.GetComponent<ContinuousWalking>();

        start = GameObject.FindGameObjectWithTag(Tags.MAIN_MENU_START).GetComponent<RectTransform>();
        startMenuOption = start.GetComponent<MenuOption>();

        quit = GameObject.FindGameObjectWithTag(Tags.MAIN_MENU_QUIT).GetComponent<RectTransform>();
        quitMenuOption = quit.GetComponent<MenuOption>();

        levelSelect = GameObject.FindGameObjectWithTag(Tags.MAIN_MENU_LEVEL_SELECT).GetComponent<RectTransform>();
        levelSelectOption = levelSelect.GetComponent<MenuOption>();
        menuInputManager = GetComponent<MenuInputManager>();
    }

    public void FocusStartButton()
    {
        start.SetAsLastSibling();
        continuousWalking.SetWalkDirection(ContinuousWalking.Direction.BACK);
        menuInputManager.SetCurrentOption(startMenuOption);
    }

    public void FocusQuitButton()
    {
        quit.SetAsLastSibling();
        continuousWalking.SetWalkDirection(ContinuousWalking.Direction.FORWARD);
        menuInputManager.SetCurrentOption(quitMenuOption);
    }

    public void FocusLevelSelectButton()
    {
        levelSelect.SetAsLastSibling();
        continuousWalking.SetWalkDirection(ContinuousWalking.Direction.LEFT);
        menuInputManager.SetCurrentOption(levelSelectOption);
    }

    public void DisableMenuInputManager()
    {
        menuInputManager.enabled = false;
    }

    public void EnableMenuInputManager()
    {
        menuInputManager.enabled = true;
    }
}
