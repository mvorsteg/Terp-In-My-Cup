using UnityEngine;

public class ButtonReceiver : MonoBehaviour
{
    [System.Serializable]
    public class ButtonLink
    {
        public Button button;
        public bool invert = false;
    }

    public ButtonLink[] requiredButtons;
    public bool mainReceiver = false;

    protected int pressedButtons;
    protected bool activated = false;

    protected virtual void Awake()
    {
        foreach (ButtonLink b in requiredButtons)
        {
            b.button.receivers.Add((this, b.invert));
        }
    }

    protected virtual void Start()
    {
        
    }

    public void SetButton(bool val)
    {
        if (val)
        {
            pressedButtons = Mathf.Clamp(pressedButtons + 1, 0, requiredButtons.Length);
            if (pressedButtons == requiredButtons.Length && !activated)
            {
                Activate();
            }
        }
        else
        {
            pressedButtons = Mathf.Clamp(pressedButtons - 1, 0, requiredButtons.Length);
            if (activated)
            {
                Deactivate();
            }
        }
    }

    protected virtual void Activate()
    {
        activated = true;
        if (mainReceiver)
        {
            UI.PlayDing();
        }
    }

    protected virtual void Deactivate()
    {
        activated = false;
    }
}