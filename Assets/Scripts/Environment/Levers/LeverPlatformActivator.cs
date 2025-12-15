using System.Collections.Generic;
using UnityEngine;

public class LeverPlatformActivator : MonoBehaviour
{
    [SerializeField] private List<MovePlatform> controlledPlatforms;
    [SerializeField] private float rotationAngle = -45f;
    [SerializeField] private float rotationSpeed = 200f;

    private bool isActive = false;
    private bool isRotating = false;
    private bool playerInRange = false;

    private Quaternion startRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    private void Update()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                isActive ? targetRotation : startRotation,
                rotationSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(transform.rotation, isActive ? targetRotation : startRotation) < 0.1f)
                isRotating = false;
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
            ToggleLever();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    private void ToggleLever()
    {
        isActive = !isActive;
        isRotating = true;

        foreach (var platform in controlledPlatforms)
        {
            if (isActive)
                platform.Activate();
            else
                platform.Deactivate();
        }
    }
}
