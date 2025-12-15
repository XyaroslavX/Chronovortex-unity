using UnityEngine;
using System.Collections.Generic;

public class LeverActivator : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToActivate;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float rotationAngle = 45f;
    [SerializeField] private float rotationSpeed = 3f;

    private bool isPlayerNearby = false;
    public bool IsActivated { get; private set; }
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
            IsActivated = !IsActivated;
            foreach (var obj in objectsToActivate)
                if (obj != null)
                    obj.SetActive(IsActivated);

            targetRotation = IsActivated
                ? Quaternion.Euler(0, 0, rotationAngle)
                : initialRotation;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
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
