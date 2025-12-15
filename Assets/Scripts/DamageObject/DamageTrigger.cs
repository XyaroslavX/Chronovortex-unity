using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            PlayerController health = other.GetComponent<PlayerController>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }
}
