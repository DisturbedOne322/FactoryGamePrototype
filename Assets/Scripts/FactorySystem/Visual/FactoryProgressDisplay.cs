using UnityEngine;
using UnityEngine.UI;

public class FactoryProgressDisplay : MonoBehaviour
{
    [SerializeField]
    private Slider _progressSlider;
    [SerializeField]
    private Text _statusText;

    public void DisplayProgress(float progress)
    {
        _progressSlider.value = progress;
    }

    public void DisplayStatus(string status)
    {
        _statusText.text = status;
    }
}
