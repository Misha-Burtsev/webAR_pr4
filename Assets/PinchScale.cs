using System.Runtime.InteropServices;
using UnityEngine;

// Масштабирование объекта щипком: два пальца на экране,
// развели – объект больше, свели – меньше
public class PinchScale : MonoBehaviour
{
    public float minScale = 0.05f;
    public float maxScale = 1f;

    float lastDistance; // расстояние между пальцами в прошлом кадре (0 – щипка не было)

#if UNITY_WEBGL && !UNITY_EDITOR
    // Функция из Assets/Plugins/PinchDistance.jslib
    [DllImport("__Internal")]
    static extern float GetPinchDistance();
#else
    // В редакторе WebXR нет – щипка не бывает
    static float GetPinchDistance() { return 0; }
#endif

    void Update()
    {
        float distance = GetPinchDistance();

        // Нужны два пальца на экране
        if (distance == 0)
        {
            lastDistance = 0;
            return;
        }

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
