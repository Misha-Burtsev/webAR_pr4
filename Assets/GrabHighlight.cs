using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

// Визуальный отклик: пока объект держат (XR Grab Interactable), он меняет цвет
public class GrabHighlight : MonoBehaviour
{
    public Color grabColor = Color.yellow;

    Renderer objectRenderer;
    Color normalColor;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        normalColor = objectRenderer.material.color;

        XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(args => objectRenderer.material.color = grabColor);
        grab.selectExited.AddListener(args => objectRenderer.material.color = normalColor);
    }
}
