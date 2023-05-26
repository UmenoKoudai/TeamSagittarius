using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class Dotween : MonoBehaviour

{
    [SerializeField] GameObject Pachinko;
    [SerializeField] GameObject Buta;
    [SerializeField] Transform _muzzle;

    Coroutine Coroutine;

    void Start()
    {
        
    }

    void Update()
    {
        if (FindObjectOfType<Player>().State == Player.BulletState.Sky)
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
        GameObject pig = Instantiate(Buta, _muzzle.position, transform.rotation);
        pig.transform.DOJump(Pachinko.transform.position, 1.5f, 1, 0.5f).OnComplete(() => Destroy(pig));
        FindObjectOfType<Player>().Spown();
    }
}
