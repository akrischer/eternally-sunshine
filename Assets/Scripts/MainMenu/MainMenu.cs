using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MenuInputManager))]
public class MainMenu : MonoBehaviour {

    private GameObject playerObject;
    private RectTransform start;
    private RectTransform quit;
    private MenuOption quitMenuOption;
    private MenuOption startMenuOption;
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
}
