using UnityEngine;

public class LeverController : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private Transform door;
    [SerializeField] private float shrinkSpeed = 1f;
    [SerializeField] private float minScaleY = 0.3f;

    [Header("Lever Settings")]
    [SerializeField] private Transform leverTop;
    [SerializeField] private float leverAngle = 30f;    // кут нахилу важеля
    [SerializeField] private float leverRotateSpeed = 5f;

    private Vector3 initialDoorScale;
    private Vector3 initialDoorPosition;

    private Quaternion initialLeverRot;
    private Quaternion targetLeverRot;

    private bool isPlayerNear = false;
    private bool doorShrunk = false;
    private bool leverLeft = false;

    private void Awake()
    {
        if (door != null)
        {
            initialDoorScale = door.localScale;
            initialDoorPosition = door.position;
        }

        if (leverTop != null)
        {
            initialLeverRot = leverTop.localRotation;
            targetLeverRot = initialLeverRot;
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
            ToggleLever();
        }

        UpdateDoor();
        UpdateLever();
    }

    private void ToggleDoor()
    {
        doorShrunk = !doorShrunk;
    }

    private void ToggleLever()
    {
        leverLeft = !leverLeft;

        if (leverLeft)
            targetLeverRot = Quaternion.Euler(0, 0, leverAngle) * initialLeverRot;
        else
            targetLeverRot = Quaternion.Euler(0, 0, -leverAngle) * initialLeverRot;
    }

    private void UpdateDoor()
    {
        if (door == null) return;

        Vector3 scale = door.localScale;
        Vector3 pos = door.position;

        if (doorShrunk)
        {
            if (scale.y > minScaleY)
            {
                float delta = shrinkSpeed * Time.deltaTime;
                scale.y = Mathf.Max(minScaleY, scale.y - delta);
                pos.y += delta / 2f;
            }
        }
        else
        {
            scale.y = Mathf.MoveTowards(scale.y, initialDoorScale.y, shrinkSpeed * Time.deltaTime);
            pos.y = Mathf.MoveTowards(pos.y, initialDoorPosition.y, shrinkSpeed * 0.5f * Time.deltaTime);
        }

        door.localScale = scale;
        door.position = pos;
    }

    private void UpdateLever()
    {
        if (leverTop == null) return;

        leverTop.localRotation = Quaternion.Lerp(leverTop.localRotation, targetLeverRot, Time.deltaTime * leverRotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = false;
    }
}
