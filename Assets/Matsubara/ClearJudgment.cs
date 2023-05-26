using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearJudgment : MonoBehaviour
{
    GameObject[] _enemy;
    [SerializeField, Header("�j��Ώۂ̃^�O")] string _enemyTag;
    int _destroyEnemyCount = 0;
    SceneChanger _changer;
    [SerializeField, Header("�N���A�V�[���̖��O")] string _clearSceneName;
    void Start()
    {
        _enemy = GameObject.FindGameObjectsWithTag(_enemyTag);
        _changer = FindAnyObjectByType<SceneChanger>();
    }
    private void FixedUpdate()
    {
        foreach (GameObject enemy in _enemy)
        {
            if (enemy == null)
            {
                _destroyEnemyCount++;
            }
        }
        if (_destroyEnemyCount == _enemy.Length)
        {
            GameClear();
        }
    }
    private void GameClear()
    {
        _changer.SceneChange(_clearSceneName);
    }
}
