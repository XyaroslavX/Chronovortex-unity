using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] private PlayerTeleportSystem teleportSystem;
    [SerializeField] private PlayerTeleportSystem.AccessMode accessMode;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            teleportSystem.SetAccess(accessMode);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            teleportSystem.ResetAccess();
    }
}
