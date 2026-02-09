using UnityEngine;

public class AdaptiveCamera : MonoBehaviour
{
    [SerializeField] private float _targetFOV = 40f;
    [SerializeField] private float _targetAspectRatio = 1.777f;

    private void Awake()
    {
        Camera cam = gameObject.GetComponent<Camera>();

        if (!cam) return;

        float aspectRatio = (float)Screen.height / Screen.width;
        cam.fieldOfView = (aspectRatio / _targetAspectRatio) * _targetFOV;
    }
}
