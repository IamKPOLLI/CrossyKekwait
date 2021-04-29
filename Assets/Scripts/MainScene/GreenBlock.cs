using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlock : MonoBehaviour
{
    //скрипт для зеленого блока


    private float[] _xPositionsForTreedAndCoins;
    private mapManager _mapManager;
    private List<float> _usedPos;
    private int _checkCicle;

    private int _minTrees = 4;
    private int _maxTrees = 1;


    private void Awake()
    {
        _mapManager = FindObjectOfType<mapManager>();
        _xPositionsForTreedAndCoins = new float[10] {-4.3f, -2.7f, -1.1f, 0.5f, 2.1f, 3.7f, 5.3f, 6.9f, 8.5f, 10.1f};
        _usedPos = new List<float>();
    }



    public void GenerateTrees()
    {
        var countOfTrees = Random.Range(_minTrees, _maxTrees+1);
        ClearList();

        for(int i = 0; i < countOfTrees; i++)
        {
            bool flag = true;
            while (_checkCicle<10 && flag)
            {
                var pos = Random.Range(0, 10);
                if (!_usedPos.Contains(_xPositionsForTreedAndCoins[pos]))
                {
                    _usedPos.Add(_xPositionsForTreedAndCoins[pos]);
                    var newTree = _mapManager.GetNewTree();
                    newTree.transform.position = new Vector3(_xPositionsForTreedAndCoins[pos] - 0.3f, 0.5f, transform.position.z);
                    flag = false;
                }
                _checkCicle++;
            }
            flag = true;          
        }
        var value = Random.Range(0, 6);
        if (1 < value && value < 3)
        {
            var pos = Random.Range(0, 10);
            if (!_usedPos.Contains(_xPositionsForTreedAndCoins[pos]))
            {
                _usedPos.Add(_xPositionsForTreedAndCoins[pos]);
                var newCoin = _mapManager.GetNewCoin();
                newCoin.transform.position = new Vector3(_xPositionsForTreedAndCoins[pos] - 0.3f, 0.5f, transform.position.z);
            }
            
        }
    }

    private void ClearList()
    {
        this._usedPos.Clear();
    }
}
