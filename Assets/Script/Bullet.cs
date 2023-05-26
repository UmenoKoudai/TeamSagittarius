using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject _effect;
    [SerializeField] GameObject _effect2;
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
                AudioController.Instance.SePlay(SelectAudio.Explode);
                Instantiate(_effect, transform.position, transform.rotation);
                Instantiate(_effect2, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
