using UnityEngine;
using UnityEngine.UI;

public class PlayerItemInteractionProgres : MonoBehaviour
{
    [SerializeField]
    private PlayerItemPickup _itemPickupSystem;
    [SerializeField]
    private Slider _progressSlider;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        ToggleActive(false);
    }

    private void Update()
    {
        bool active = _itemPickupSystem.Interacting;
        if (!active)
        {
            ToggleActive(false);
            return;
        }

        UpdateSliderRotation(_camera.transform.forward);
        UpdateProgress(_itemPickupSystem.PickupTimerNormalized);
        ToggleActive(true);
    }

    private void ToggleActive(bool active) => _progressSlider.gameObject.SetActive(active);
    private void UpdateProgress(float progress) => _progressSlider.value = progress;
    private void UpdateSliderRotation(Vector3 forward) => _progressSlider.transform.forward = forward;
}
