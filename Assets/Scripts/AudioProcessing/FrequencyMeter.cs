using UnityEngine;

public class FrequencyMeter : MonoBehaviour
{
    [field: SerializeField, Range(0, 100)]
    public float Frequency { get; set; } = 0.01f;

    [field: SerializeField, Range(0,100)]
    public float Intensity { get; set; } = 1.0f;

    void Update()
    {
        transform.localScale = new Vector3(1, Intensity * Frequency, 1);
    }
}
