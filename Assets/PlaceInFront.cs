using UnityEngine;

// Ставит объект в центр поля зрения (viewer-space) – на distance метров перед камерой.
// Срабатывает при старте и при входе в AR: в AR пакет WebXR переключает
// Camera.main на AR-камеру, и объект переставляется перед ней.
public class PlaceInFront : MonoBehaviour
{
    public float distance = 1f;

    Camera lastCamera;

    // LateUpdate – когда положение камеры в этом кадре уже обновлено
    void LateUpdate()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null || mainCamera == lastCamera) return;
        lastCamera = mainCamera;

        Transform viewer = mainCamera.transform;
        transform.position = viewer.position + viewer.forward * distance;
    }
}
