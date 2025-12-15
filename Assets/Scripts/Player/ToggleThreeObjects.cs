using UnityEngine;

public class ToggleThreeObjects : MonoBehaviour
{
    [Header("Основні об'єкти")]
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;

    [Header("UI Об'єкти")]
    [SerializeField] private GameObject uiObject1;
    [SerializeField] private GameObject uiObject2;
    [SerializeField] private GameObject uiObject3;

    public enum AccessMode { None, Only1, Only2, Only3, All }
    private AccessMode currentAccess = AccessMode.None;

    private void Update()
    {
        if (currentAccess == AccessMode.None) return;

        if (Input.GetKeyDown(KeyCode.Alpha1) &&
            (currentAccess == AccessMode.All || currentAccess == AccessMode.Only1))
        {
            SetActiveOnly(object1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) &&
            (currentAccess == AccessMode.All || currentAccess == AccessMode.Only2))
        {
            SetActiveOnly(object2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) &&
            (currentAccess == AccessMode.All || currentAccess == AccessMode.Only3))
        {
            SetActiveOnly(object3);
        }
    }

    private void SetActiveOnly(GameObject target)
    {
        if (object1 != null) object1.SetActive(object1 == target);
        if (object2 != null) object2.SetActive(object2 == target);
        if (object3 != null) object3.SetActive(object3 == target);
    }

    public void SetAccess(AccessMode mode)
    {
        currentAccess = mode;
        UpdateUI();
    }

    public void ResetAccess()
    {
        currentAccess = AccessMode.None;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (uiObject1 != null) uiObject1.SetActive(currentAccess == AccessMode.Only1);
        if (uiObject2 != null) uiObject2.SetActive(currentAccess == AccessMode.Only2);
        if (uiObject3 != null) uiObject3.SetActive(currentAccess == AccessMode.Only3);

        if (currentAccess == AccessMode.All)
        {
            if (uiObject1 != null) uiObject1.SetActive(true);
            if (uiObject2 != null) uiObject2.SetActive(true);
            if (uiObject3 != null) uiObject3.SetActive(true);
        }

        if (currentAccess == AccessMode.None)
        {
            if (uiObject1 != null) uiObject1.SetActive(false);
            if (uiObject2 != null) uiObject2.SetActive(false);
            if (uiObject3 != null) uiObject3.SetActive(false);
        }
    }
}
