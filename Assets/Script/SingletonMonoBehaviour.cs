using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    //�p����̃N���X���A�^�b�`����Ă���I�u�W�F�N�g���i�[����
    public static T Instance
    {
        get
        {
            //_instance�ɂȂ���i�[����ĂȂ���΃V�[�����T�N���X�������Ă���I�u�W�F�N�g���i�[����
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                //����ł��i�[����ĂȂ���΃G���[����Ԃ�
                if (_instance == null)
                {
                    Debug.Log("�ǉ�����Ă���GameObject�����݂��܂���B");
                }
            }

            return _instance;
        }
    }

    //�Q�[���J�n����T�N���X���������I�u�W�F�N�g���V���O���g��������
    virtual protected void Awake()
    {
        if (FindObjectsOfType<T>().Length > 1)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
}
