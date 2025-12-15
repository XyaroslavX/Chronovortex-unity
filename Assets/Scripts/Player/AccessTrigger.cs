using UnityEngine;

public class AccessTrigger : MonoBehaviour
{
    [SerializeField] private ToggleThreeObjects controller;
    [SerializeField] private ToggleThreeObjects.AccessMode mode;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controller.SetAccess(mode);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controller.ResetAccess();
        }
    }
}
