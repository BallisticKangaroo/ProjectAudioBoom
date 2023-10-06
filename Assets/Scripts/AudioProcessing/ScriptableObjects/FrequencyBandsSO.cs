using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrequencyBands", menuName = "AudioProcessing/FrequencyBands")]
public class FrequencyBandsSO : ScriptableObject
{
    public List<float> Bands = new();

    public AnimationCurve SamplingCurve = new();

    [SerializeField]
    private int _numberOfBands = 64;

    public int NumberOfBands
    {
        get => _numberOfBands;
        set
        {
            if (value <= 0)
            {
                return;
            }

            CreateFrequenciesFromSamplingCurve();
        }
    }

    public void CreateFrequenciesFromSamplingCurve()
    {
        Bands = new List<float>();
        float bandWidth = (float)1 / (NumberOfBands - 1);
        for (int i = 0; i < NumberOfBands; i++)
        {
            Bands.Add(SamplingCurve.Evaluate(i * bandWidth) * AudioSettings.outputSampleRate);
        }
    }

    //public void CreatePianoKeyFrequencies()
    //{
    //    // Alternatively, the function :
    //    // f(n) = 2 ^ ((n - 49) / 2) * 440 Hz
    //    // with n = nth key of the piano can produce any number of keys.
    //    // The standard ideal piano has 88 keys.
    //    // => Source: https://en.wikipedia.org/wiki/Piano_key_frequencies
    //    Bands = new List<float>
    //    {
    //        48000f, // end range for easier computations later
    //        7902.133f,
    //        7458.62f,
    //        7040f,
    //        6644.875f,
    //        6271.927f,
    //        5919.911f,
    //        5587.652f,
    //        5274.041f,
    //        4978.032f,
    //        4698.636f,
    //        4434.922f,
    //        4186.009f,
    //        3951.066f,
    //        3729.31f,
    //        3520f,
    //        3322.438f,
    //        3135.963f,
    //        2959.955f,
    //        2793.826f,
    //        2637.02f,
    //        2489.016f,
    //        2349.318f,
    //        2217.461f,
    //        2093.005f,
    //        1975.533f,
    //        1864.655f,
    //        1760f,
    //        1661.219f,
    //        1567.982f,
    //        1479.978f,
    //        1396.913f,
    //        1318.51f,
    //        1244.508f,
    //        1174.659f,
    //        1108.731f,
    //        1046.502f,
    //        987.7666f,
    //        932.3275f,
    //        880f,
    //        830.6094f,
    //        783.9909f,
    //        739.9888f,
    //        698.4565f,
    //        659.2551f,
    //        622.254f,
    //        587.3295f,
    //        554.3653f,
    //        523.2511f,
    //        493.8833f,
    //        466.1638f,
    //        440f,
    //        415.3047f,
    //        391.9954f,
    //        369.9944f,
    //        349.2282f,
    //        329.6276f,
    //        311.127f,
    //        293.6648f,
    //        277.1826f,
    //        261.6256f,
    //        246.9417f,
    //        233.0819f,
    //        220f,
    //        207.6523f,
    //        195.9977f,
    //        184.9972f,
    //        174.6141f,
    //        164.8138f,
    //        155.5635f,
    //        146.8324f,
    //        138.5913f,
    //        130.8128f,
    //        123.4708f,
    //        116.5409f,
    //        110f,
    //        103.8262f,
    //        97.99886f,
    //        92.49861f,
    //        87.30706f,
    //        82.40689f,
    //        77.78175f,
    //        73.41619f,
    //        69.29566f,
    //        65.40639f,
    //        61.73541f,
    //        58.27047f,
    //        55f,
    //        51.91309f,
    //        48.99943f,
    //        46.2493f,
    //        43.65353f,
    //        41.20344f,
    //        38.89087f,
    //        36.7081f,
    //        34.64783f,
    //        32.7032f,
    //        30.86771f,
    //        29.13524f,
    //        27.5f,
    //        25.95654f,
    //        24.49971f,
    //        23.12465f,
    //        21.82676f,
    //        20.60172f,
    //        19.44544f,
    //        18.35405f,
    //        17.32391f,
    //        16.3516f,
    //        0f // start range for easier computations later
    //    };

    //    Bands.Reverse();
    //    NumberOfBins = Bands.Count;
    //}
}
