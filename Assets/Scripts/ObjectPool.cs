using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialSize = 500;

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = CreateNewObject();
            ReturnToPool(obj);
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        obj.SetActive(false);
        return obj;
    }

    public GameObject GetFromPool()
    {
        if (pool.Count == 0)
        {
            pool.Enqueue(CreateNewObject());
        }

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool.Enqueue(obj);
    }
}
