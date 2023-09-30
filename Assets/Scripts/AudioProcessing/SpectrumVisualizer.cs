using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SpectrumVisualizer : MonoBehaviour
{
    [SerializeField, Tooltip("Prefab to display one frequency")]
    private GameObject _frequencyMeterPrefab;

    [SerializeField, Tooltip("RuntimeSet for spectrum data to read from")]
    private FloatRuntimeSet _spectrumData;

    [Tooltip("How many meters are displayed?")]
    [SerializeField, Range(2, 256, order = 1)]
    private int _meterCount = 16;

    [Tooltip("Smoothing of the meter movement. Higher value means less smoothing.")]
    [SerializeField, Range(0.01f, 1)]
    private float _smoothingFactor = 0.5f;

    private List<FrequencyMeter> _frequencyMeters = new();


    private float meterSpacing = 0.5f;

    void Start()
    {
        int halfNumberOfMeters = _meterCount / 2; // Number of meters on each side
        bool evenNumberOfMeters = _meterCount % 2 == 0;
        float leftShift = (evenNumberOfMeters ? halfNumberOfMeters - 0.5f : halfNumberOfMeters);
        float spacingShift = (evenNumberOfMeters ? meterSpacing * (halfNumberOfMeters - 1) + meterSpacing / 2.0f : meterSpacing * halfNumberOfMeters);
        float leftMostShift = leftShift + spacingShift;
        for (int i = 0; i < _meterCount; i++)
        {
            float positionShift = -leftMostShift + i + i * meterSpacing;
            var position = transform.position + new Vector3(positionShift, 0, 0);
            var meter = Instantiate(_frequencyMeterPrefab, position, Quaternion.identity, transform).GetComponent<FrequencyMeter>();
            _frequencyMeters.Add(meter);
        }
    }

    void Update()
    {
        for (int i = 0; i < _frequencyMeters.Count; i++)
        {
            _frequencyMeters[i].Frequency = Mathf.Lerp(_frequencyMeters[i].transform.localScale.y, Mathf.Clamp(_spectrumData.Items[i] * 1000, 0, 100), _smoothingFactor);
        }

        for (int i = 1; i < _spectrumData.Items.Count - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, _spectrumData.Items[i] + 10, 0), new Vector3(i, _spectrumData.Items[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(_spectrumData.Items[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(_spectrumData.Items[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), _spectrumData.Items[i - 1] - 10, 1), new Vector3(Mathf.Log(i), _spectrumData.Items[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(_spectrumData.Items[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(_spectrumData.Items[i]), 3), Color.blue);
        }
    }


}

//public class AudioMeasureCS : MonoBehaviour
//{
//    public float RmsValue;
//    public float DbValue;
//    public float PitchValue;

//    private const int QSamples = 1024;
//    private const float RefValue = 0.1f;
//    private const float Threshold = 0.02f;

//    float[] _samples;
//    private float[] _spectrum;
//    private float _fSample;

//    void Start()
//    {
//        _samples = new float[QSamples];
//        _spectrum = new float[QSamples];
//        _fSample = AudioSettings.outputSampleRate;
//    }

//    void Update()
//    {
//        AnalyzeSound();
//    }

//    void AnalyzeSound()
//    {
//        GetComponent<AudioSource>().GetOutputData(_samples, 0); // fill array with samples
//        int i;
//        float sum = 0;
//        for (i = 0; i < QSamples; i++)
//        {
//            sum += _samples  *_samples ; // sum squared samples*</em>
//        }
//        RmsValue = Mathf.Sqrt(sum / QSamples); // rms = square root of average
//        DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB
//        if (DbValue < -160) DbValue = -160; // clamp it to -160dB min
//        // get sound spectrum
//        GetComponent().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
//        float maxV = 0;
//        var maxN = 0;
//        for (i = 0; i < QSamples; i++)
//        { // find max
//            if (!(spectrum > maxV) || !(spectrum > Threshold))
//                continue;

//            maxV = spectrum *; *
//            maxN = i; // maxN is the index of max
//        }
//        float freqN = maxN; // pass the index to a float variable
//        if (maxN > 0 && maxN < QSamples - 1)
//        { // interpolate index using neighbours
//            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
//            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
//            freqN += 0.5f * (dR * dR - dL * dL);
//        }
//        PitchValue = freqN * (fSample / 2) / QSamples; // convert index to frequency
//    }
//}
