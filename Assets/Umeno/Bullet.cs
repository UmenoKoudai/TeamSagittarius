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
        if(_playerScript.State != Player.BulletState.Set)
        {
            GetComponent<ParticleSystem>().Play();
            if(Input.GetButtonDown("Fire2"))
            {
                Instantiate(_effect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<Dotween>().JumpMove();
        Destroy(gameObject);
        //if (collision.tag == "PlayArea")
        //{
        //    FindObjectOfType<Dotween>().JumpMove();
        //    Destroy(gameObject);
        //}
    }
}
