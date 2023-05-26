using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearJudgment : MonoBehaviour
{
    GameObject[] _enemy;
    [SerializeField, Header("破壊対象のタグ")] string _enemyTag;
    int _destroyEnemyCount = 0;
    SceneChanger _changer;
    [SerializeField, Header("クリアシーンの名前")] string _clearSceneName;
    List<GameObject> _bulletList;
    [SerializeField] GameObject _gameOverPanel;
    void Start()
    {
        _enemy = GameObject.FindGameObjectsWithTag(_enemyTag);
        _changer = FindAnyObjectByType<SceneChanger>();
        _bulletList = FindAnyObjectByType<Player>().BulletList;
        _gameOverPanel.SetActive(false);
    }
    private void Update()
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
        if (_bulletList.Count < 0)
        {
            GameOver();
        }
    }
    private void GameClear()
    {
        _changer.SceneChange(_clearSceneName);
    }
    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
    }
}
