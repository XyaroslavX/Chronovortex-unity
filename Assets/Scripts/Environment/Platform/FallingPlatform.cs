using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float respawnDelay = 3f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody2D rb;
    private bool isFalling;
    private bool isShaking;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling || isShaking) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallRoutine());
        }
    }

    private System.Collections.IEnumerator FallRoutine()
    {
        isShaking = true;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Mathf.Sin(Time.time * 50f) * 0.05f;
            transform.position += new Vector3(x, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;
        isShaking = false;
        yield return new WaitForSeconds(fallDelay);

        rb.bodyType = RigidbodyType2D.Dynamic;
        isFalling = true;

        yield return new WaitForSeconds(respawnDelay);
        ResetPlatform();
    }

    private void ResetPlatform()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = startPosition;
        transform.rotation = startRotation;

        isFalling = false;
    }
}
