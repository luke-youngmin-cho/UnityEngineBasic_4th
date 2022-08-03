using UnityEngine;
using UnityEngine.Video;
using UnityEditor;
public class NotesMaker : MonoBehaviour
{
    private SongData _songData;
    private KeyCode[] _keyCodes =
    {
        KeyCode.S,
        KeyCode.D,
        KeyCode.F,
        KeyCode.Space,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
    };

    [SerializeField] private VideoPlayer _videoPlayer;
    public bool isRecording
    {
        get => _videoPlayer.isPlaying;
    }

    public void OnRecordButtonClick()
    {
        _songData = new SongData();
        _songData.videoName = _videoPlayer.clip.name;
        _videoPlayer.Play();
    }

    public void OnStopRecordButtonClick()
    {
        SaveSongData();
        _videoPlayer.Stop();
    }

    private void Update()
    {
        if (isRecording)
            Recording();
    }

    private void Recording()
    {
        foreach (KeyCode keyCode in _keyCodes)
        {
            if (Input.GetKeyDown(keyCode))
                CreateNoteData(keyCode);
        }
    }

    private void CreateNoteData(KeyCode keyCode)
    {
        float roundedTime = (float)System.Math.Round(_videoPlayer.time, 2);
        NoteData noteData = new NoteData(keyCode, roundedTime, 1.0f);
        Debug.Log($"노트 데이터 생성 : {keyCode}, {roundedTime}");
        _songData.notes.Add(noteData);
    }

    private void SaveSongData()
    {
        string dir = EditorUtility.SaveFilePanel("저장할 곳을 입력하세여", 
                                                 "",
                                                 _songData.videoName,
                                                 "json");
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(_songData));
    }
}
