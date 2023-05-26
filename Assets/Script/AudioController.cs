using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : SingletonMonoBehaviour<AudioController>
{
    [SerializeField] AudioClip[] _seAudios;
    AudioSource _audio;
    //他にAudioControllerを付けたオブジェクトがあればこのオブジェクトを削除する無ければシングルトン化する
    private void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    //選んだAudioClipをPlayする関数
    public void SePlay(SelectAudio selectAudio)
    {
        //enumを整数にする
        int index = (int)selectAudio;
        _audio.PlayOneShot(_seAudios[index]);
    }
}

public enum SelectAudio
{
    Enemy,
    Start,
    GameOver,
    Explode,
    Draw,
    Shoot,
}