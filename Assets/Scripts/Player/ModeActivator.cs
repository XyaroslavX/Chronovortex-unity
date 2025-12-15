using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IModeSwitcher
{
    void SwitchToMode(int modeIndex);
}

public class ModeActivator : MonoBehaviour, IModeSwitcher
{
    [SerializeField] private List<GameObject> modeOneObjects;
    [SerializeField] private List<GameObject> modeTwoObjects;
    [SerializeField] private Image modeOneUI;
    [SerializeField] private Image modeTwoUI;
    [SerializeField] private float activeAlpha = 1f;
    [SerializeField] private float inactiveAlpha = 0.2f;

    private readonly Dictionary<int, IModeHandler> modeHandlers = new();

    private void Awake()
    {
        modeHandlers[1] = new ModeHandler(modeOneObjects, modeOneUI, activeAlpha, inactiveAlpha);
        modeHandlers[2] = new ModeHandler(modeTwoObjects, modeTwoUI, activeAlpha, inactiveAlpha);
        SwitchToMode(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchToMode(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchToMode(2);
    }

    public void SwitchToMode(int modeIndex)
    {
        foreach (var handler in modeHandlers.Values) handler.Deactivate();
        modeHandlers[modeIndex].Activate();
    }
}

public interface IModeHandler
{
    void Activate();
    void Deactivate();
}

public class ModeHandler : IModeHandler
{
    private readonly List<GameObject> objects;
    private readonly Image uiElement;
    private readonly float activeA;
    private readonly float inactiveA;

    public ModeHandler(List<GameObject> objects, Image uiElement, float activeA, float inactiveA)
    {
        this.objects = objects;
        this.uiElement = uiElement;
        this.activeA = activeA;
        this.inactiveA = inactiveA;
    }

    public void Activate()
    {
        foreach (var obj in objects) obj.SetActive(true);
        var c = uiElement.color;
        uiElement.color = new Color(c.r, c.g, c.b, activeA);
    }

    public void Deactivate()
    {
        foreach (var obj in objects) obj.SetActive(false);
        var c = uiElement.color;
        uiElement.color = new Color(c.r, c.g, c.b, inactiveA);
    }
}
