using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Button))]
public class MenuOption : MonoBehaviour {

    public Button button;
    // What actions to call when a specific input is given
    public List<MenuDirectionAndAction> adjacentActions;

    public bool IsInteractable
    {
        get
        {
            return button.IsInteractable();
        }
    }

    public bool TryInvoke()
    {
        if (IsInteractable)
        {
            button.onClick.Invoke();
            return true;
        } else
        {
            return false;
        }
    }

	public bool TryGoInMenuDirection(MenuInputManager.MenuDirection direction)
    {
        List<MenuDirectionAndAction> actionsInDirection = adjacentActions.Where(mdaa => mdaa.menuDirection == direction).ToList();

        if (actionsInDirection.Count > 0)
        {
            actionsInDirection.ForEach(action => action.Invoke());
            return true;
        } else
        {
            return false;
        }
    }

    [System.Serializable]
    public class MenuDirectionAndAction
    {
        public MenuInputManager.MenuDirection menuDirection;
        public UnityEvent action;

        public MenuDirectionAndAction(MenuInputManager.MenuDirection menuDirection, UnityEvent action)
        {
            this.menuDirection = menuDirection;
            this.action = action;
        }

        public void Invoke()
        {
            action.Invoke();
        }
    }
}
