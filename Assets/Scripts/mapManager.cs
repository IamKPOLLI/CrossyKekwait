using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour
{
    [SerializeField] private int _poolCount = 40;
    [SerializeField] private bool _autoExpand = false;
    [SerializeField] private GreenBlock _greenPrefab;
    [SerializeField] private RoadBlock _roadPrefab;
    [SerializeField] private RiverBlock _riverPrefab;
    private Vector3 _currentLastBlockPosition;
    private int _countOfStartBlocks = 20;



    private ObjectPool<GreenBlock> _greenPool;
    private ObjectPool<RoadBlock> _roadPool;
    private ObjectPool<RiverBlock> _riverPool;




    private void Awake()
    {
        this._greenPool = new ObjectPool<GreenBlock>(this._greenPrefab, this._poolCount, this.transform);
        this._roadPool = new ObjectPool<RoadBlock>(this._roadPrefab, this._poolCount, this.transform);
        this._riverPool = new ObjectPool<RiverBlock>(this._riverPrefab, this._poolCount, this.transform);

    }
    void Start()
    {
        GenerateStartPartOfMap();
    }

    void Update()
    {

    }

    public void CreateNewBlock()
    {
        int value = Random.Range(0, 3);
        _currentLastBlockPosition += new Vector3(0, 0, 1.6f);
        switch (value)
        {
            case 0:
                this._greenPool.GetFreeElenemt().transform.position = _currentLastBlockPosition;
                break;
            case 1:
                this._roadPool.GetFreeElenemt().transform.position = _currentLastBlockPosition;
                break;
            case 2:
                this._riverPool.GetFreeElenemt().transform.position = _currentLastBlockPosition;
                break;
        }
    }


    private void GenerateStartPartOfMap()
    {
        for(int i = 0; i < _countOfStartBlocks; i++)
        {
            int value = Random.Range(0, 3);
            switch (value)
            {
                case 0:
                    this._greenPool.GetFreeElenemt().transform.position = new Vector3(0, 0, i * 1.6f);
                    _currentLastBlockPosition = new Vector3(0, 0, i * 1.6f);
                    break;
                case 1:
                    this._roadPool.GetFreeElenemt().transform.position = new Vector3(0, 0, i * 1.6f);
                    _currentLastBlockPosition = new Vector3(0, 0, i * 1.6f);
                    break;
                case 2:
                    this._riverPool.GetFreeElenemt().transform.position = new Vector3(0, 0, i * 1.6f);
                    _currentLastBlockPosition = new Vector3(0, 0, i * 1.6f);
                    break;
            }
        }
    }

    

}
