using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private void Start()
    {
        if (FindObjectOfType<SceneChanger>() != null)
        {
            Destroy(GetComponent<SceneChanger>());
        }
    }
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
