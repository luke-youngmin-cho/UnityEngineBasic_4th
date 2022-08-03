using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class SongSelector : MonoBehaviour
{
#region ΩÃ±€≈Ê
    public static SongSelector instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }
#endregion

    public VideoClip clip { get; private set; }
    public SongData songData { get; private set; }
    public bool isDataLoaded { get; private set; }
    public void LoadSongData(string clipName)
    {
        bool isOK = true;
        try
        {
            clip = Resources.Load<VideoClip>($"VideoClips/{clipName}");
            TextAsset songDataText = Resources.Load<TextAsset>($"SongsData/{clipName}");
            songData = JsonUtility.FromJson<SongData>(songDataText.ToString());
        }
        catch
        {
            isOK = false;
        }

        isDataLoaded = isOK;
        if (isDataLoaded)
        {
            Debug.Log($"SongData Loaded : {clipName}");
        }
    }
}
