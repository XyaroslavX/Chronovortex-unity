using UnityEngine;

public class TriggerShrink : MonoBehaviour
{
    [SerializeField] private Transform objectToShrink; // об'єкт A
    [SerializeField] private float shrinkSpeed = 1f;
    [SerializeField] private float minScaleY = 0.3f;

    private bool isTriggered = false;
    private Vector3 initialScale;
    private Vector3 initialPosition;

    private void Awake()
    {
        if (objectToShrink != null)
        {
            initialScale = objectToShrink.localScale;
            initialPosition = objectToShrink.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Перевіряємо, що об'єкт, який увійшов, має Rigidbody2D і не є тригером B
        if (other.attachedRigidbody != null && !other.isTrigger)
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody != null && !other.isTrigger)
        {
            isTriggered = false;
        }
    }

    private void Update()
    {
        if (objectToShrink == null) return;

        Vector3 scale = objectToShrink.localScale;
        Vector3 pos = objectToShrink.position;

        if (isTriggered)
        {
            if (scale.y > minScaleY)
            {
                float delta = shrinkSpeed * Time.deltaTime;
                scale.y = Mathf.Max(minScaleY, scale.y - delta);
                pos.y += delta / 2f; // зміщення вгору для ефекту знизу
            }
        }
        else
        {
            scale.y = Mathf.MoveTowards(scale.y, initialScale.y, shrinkSpeed * Time.deltaTime);
            pos.y = Mathf.MoveTowards(pos.y, initialPosition.y, shrinkSpeed * 0.5f * Time.deltaTime);
        }

        objectToShrink.localScale = scale;
        objectToShrink.position = pos;
    }
}
