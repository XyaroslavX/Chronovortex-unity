using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerTeleportSystem : MonoBehaviour
{
    [System.Serializable]
    public class TeleportPoint
    {
        public Transform point;
        [HideInInspector] public int count;
    }

    [Header("Основні об'єкти")]
    [SerializeField] private Transform player;
    [SerializeField] private List<TeleportPoint> teleportPoints = new();
    [SerializeField] private float teleportYOffset = 1f;

    [Header("UI елементи")]
    [SerializeField] private GameObject uiObject1;
    [SerializeField] private GameObject uiObject2;
    [SerializeField] private GameObject uiObject3;

    public enum AccessMode { None, Only1, Only2, Only3, All }
    private AccessMode currentAccess = AccessMode.None;

    private void Start()
    {
        AssignCounts();
        UpdateUI(AccessMode.None);
    }

    private void Update()
    {
        if (currentAccess == AccessMode.None || player == null)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1) &&
            (currentAccess == AccessMode.All || currentAccess == AccessMode.Only1))
        {
            TeleportToLinkedPoint();
            UpdateUI(AccessMode.Only1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) &&
            (currentAccess == AccessMode.All || currentAccess == AccessMode.Only2))
        {
            TeleportToLinkedPoint();
            UpdateUI(AccessMode.Only2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) &&
            (currentAccess == AccessMode.All || currentAccess == AccessMode.Only3))
        {
            TeleportToLinkedPoint();
            UpdateUI(AccessMode.Only3);
        }
    }

    private void AssignCounts()
    {
        for (int i = 0; i < teleportPoints.Count; i++)
        {
            teleportPoints[i].count = i;
        }
    }

    private void TeleportToLinkedPoint()
    {
        if (teleportPoints.Count < 2) return;

        TeleportPoint nearest = GetNearestPoint();
        if (nearest == null) return;

        int targetIndex = (nearest.count % 2 == 0) ? nearest.count + 1 : nearest.count - 1;
        if (targetIndex < 0 || targetIndex >= teleportPoints.Count) return;

        TeleportPoint target = teleportPoints[targetIndex];
        if (target != null && target.point != null)
        {
            player.position = target.point.position + Vector3.up * teleportYOffset;
        }
    }

    private TeleportPoint GetNearestPoint()
    {
        TeleportPoint nearest = null;
        float minDist = float.MaxValue;

        foreach (var tp in teleportPoints)
        {
            if (tp.point == null) continue;
            float dist = Vector3.Distance(player.position, tp.point.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = tp;
            }
        }

        return nearest;
    }

    public void SetAccess(AccessMode mode)
    {
        currentAccess = mode;
        UpdateUI(mode);
    }

    public void ResetAccess()
    {
        currentAccess = AccessMode.None;
        UpdateUI(AccessMode.None);
    }

    private void UpdateUI(AccessMode mode)
    {
        if (uiObject1 != null) uiObject1.SetActive(mode == AccessMode.Only1);
        if (uiObject2 != null) uiObject2.SetActive(mode == AccessMode.Only2);
        if (uiObject3 != null) uiObject3.SetActive(mode == AccessMode.Only3);

        if (mode == AccessMode.All)
        {
            if (uiObject1 != null) uiObject1.SetActive(true);
            if (uiObject2 != null) uiObject2.SetActive(true);
            if (uiObject3 != null) uiObject3.SetActive(true);
        }

        if (mode == AccessMode.None)
        {
            if (uiObject1 != null) uiObject1.SetActive(false);
            if (uiObject2 != null) uiObject2.SetActive(false);
            if (uiObject3 != null) uiObject3.SetActive(false);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (teleportPoints == null || teleportPoints.Count == 0)
            return;

        for (int i = 0; i < teleportPoints.Count; i += 2)
        {
            if (i + 1 >= teleportPoints.Count) break;

            var a = teleportPoints[i].point;
            var b = teleportPoints[i + 1].point;

            if (a != null && b != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(a.position, b.position);
                Gizmos.DrawSphere(a.position, 0.15f);
                Gizmos.DrawSphere(b.position, 0.15f);

                Handles.Label((a.position + b.position) / 2 + Vector3.up * 0.3f, $"Pair {i / 2 + 1}");
            }
        }
    }
#endif
}
