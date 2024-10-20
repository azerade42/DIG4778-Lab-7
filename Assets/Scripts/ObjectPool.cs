using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    public static ObjectPool<T> Instance { get; private set; }

    [SerializeField] private T poolPrefab;
    [SerializeField] private int poolMax;

    private List<T> poolList;

    private void Awake()
    {
        Instance = this;
        InitalizePool();
    }

    private void InitalizePool()
    {
        poolList = new List<T>();

        for (int i = 0; i < poolMax; i++)
        {
            T obj = Instantiate(poolPrefab);
            obj.gameObject.SetActive(false);
            poolList.Add(obj);
        }
    }

    public T GetObject()
    {
        foreach(var obj in poolList)
        {
            if(!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }
}
