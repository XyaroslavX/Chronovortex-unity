using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private CollectCounter counter; // посилання на лічильник

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (counter != null)
            {
                counter.AddPoint();
            }
            Destroy(gameObject); // зникає після збору
        }
    }
}
