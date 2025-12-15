using System.Collections.Generic;
using UnityEngine;

public class DisableTargetOnTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
                targets[i].SetActive(false);
        }
    }
}
