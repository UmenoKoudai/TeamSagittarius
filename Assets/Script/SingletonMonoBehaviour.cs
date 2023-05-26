using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    //継承先のクラスがアタッチされているオブジェクトを格納する
    public static T Instance
    {
        get
        {
            //_instanceになりも格納されてなければシーン上のTクラスを持っているオブジェクトを格納する
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                //それでも格納されてなければエラー文を返す
                if (_instance == null)
                {
                    Debug.Log("追加されているGameObjectが存在しません。");
                }
            }

            return _instance;
        }
    }

    //ゲーム開始時にTクラスを持ったオブジェクトをシングルトン化する
    virtual protected void Awake()
    {
        if (FindObjectsOfType<T>().Length > 1)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
}
