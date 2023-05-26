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
    [SerializeField] GameObject[] _bulletArray;
    GameObject _nowBullet;
    Vector3 _basePosition;
    int _index;

    public int BulletCount { get => _bulletCount; }

    void Start()
    {
        for (int i = 0; i < _bulletCount; i++)
        {
            GameObject bullet = Instantiate(_bulletImage);
            bullet.transform.SetParent(_storage.transform);
        }
        _bulletArray = Enumerable.Repeat(_bullet, _bulletCount).ToArray();
        _nowBullet = Instantiate(_bulletArray[_index]);
        _basePosition = _muzzle.transform.position;
        _nowBullet.transform.position = _muzzle.position;
    }

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Debug.Log("ŒÄ‚ñ‚¾?");
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            _muzzle.transform.position = mousePosition;
            _nowBullet.transform.position = _muzzle.position;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("”­ŽË");
            Vector3 dir = _basePosition - _muzzle.position;
            float power = Vector3.Distance(_basePosition, _muzzle.position);
            Rigidbody2D bulletRb = _nowBullet.GetComponent<Rigidbody2D>();
            bulletRb.gravityScale = 1;
            bulletRb.AddForce(dir * (power * 10), ForceMode2D.Impulse);
        }
    }
}
