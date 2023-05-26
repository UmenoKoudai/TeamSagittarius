using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _bulletImage;
    [SerializeField] Transform _muzzle;
    [SerializeField] int _bulletCount;
    [SerializeField] GameObject _storage;
    [SerializeField] List<GameObject> _bulletList;
    [SerializeField] LineRenderer _leftLine;
    [SerializeField] LineRenderer _rightLine;
    [SerializeField] Transform _left;
    [SerializeField] Transform _right;
    BulletState _state = BulletState.Set;
    GameObject _nowBullet;
    Vector3 _basePosition;

    public List<GameObject> BulletList { get => _bulletList; }
    public BulletState State { get => _state; set => _state = value; }
    public Vector3 BasePosition { get => _basePosition; }

    void Start()
    {
        //���ˉ\�Ȓe���̃C���[�W��o�^(Panel�̎q�I�u�W�F�N�g�ɂ���)
        for (int i = 0; i < _bulletCount; i++)
        {
            GameObject bullet = Instantiate(_bulletImage);
            bullet.transform.SetParent(_storage.transform);
        }
        //���ˉ\�Ȓe���̃��X�g
        _bulletList = Enumerable.Repeat(_bullet, _bulletCount).ToList();
        //�ŏ��̒e�������[�h
        _nowBullet = Instantiate(_bulletList[0], _muzzle.transform.position, transform.rotation);
        //�����[�h�����甭�ˉ\�I�u�W�F�N�g��1���炷
        _bulletList.RemoveAt(0);
        //Panel�̃C���[�W��1���炷
        Destroy(_storage.transform.GetChild(0).gameObject);
        //Muzzle�������ʒu��ۑ�
        _basePosition = _muzzle.transform.position;
        //�p�`���R�̃S��������\��(�Œ�ʒu)
        _leftLine.SetPosition(0, _left.position);
        _rightLine.SetPosition(0, _right.position);
    }

    void Update()
    {
        Debug.Log(_state);
        //�c�e������ꍇ�������s�����
        if (_state != BulletState.NoBullets)
        {
            //�e���Z�b�g�o�����甭�ˉ\�ȏ�ԂɂȂ�
            if (_state == BulletState.Set)
            {
                if(Input.GetButtonDown("Fire1"))
                {
                    AudioController.Instance.SePlay(SelectAudio.Draw);
                }
                //���N���b�N�������Ă���Ԏ��s
                if (Input.GetButton("Fire1"))
                {
                    //�}�E�X�̈ʒu���擾
                    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0;
                    //Muzzle�̈ʒu��ύX(�}�E�X�̈ʒu��)
                    _muzzle.transform.position = mousePosition;
                    //�e�̈ʒu��Muzzle�̈ʒu�ɂ���
                    _nowBullet.transform.position = _muzzle.transform.position;
                    //�p�`���R�̃S���̒�������������Muzzle�̈ʒu�ɂ���
                    _leftLine.SetPosition(1, _muzzle.position);
                    _rightLine.SetPosition(1, _muzzle.position);
                }
                else
                {
                    //���N���b�N�������ĂȂ��Ԃ̓S���̒���������Muzzle�̏����ʒu����
                    _leftLine.SetPosition(1, _basePosition);
                    _rightLine.SetPosition(1, _basePosition);
                }
                //���N���b�N�𗣂����Ƃ�
                if (Input.GetButtonUp("Fire1"))
                {
                    AudioController.Instance.SePlay(SelectAudio.Shoot);
                    //����Muzzle��Muzle�̏����ʒu���Q�Ƃ��Ēe�̃x�N�g�����擾
                    Vector3 dir = _basePosition - _muzzle.position;
                    //����Muzzle��Muzzle�̏����ʒu���Q�Ƃ��Ĉ��������ɂ���ė͂�ς���
                    float power = Vector3.Distance(_basePosition, _muzzle.position);
                    //�����U���Ă���e��RigitBody���擾
                    Rigidbody2D bulletRb = _nowBullet.GetComponent<Rigidbody2D>();
                    //�e�͕����������������̂ŏd�͂�ݒ�
                    bulletRb.gravityScale = 1;
                    //��Ŏ擾�������˃x�N�g���Ɨ͂��g�p���Ēe�𔭎˂���
                    bulletRb.AddForce(dir * (power * 5), ForceMode2D.Impulse);
                    //�e�̏�Ԃ��󒆂ɕύX
                    _state = BulletState.Sky;
                    //�c�e���Ȃ��Ȃ�����e�̏�Ԃ��c�e�Ȃ��ɕύX
                    if (_bulletList.Count == 0)
                    {
                        _state = BulletState.NoBullets;
                    }
                }

            }
        }
    }

    public void Spown()
    {
        //�c�e��������
        if (_bulletList.Count != 0)
        {
            //�O�ɔ��˂����e���t�B�[���h�ォ�疳���Ȃ�����
            if (FindObjectsOfType<Bullet>().Length < 1)
            {
                //�V�����e�������[�h����
                _nowBullet = Instantiate(_bulletList[0], _basePosition, transform.rotation);
                _bulletList.RemoveAt(0);
                Destroy(_storage.transform.GetChild(0).gameObject);
                //�e�̏�Ԃ��Z�b�g�����ɕύX
                _state = BulletState.Set;
            }
        }
    }
    public enum BulletState
    {
        NoBullets,
        Set,
        Sky,
    }
}
