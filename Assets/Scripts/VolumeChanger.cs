using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private Slider _sliderBackgroundMusic;

    private void OnEnable()
    {
        _sliderBackgroundMusic.onValueChanged.AddListener
            (BackgroundSounds.InstanceBackgroundSounds.OnSliderBackgroundMusicValueChanged);
    }

    private void OnDisable()
    {
        _sliderBackgroundMusic.onValueChanged.RemoveListener
            (BackgroundSounds.InstanceBackgroundSounds.OnSliderBackgroundMusicValueChanged);
    }
}