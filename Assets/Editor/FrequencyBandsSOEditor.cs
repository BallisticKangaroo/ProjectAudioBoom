using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FrequencyBandsSO))]
public class FrequencyBandsSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (FrequencyBandsSO)target;

        if (GUILayout.Button("Create Piano Key Frequencies", GUILayout.Height(40)))
        {
            script.CreatePianoKeyFrequencies();
        }
    }
}
