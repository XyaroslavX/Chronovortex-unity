using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private bool loop = true;
    [SerializeField] private float stopTime = 0.5f;

    private int currentPointIndex = 0;
    private bool movingForward = true;
    private float waitTimer = 0f;
    private Vector3 lastPosition;

    private Transform playerOnPlatform; // гравець, який стоїть на платформі

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (points == null || points.Length == 0)
            return;

        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        Transform targetPoint = points[currentPointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // обчислюємо зсув платформи з минулого кадру
        Vector3 deltaMovement = transform.position - lastPosition;

        // якщо гравець стоїть на платформі — рухаємо його разом із нею
        if (playerOnPlatform != null)
        {
            playerOnPlatform.position += deltaMovement;
        }

        // оновлюємо позицію для наступного кадру
        lastPosition = transform.position;

        // перевірка досягнення точки
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            waitTimer = stopTime;

            if (loop)
            {
                currentPointIndex = (currentPointIndex + 1) % points.Length;
            }
            else
            {
                if (movingForward)
                {
                    if (currentPointIndex < points.Length - 1)
                        currentPointIndex++;
                    else
                        movingForward = false;
                }
                else
                {
                    if (currentPointIndex > 0)
                        currentPointIndex--;
                    else
                        movingForward = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (points == null || points.Length == 0)
            return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] != null)
                Gizmos.DrawSphere(points[i].position, 0.1f);

            if (i < points.Length - 1 && points[i] != null && points[i + 1] != null)
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }

        if (loop && points.Length > 1 && points[0] != null && points[points.Length - 1] != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(points[points.Length - 1].position, points[0].position);
        }
    }
}
