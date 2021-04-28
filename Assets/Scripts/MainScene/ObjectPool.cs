using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T: MonoBehaviour
{
    [SerializeField] private T _prefab { get; }
    [SerializeField] private Transform _container { get; }
    public bool autoExpand { get; set; }
    private List<T> _pool;


    public ObjectPool(T prefab, int count)
    {
        this._prefab = prefab;
        this._container = null;
        this.CreatePool(count);
    }

    public ObjectPool(T prefab, int count, Transform container)
    {
        this._prefab = prefab;
        this._container = container;
        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this._pool = new List<T>();

        for(int i = 0;i<count;i++)
        {
            this.CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(this._prefab, this._container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this._pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach(var mono in _pool)
            {
                if(!mono.gameObject.activeInHierarchy)
                    {
                        element = mono;
                        mono.gameObject.SetActive(true);
                        return true;
                    }
            }
        element = null;
        return false;
    }

    public T GetFreeElenemt()
    {
        if(this.HasFreeElement(out var element))
        {
            return element;
        }
        if (this.autoExpand)
        {
            return this.CreateObject(true);
        }

        throw new Exception(typeof(T).ToString() + "Error");
    }
}
