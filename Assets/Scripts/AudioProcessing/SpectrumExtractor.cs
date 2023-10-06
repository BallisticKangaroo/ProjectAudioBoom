using System.Linq;
using UnityEngine;

public class SpectrumExtractor : MonoBehaviour
{
    [SerializeField, Tooltip("The AudioSource for which the spectrum is extracted")]
    private AudioSource _audioSource;

    [SerializeField, Tooltip("RuntimeSet to save spectrum data to")]
    private FloatRuntimeSet _spectrumData;

    private float[] _spectrum = new float[8192];

    void Update()
    {
        _audioSource?.GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);

        _spectrumData.Items.Clear();

        _spectrumData.Items = _spectrum.ToList();

        if (_audioSource.time >= 128f && _audioSource.time < 129f)
        {
            float[] curSpectrum = new float[1024];
            _audioSource.GetSpectrumData(curSpectrum, 0, FFTWindow.BlackmanHarris);

            float targetFrequency = 234f;
            float hertzPerBin = (float)AudioSettings.outputSampleRate / 2f / 1024;
            int targetIndex = (int)(targetFrequency / hertzPerBin);

            string outString = "";
            for (int i = targetIndex - 3; i <= targetIndex + 3; i++)
            {
                outString += string.Format("| Bin {0} : {1}Hz : {2} |   ", i, i * hertzPerBin, curSpectrum[i]);
            }

            Debug.Log(outString);
        }
    }

    void OnDestroy()
    {
        _spectrumData.Items.Clear();
    }
}
