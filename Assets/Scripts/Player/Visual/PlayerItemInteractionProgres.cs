using UnityEngine;
using UnityEngine.UI;

public class PlayerItemInteractionProgres : MonoBehaviour
{
    [SerializeField]
    private Slider _progressSlider;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        UpdateSliderRotation(_camera.transform.forward);
    }

    public void ToggleActive(bool active) => _progressSlider.gameObject.SetActive(active);

    public void UpdateProgress(float progress) => _progressSlider.value = progress;
    public void UpdateSliderRotation(Vector3 forward) => _progressSlider.transform.forward = forward;
}
