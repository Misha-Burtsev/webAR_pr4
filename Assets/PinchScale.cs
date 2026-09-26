using UnityEngine;
using UnityEngine.InputSystem;

// Масштабирование объекта щипком: два пальца на экране,
// развели – объект больше, свели – меньше
public class PinchScale : MonoBehaviour
{
    public float minScale = 0.05f;
    public float maxScale = 1f;

    float lastDistance; // расстояние между пальцами в прошлом кадре (0 – щипка не было)

    void Update()
    {
        Touchscreen screen = Touchscreen.current;

        // Нужны ровно два пальца на экране
        if (screen == null || !screen.touches[0].isInProgress || !screen.touches[1].isInProgress)
        {
            lastDistance = 0;
            return;
        }

        Vector2 finger1 = screen.touches[0].position.ReadValue();
        Vector2 finger2 = screen.touches[1].position.ReadValue();
        float distance = Vector2.Distance(finger1, finger2);

        if (lastDistance > 0)
        {
            // Меняем масштаб во столько раз, во сколько изменилось расстояние между пальцами
            float scale = transform.localScale.x * distance / lastDistance;
            scale = Mathf.Clamp(scale, minScale, maxScale);
            transform.localScale = Vector3.one * scale;
        }

        lastDistance = distance;
    }
}
