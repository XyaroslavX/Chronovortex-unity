using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeverCycleActivator : MonoBehaviour
{
    [SerializeField] private List<GameObject> cycleObjects;
    [SerializeField] private float interval = 1f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float rotationAngle = 45f;
    [SerializeField] private float rotationSpeed = 3f;

    private bool isPlayerNearby = false;
    private bool isActive = false;
    private Coroutine cycleRoutine;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation;
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            isActive = !isActive;

            if (isActive)
            {
                targetRotation = Quaternion.Euler(0, 0, rotationAngle);
                cycleRoutine = StartCoroutine(CycleObjects());
            }
            else
            {
                targetRotation = initialRotation;
                if (cycleRoutine != null)
                    StopCoroutine(cycleRoutine);

                foreach (var obj in cycleObjects)
                    if (obj != null)
                        obj.SetActive(false);
            }
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private IEnumerator CycleObjects()
    {
        int index = 0;
        while (isActive)
        {
            for (int i = 0; i < cycleObjects.Count; i++)
                if (cycleObjects[i] != null)
                    cycleObjects[i].SetActive(i == index);

            index = (index + 1) % cycleObjects.Count;
            yield return new WaitForSeconds(interval);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            isPlayerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            isPlayerNearby = false;
    }
}
