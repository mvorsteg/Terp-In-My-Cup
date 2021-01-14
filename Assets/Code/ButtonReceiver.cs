using UnityEngine;

public class ButtonReceiver : MonoBehaviour
{
    public Button[] requiredButtons;

    protected int pressedButtons;
    protected bool activated = false;

    protected virtual void Awake()
    {
        foreach (Button b in requiredButtons)
        {
            b.receiver = this;
        }
    }

    public void SetButton(bool val)
    {
        if (val)
        {
            pressedButtons++;
            if (pressedButtons == requiredButtons.Length && !activated)
            {
                Activate();
            }
        }
        else
        {
            pressedButtons--;
            if (activated)
            {
                Deactivate();
            }
        }
    }

    protected virtual void Activate()
    {
        UI.PlayDing();
        activated = true;
    }

    protected virtual void Deactivate()
    {
        activated = false;
    }
}