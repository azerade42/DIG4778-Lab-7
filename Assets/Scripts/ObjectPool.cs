using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    //public static ObjectPool<T> Instance { get; private set; }
    private Queue<T> poolList;

    public void InitalizePool(T prefab, int poolCap)
    {
        poolList = new Queue<T>();

        for (int i = 0; i < poolCap; i++)
        {
            T obj = Instantiate(prefab);
            obj.gameObject.SetActive(false);
            poolList.Enqueue(obj);
        }
    }

    public T GetObject()
    {
        if(poolList.Count == 0)
        {

            return null;
        }

        T obj = poolList.Dequeue();
        obj.gameObject.SetActive(true);
             
        return obj;
    }

    public void ReturnObject(T obj)
    {
        poolList.Enqueue(obj);
        obj.gameObject.SetActive(false);
        
    }
}
