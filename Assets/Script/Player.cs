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
        //発射可能な弾数のイメージを登録(Panelの子オブジェクトにする)
        for (int i = 0; i < _bulletCount; i++)
        {
            GameObject bullet = Instantiate(_bulletImage);
            bullet.transform.SetParent(_storage.transform);
        }
        //発射可能な弾数のリスト
        _bulletList = Enumerable.Repeat(_bullet, _bulletCount).ToList();
        //最初の弾をリロード
        _nowBullet = Instantiate(_bulletList[0], _muzzle.transform.position, transform.rotation);
        //リロードしたら発射可能オブジェクトを1つ減らす
        _bulletList.RemoveAt(0);
        //Panelのイメージを1つ減らす
        Destroy(_storage.transform.GetChild(0).gameObject);
        //Muzzleを初期位置を保存
        _basePosition = _muzzle.transform.position;
        //パチンコのゴム部分を表現(固定位置)
        _leftLine.SetPosition(0, _left.position);
        _rightLine.SetPosition(0, _right.position);
    }

    void Update()
    {
        Debug.Log(_state);
        //残弾がある場合だけ実行される
        if (_state != BulletState.NoBullets)
        {
            //弾がセット出来たら発射可能な状態になる
            if (_state == BulletState.Set)
            {
                if(Input.GetButtonDown("Fire1"))
                {
                    AudioController.Instance.SePlay(SelectAudio.Draw);
                }
                //左クリックを押している間実行
                if (Input.GetButton("Fire1"))
                {
                    //マウスの位置を取得
                    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0;
                    //Muzzleの位置を変更(マウスの位置に)
                    _muzzle.transform.position = mousePosition;
                    //弾の位置をMuzzleの位置にする
                    _nowBullet.transform.position = _muzzle.transform.position;
                    //パチンコのゴムの中央部分を今のMuzzleの位置にする
                    _leftLine.SetPosition(1, _muzzle.position);
                    _rightLine.SetPosition(1, _muzzle.position);
                }
                else
                {
                    //左クリックを押してない間はゴムの中央部分をMuzzleの初期位置する
                    _leftLine.SetPosition(1, _basePosition);
                    _rightLine.SetPosition(1, _basePosition);
                }
                //左クリックを離したとき
                if (Input.GetButtonUp("Fire1"))
                {
                    AudioController.Instance.SePlay(SelectAudio.Shoot);
                    //今のMuzzleとMuzleの初期位置を参照して弾のベクトルを取得
                    Vector3 dir = _basePosition - _muzzle.position;
                    //今のMuzzleとMuzzleの初期位置を参照して引っ張り具合によって力を変える
                    float power = Vector3.Distance(_basePosition, _muzzle.position);
                    //今装填している弾のRigitBodyを取得
                    Rigidbody2D bulletRb = _nowBullet.GetComponent<Rigidbody2D>();
                    //弾は物理挙動をしたいので重力を設定
                    bulletRb.gravityScale = 1;
                    //上で取得した発射ベクトルと力を使用して弾を発射する
                    bulletRb.AddForce(dir * (power * 5), ForceMode2D.Impulse);
                    //弾の状態を空中に変更
                    _state = BulletState.Sky;
                    //残弾がなくなったら弾の状態を残弾なしに変更
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
        //残弾がある状態
        if (_bulletList.Count != 0)
        {
            //前に発射した弾がフィールド上から無くなったら
            if (FindObjectsOfType<Bullet>().Length < 1)
            {
                //新しい弾をリロードする
                _nowBullet = Instantiate(_bulletList[0], _basePosition, transform.rotation);
                _bulletList.RemoveAt(0);
                Destroy(_storage.transform.GetChild(0).gameObject);
                //弾の状態をセット完了に変更
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
