using System.Linq;
using UnityEngine;

public class SpectrumExtractor : MonoBehaviour
{
    [SerializeField, Tooltip("The AudioSource for which the spectrum is extracted")]
    private AudioSource _audioSource;

    [SerializeField, Tooltip("RuntimeSet to save spectrum data to")]
    private FloatRuntimeSet _spectrumData;

    private float[] _spectrum = new float[64];

    void Update()
    {
        _audioSource?.GetSpectrumData(_spectrum, 0, FFTWindow.Rectangular);

        _spectrumData.Items.Clear();

        _spectrumData.Items = _spectrum.ToList();
    }

    void OnDestroy()
    {
        _spectrumData.Items.Clear();
    }
}
