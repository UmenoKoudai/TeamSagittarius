using DG.Tweening;
using UnityEngine;

public class Dotween : MonoBehaviour

{
    [SerializeField] GameObject Pachinko;
    [SerializeField] GameObject Buta;
    [SerializeField] Transform _muzzle;
    Player _playerScript;

    Coroutine Coroutine;

    private void Start()
    {
        _playerScript = FindObjectOfType<Player>();
    }
    void Update()
    {
        if (_playerScript.State == Player.BulletState.Sky)
        {
            if (FindObjectsOfType<Bullet>().Length < 1)
            {
                JumpMove();
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    transform.DOJump(Pachinko.transform.position, 1.5f, 1, 0.5f);
        //    StartCoroutine(PigSet());
        //}

        //IEnumerator PigSet()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    GameObject Pig = Instantiate(Buta, transform.position, Quaternion.identity);
        //    Destroy(this.gameObject);
        //}
    }

    public void JumpMove()
    {
        if (_playerScript.State != Player.BulletState.NoBullets)
        {
            GameObject pig = Instantiate(Buta, _muzzle.position, transform.rotation);
            pig.transform.DOJump(_playerScript.BasePosition, 1.5f, 1, 0.5f).OnComplete(() => Destroy(pig));
            FindObjectOfType<Player>().Spown();
        }
    }
}
