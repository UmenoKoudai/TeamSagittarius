using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float _hp = 100;
    [SerializeField, Header("���̃^�O")] string _groundTag;
    [SerializeField, Header("�e�̃^�O")] string _bulletTag;
    [SerializeField] float _damage = 0.1f;
    bool _isCollision = false;
    private void Update()
    {
        if (_isCollision)
        {
            _hp -= _damage;
            if (_hp < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_bulletTag))
        {
            Destroy(this.gameObject);
        }
        else if (!collision.gameObject.CompareTag(_groundTag))
        {
            _isCollision = true;
        }
    }
}