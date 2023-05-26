using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : SingletonMonoBehaviour<AudioController>
{
    [SerializeField] AudioClip[] _seAudios;
    AudioSource _audio;
    //����AudioController��t�����I�u�W�F�N�g������΂��̃I�u�W�F�N�g���폜���閳����΃V���O���g��������
    private void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    //�I��AudioClip��Play����֐�
    public void SePlay(SelectAudio selectAudio)
    {
        //enum�𐮐��ɂ���
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