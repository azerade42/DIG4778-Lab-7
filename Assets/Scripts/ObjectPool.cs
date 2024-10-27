using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    //public static ObjectPool<T> Instance { get; private set; }
    public Queue<T> Pool;
    public List<T> ActivePool;

    public void InitalizePool(T prefab, int poolCap)
    {
        Pool = new Queue<T>();
        ActivePool = new List<T>();

        for (int i = 0; i < poolCap; i++)
        {
            T obj = Instantiate(prefab);
            obj.gameObject.SetActive(false);
            Pool.Enqueue(obj);
        }
    }

    public T GetObject()
    {
        if(Pool.Count == 0)
        {
            return null;
        }

        T obj = Pool.Dequeue();
        ActivePool.Add(obj);
        obj.gameObject.SetActive(true);
             
        return obj;
    }

    public void ReturnObject(T obj)
    {
        Pool.Enqueue(obj);
        ActivePool.Remove(obj);
        obj.gameObject.SetActive(false);
    }

    public void ReturnAllObjects()
    {
        foreach (T obj in Pool)
        {
            Destroy(obj.gameObject);
        }
        foreach (T obj in ActivePool)
        {
            Destroy(obj.gameObject);
        }

        Pool.Clear();
        ActivePool.Clear();
    }
}
