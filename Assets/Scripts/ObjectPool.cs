using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool inst;
    public GameObject objectToPool;
    public int poolAmt;
    public List<GameObject> pool;

    private void Start()
    {
        inst = this;

        for (int i = 0; i < poolAmt; i++)
        {
            var newObj = Instantiate(objectToPool, transform);
            pool.Add(newObj);
            newObj.SetActive(false);
        }
    }

    public GameObject GetObj()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf) return pool[i];
        }

        var newObj = Instantiate(objectToPool, transform);
        pool.Add(newObj);
        return newObj;

    }
}
