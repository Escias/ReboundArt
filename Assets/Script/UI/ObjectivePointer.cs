using UnityEngine;

public class ObjectivePointer : MonoBehaviour
{
    public Transform objective;
    public RectTransform arrow;
    public Camera mainCamera;
    public float borderOffset = 20f;

    void Update()
    {
        if (objective == null || arrow == null || mainCamera == null)
        {
            Debug.LogError("Objective, Arrow, or Camera is not assigned!");
            return;
        }
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(objective.position);
        bool isOffScreen = screenPosition.z < 0 ||
                           screenPosition.x < 0 ||
                           screenPosition.x > Screen.width ||
                           screenPosition.y < 0 ||
                           screenPosition.y > Screen.height;
        if (isOffScreen)
        {
            if (!arrow.gameObject.activeSelf)
            {
                arrow.gameObject.SetActive(true);
            }
            if (screenPosition.z < 0)
            {
                screenPosition *= -1;
            }
            Vector3 clampedPosition = screenPosition;
            clampedPosition.x = Mathf.Clamp(screenPosition.x, borderOffset, Screen.width - borderOffset);
            clampedPosition.y = Mathf.Clamp(screenPosition.y, borderOffset, Screen.height - borderOffset);
            arrow.position = clampedPosition;
            Vector3 direction = (objective.position - mainCamera.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrow.rotation = Quaternion.Euler(0, 0, angle);
            arrow.Rotate(Vector3.forward, 0f);
        }
        else
        {
            if (arrow.gameObject.activeSelf)
            {
                arrow.gameObject.SetActive(false);
            }
        }
    }
}
