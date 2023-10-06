using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FrequencyBandsSO))]
public class FrequencyBandsSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var script = (FrequencyBandsSO)target;

        GUILayout.Space(EditorGUIUtility.singleLineHeight);

        if (GUILayout.Button("Create Frequencies", GUILayout.Height(40)))
        {
            script.CreateFrequenciesFromSamplingCurve();
        }
    }
}
