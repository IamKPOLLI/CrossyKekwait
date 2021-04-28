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
    [SerializeField] private Tree _treePrefab;
    [SerializeField] private Coin _coinPrefab;

    private ObjectPool<GreenBlock> _greenPool;
    private ObjectPool<RoadBlock> _roadPool;
    private ObjectPool<RiverBlock> _riverPool;
    private ObjectPool<CarController> _carPool;
    private ObjectPool<Tree> _treePool;
    private ObjectPool<Coin> _coinPool;


    private Vector3 _currentLastBlockPosition;
    private int _countOfStartBlocks = 20;

    [SerializeField] private GameObject _borders;







    private void Awake()
    {
        this._greenPool = new ObjectPool<GreenBlock>(this._greenPrefab, this._poolCount, this.transform);
        this._roadPool = new ObjectPool<RoadBlock>(this._roadPrefab, this._poolCount, this.transform);
        this._riverPool = new ObjectPool<RiverBlock>(this._riverPrefab, this._poolCount, this.transform);
        this._carPool = new ObjectPool<CarController>(this._carPrefab, 70, this.transform);
        //this._carPool.autoExpand = true;
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
        int value = Random.Range(0, 3);
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
                this._riverPool.GetFreeElenemt().transform.position = _currentLastBlockPosition;
                MoveBorders();
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
                    this._riverPool.GetFreeElenemt().transform.position = new Vector3(0, 0, i * 1.6f);
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


    public Tree GetNewTree()
    {
        return this._treePool.GetFreeElenemt();
    }

    public Coin GetNewCoin()
    {
        return this._coinPool.GetFreeElenemt();
    }



}
