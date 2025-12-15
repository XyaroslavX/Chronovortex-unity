using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    [SerializeField] private Text counterText; // посилання на UI Text
    [SerializeField] private int maxValue = 5; // значення, яке виставляється у редакторі

    private static int counter = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            counter++;

            if (counterText != null)
            {
                counterText.text = counter + "/" + maxValue;
            }

            Destroy(gameObject);
        }
    }
}
