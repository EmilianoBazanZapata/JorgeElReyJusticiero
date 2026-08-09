using UnityEngine;

[DisallowMultipleComponent]
public sealed class GameSettingsController : MonoBehaviour
{
    private const string BrightnessKey = "Brillo";
    private const string VolumeKey = "VolumenAudio";
    private const string QualityKey = "NumeroCalidad";
    private const string ResolutionKey = "Resolution";

    public float LoadBrightness()
    {
        return PlayerPrefs.GetFloat(BrightnessKey, 0.5f);
    }

    public void SaveBrightness(float value)
    {
        PlayerPrefs.SetFloat(BrightnessKey, value);
    }

    public float LoadVolume()
    {
        return PlayerPrefs.GetFloat(VolumeKey, 5f);
    }

    public void SaveVolume(float value)
    {
        PlayerPrefs.SetFloat(VolumeKey, value);
    }

    public void ApplyVolume(float value)
    {
        AudioListener.volume = value;
    }

    public int LoadQuality()
    {
        return PlayerPrefs.GetInt(QualityKey, 3);
    }

    public void ApplyQuality(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel, true);
        PlayerPrefs.SetInt(QualityKey, qualityLevel);
    }

    public int LoadResolutionIndex()
    {
        return PlayerPrefs.GetInt(ResolutionKey, 0);
    }

    public void ApplyResolution(Resolution resolution, int resolutionIndex)
    {
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(ResolutionKey, resolutionIndex);
    }

    public void ApplyFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}
