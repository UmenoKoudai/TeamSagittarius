using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject _effect;
    Player _playerScript;
    private void Start()
    {
        _playerScript = FindObjectOfType<Player>();
    }
    void Update()
    {
        if(_playerScript.State == Player.BulletState.Sky)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Instantiate(_effect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayArea")
        {
            Destroy(gameObject);
        }
    }
}
