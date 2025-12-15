using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    [SerializeField] private Text counterText;
    [SerializeField] private int maxValue = 5;
    [SerializeField] private List<GameObject> objectsToActivate;
    [SerializeField] private GameObject finalObject;

    private static int counter = 0;

    private void Start()
    {
        if (finalObject != null)
            finalObject.SetActive(false);

        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        counter++;
        UpdateUI();

        foreach (var obj in objectsToActivate)
            obj.SetActive(true);

        if (counter >= maxValue && finalObject != null)
            finalObject.SetActive(true);

        Destroy(gameObject);
    }

    private void UpdateUI()
    {
        if (counterText != null)
            counterText.text = counter + "/" + maxValue;
    }
}
