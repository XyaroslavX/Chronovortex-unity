using UnityEngine;
using System.Collections.Generic;

public class LeverDoorController : MonoBehaviour
{
    [SerializeField] private LeverActivator firstLever;
    [SerializeField] private List<GameObject> requiredObjects;
    [SerializeField] private Transform door;
    [SerializeField] private float shrinkSpeed = 1f;
    [SerializeField] private float minScaleY = 0.3f;
    [SerializeField] private float rotationAngle = 45f;
    [SerializeField] private float rotationSpeed = 6f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private string playerTag = "Player";

    private Vector3 initialDoorScale;
    private Vector3 initialDoorPosition;
    private bool doorOpenRequested = false;
    private bool isPlayerNearby = false;
    private bool isToggled = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private void Awake()
    {
        if (door != null)
        {
            initialDoorScale = door.localScale;
            initialDoorPosition = door.position;
        }
        initialRotation = transform.localRotation;
        targetRotation = initialRotation;
    }

    private void Update()
    {
        bool prerequisitesMet = firstLever != null && firstLever.IsActivated && AllRequiredActive();

        if (isPlayerNearby && Input.GetKeyDown(interactKey) && prerequisitesMet)
        {
            isToggled = !isToggled;
            doorOpenRequested = isToggled;
            targetRotation = isToggled ? Quaternion.Euler(0, 0, rotationAngle) * initialRotation : initialRotation;
        }

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
        AnimateDoor();
    }

    private bool AllRequiredActive()
    {
        if (requiredObjects == null || requiredObjects.Count == 0) return true;
        foreach (var obj in requiredObjects)
            if (obj == null || !obj.activeSelf) return false;
        return true;
    }

    private void AnimateDoor()
    {
        if (door == null) return;

        Vector3 scale = door.localScale;
        Vector3 pos = door.position;

        if (doorOpenRequested)
        {
            if (scale.y > minScaleY)
            {
                float delta = shrinkSpeed * Time.deltaTime;
                scale.y = Mathf.Max(minScaleY, scale.y - delta);
                pos.y += delta / 2f;
                door.localScale = scale;
                door.position = pos;
            }
        }
        else
        {
            scale.y = Mathf.MoveTowards(scale.y, initialDoorScale.y, shrinkSpeed * Time.deltaTime);
            pos.y = Mathf.MoveTowards(pos.y, initialDoorPosition.y, shrinkSpeed * 0.5f * Time.deltaTime);
            door.localScale = scale;
            door.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag)) isPlayerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag)) isPlayerNearby = false;
    }
}
