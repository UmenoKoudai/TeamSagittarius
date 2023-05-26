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

    void Start()
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            GameObject bullet = Instantiate(_bulletImage);
            bullet.transform.SetParent(_storage.transform);
        }
        _bulletList = Enumerable.Repeat(_bullet, _bulletCount).ToList();
        _nowBullet = Instantiate(_bulletList[0], _muzzle.transform.position, transform.rotation);
        _bulletList.RemoveAt(0);
        Destroy(_storage.transform.GetChild(0).gameObject);
        _basePosition = _muzzle.transform.position;
    }

    void Update()
    {
        Debug.Log(_state);
        _leftLine.SetPosition(0, _left.position);
        _rightLine.SetPosition(0, _right.position);
        if (_state != BulletState.NoBullets)
        {
            if (_state == BulletState.Set)
            {
                if (Input.GetButton("Fire1"))
                {
                    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0;
                    _muzzle.transform.position = mousePosition;
                    _nowBullet.transform.position = _muzzle.transform.position;
                    _leftLine.SetPosition(1, _muzzle.position);
                    _rightLine.SetPosition(1, _muzzle.position);
                }
                else
                {
                    _leftLine.SetPosition(1, _basePosition);
                    _rightLine.SetPosition(1, _basePosition);
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    Vector3 dir = _basePosition - _muzzle.position;
                    float power = Vector3.Distance(_basePosition, _muzzle.position);
                    Rigidbody2D bulletRb = _nowBullet.GetComponent<Rigidbody2D>();
                    bulletRb.gravityScale = 1;
                    bulletRb.AddForce(dir * (power * 10), ForceMode2D.Impulse);
                    _state = BulletState.Sky;
                    if (_bulletList.Count == 0)
                    {
                        _state = BulletState.NoBullets;
                    }
                }

            }
        }
        if (_bulletList.Count != 0)
        {
            if (FindObjectsOfType<Bullet>().Length < 1)
            {
                _nowBullet = Instantiate(_bulletList[0], _basePosition, transform.rotation);
                _bulletList.RemoveAt(0);
                Destroy(_storage.transform.GetChild(0).gameObject);
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
