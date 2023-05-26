using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dotween : MonoBehaviour

{
    [SerializeField] GameObject Pachinko;
    [SerializeField] GameObject Buta;

    Coroutine Coroutine;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.DOJump(Pachinko.transform.position, 1.5f, 1, 0.5f);
            StartCoroutine(PigSet());
        }

        IEnumerator PigSet()
        {
            yield return new WaitForSeconds(0.5f);
            GameObject Pig = Instantiate(Buta, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
