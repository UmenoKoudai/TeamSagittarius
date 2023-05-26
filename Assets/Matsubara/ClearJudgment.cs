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
        _changer = FindAnyObjectByType<SceneChanger>();
        _bulletList = FindAnyObjectByType<Player>().BulletList;
        _gameOverPanel.SetActive(false);
    }
    private void Update()
    {
        if (FindObjectsOfType<EnemyController>().Length < 1)
        {
            GameClear();
        }
        if (FindObjectOfType<Player>().State == Player.BulletState.NoBullets && FindObjectsOfType<EnemyController>().Length >= 1)
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
