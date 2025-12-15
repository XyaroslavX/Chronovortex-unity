using UnityEngine;
using UnityEngine.UI;

public class CollectCounter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text counterText; // Текст UI, куди буде виводитися лічильник

    [Header("Налаштування лічильника")]
    [SerializeField] private int maxValue = 3; // Максимальна кількість предметів
    private int currentValue = 0;              // Поточний лічильник

    [Header("Двері для відкриття")]
    [SerializeField] private Transform door;   // Посилання на двері
    [SerializeField] private float openSpeed = 1f; // Швидкість відкриття
    [SerializeField] private float minDoorScaleY = 0.3f; // Мінімальний scale дверей при відкритті

    private bool doorOpening = false;
    private Vector3 initialDoorScale;
    private Vector3 initialDoorPosition;

    private void Start()
    {
        if (door != null)
        {
            initialDoorScale = door.localScale;
            initialDoorPosition = door.position;
        }

        UpdateCounterText();
    }

    public void AddPoint()
    {
        if (currentValue < maxValue)
        {
            currentValue++;
            UpdateCounterText();
        }

        if (currentValue >= maxValue)
        {
            doorOpening = true;
        }
    }

    private void UpdateCounterText()
    {
        if (counterText != null)
        {
            counterText.text = currentValue + " / " + maxValue;
        }
    }

    private void Update()
    {
        if (doorOpening && door != null)
        {
            Vector3 scale = door.localScale;
            Vector3 pos = door.position;

            if (scale.y > minDoorScaleY)
            {
                float delta = openSpeed * Time.deltaTime;
                scale.y = Mathf.Max(minDoorScaleY, scale.y - delta);
                pos.y += delta / 2f; // двері "відчиняються знизу вгору"
            }

            door.localScale = scale;
            door.position = pos;
        }
    }
}
