using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class AudioFileReader : MonoBehaviour
{
    [SerializeField]
    private string _pathToFile = "D:/Music/01-ADTR.mp3";

    private AudioSource _audioSource;
    private AudioClip _audioClip;

    void Awake()
    {
        if (!(_audioSource = GetComponent<AudioSource>()))
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }


    async void Start()
    {
        Debug.Log("LoadingClip...");

        _audioClip = await LoadClip(_pathToFile);

        Debug.Log("Clip loaded!");
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(5,5, 150, 50), "Play"))
        {
            _audioSource?.PlayOneShot(_audioClip);
        }

        if (GUI.Button(new Rect(5, 60, 150, 50), "Stop"))
        {
            _audioSource?.Stop();
        }
    }

    private async Task<AudioClip> LoadClip(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError($"Audio clip from {path} does not exist.".Length, this);

            return null;
        }

        AudioClip clip = null;
        Uri uri = new Uri(path);

        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(uri.AbsolutePath, AudioType.MPEG))
        {
            try
            {
                await uwr.SendWebRequest();
                while (!uwr.isDone)
                {
                    await Task.Delay(5);
                }    

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"{uwr.error}");
                }
                else
                {
                    clip = DownloadHandlerAudioClip.GetContent(uwr);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        return clip;
    }
}
