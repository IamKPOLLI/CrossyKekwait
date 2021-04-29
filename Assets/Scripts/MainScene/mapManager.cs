using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapManager : MonoBehaviour
{
    //object pools
    [SerializeField] private int _poolCount = 40;
    [SerializeField] private GreenBlock _greenPrefab;
    [SerializeField] private RoadBlock _roadPrefab;
    [SerializeField] private RiverBlock _riverPrefab;
    [SerializeField] private CarController _carPrefab;
    [SerializeField] private LogController _logPrefab;
    [SerializeField] private Tree _treePrefab;
    [SerializeField] private Coin _coinPrefab;

    private ObjectPool<GreenBlock> _greenPool;
    private ObjectPool<RoadBlock> _roadPool;
    private ObjectPool<RiverBlock> _riverPool;
    private ObjectPool<CarController> _carPool;
    private ObjectPool<Tree> _treePool;
    private ObjectPool<Coin> _coinPool;
    private ObjectPool<LogController> _logPool;


    private Vector3 _currentLastBlockPosition;
    private int _countOfStartBlocks = 30;

    [SerializeField] private GameObject _borders;







    private void Awake()
    {
        this._greenPool = new ObjectPool<GreenBlock>(this._greenPrefab, this._poolCount, this.transform);
        this._roadPool = new ObjectPool<RoadBlock>(this._roadPrefab, this._poolCount, this.transform);
        this._riverPool = new ObjectPool<RiverBlock>(this._riverPrefab, this._poolCount, this.transform);
        this._carPool = new ObjectPool<CarController>(this._carPrefab, 70, this.transform);
        this._logPool = new ObjectPool<LogController>(this._logPrefab, 70, this.transform);
        this._logPool.autoExpand = true;
        this._carPool.autoExpand = true;
        this._treePool = new ObjectPool<Tree>(this._treePrefab, 60, this.transform);
        this._coinPool = new ObjectPool<Coin>(this._coinPrefab, 10, this.transform);

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
        int value = Random.Range(0, 5);
        _currentLastBlockPosition += new Vector3(0, 0, 1.6f);
        switch (value)
        {
            case 0:
                var newT = this._greenPool.GetFreeElenemt();
                newT.transform.position = _currentLastBlockPosition;
                newT.GenerateTrees();
                MoveBorders();
                break;
            case 1:
                var newR = this._roadPool.GetFreeElenemt();
                newR.transform.position = _currentLastBlockPosition;
                newR.AddNewCar();
                MoveBorders();
                break;
            case 2:
                var newRi = this._riverPool.GetFreeElenemt();
                newRi.transform.position = _currentLastBlockPosition;
                newRi.AddNewLog();
                MoveBorders();
                break;
            case 3:
                var newT2 = this._greenPool.GetFreeElenemt();
                newT2.transform.position = _currentLastBlockPosition;
                newT2.GenerateTrees();
                MoveBorders();
                break;
            case 4:
                var newR2 = this._roadPool.GetFreeElenemt();
                newR2.transform.position = _currentLastBlockPosition;
                newR2.AddNewCar();
                MoveBorders();
                break;
        }
    }


    private void GenerateStartPartOfMap()
    {
        for(int i = 0; i < _countOfStartBlocks; i++)
        {
            int value = Random.Range(0, 5);
            if (i == 3) value = 0;
            switch (value)
            {
                case 0:
                    var newT = this._greenPool.GetFreeElenemt();
                    newT.transform.position = new Vector3(0, 0, i * 1.6f);
                    if(i != 3)
                    {
                        newT.GenerateTrees();
                    }                   
                    _currentLastBlockPosition = new Vector3(0, 0, i * 1.6f);
                    break;
                case 1:
                    var newR = this._roadPool.GetFreeElenemt();
                    newR.transform.position = new Vector3(0, 0, i * 1.6f);
                    newR.AddNewCar();
                    _currentLastBlockPosition = new Vector3(0, 0, i * 1.6f);
                    break;
                case 2:
                    var newRi = this._riverPool.GetFreeElenemt();
                    newRi.transform.position = new Vector3(0, 0, i * 1.6f);
                    newRi.AddNewLog();
                    _currentLastBlockPosition = new Vector3(0, 0, i * 1.6f);
                    break;
                case 3:
                    var newT2 = this._greenPool.GetFreeElenemt();
                    newT2.transform.position = new Vector3(0, 0, i * 1.6f);
                    if (i != 3)
                    {
                        newT2.GenerateTrees();
                    }
                    _currentLastBlockPosition = new Vector3(0, 0, i * 1.6f);
                    break;
                case 4:
                    var newR2 = this._roadPool.GetFreeElenemt();
                    newR2.transform.position = new Vector3(0, 0, i * 1.6f);
                    newR2.AddNewCar();
                    _currentLastBlockPosition = new Vector3(0, 0, i * 1.6f);
                    break;
            }
        }
    }

    private void MoveBorders()
    {
        _borders.transform.position += new Vector3(0, 0, 1.6f);
    } 

    public CarController GetNewCar()
    {
       return  this._carPool.GetFreeElenemt();
    }

    public LogController GetNewLog()
    {
        return this._logPool.GetFreeElenemt();
    }


    public Tree GetNewTree()
    {
        return this._treePool.GetFreeElenemt();
    }

    public Coin GetNewCoin()
    {
        return this._coinPool.GetFreeElenemt();
    }



}
