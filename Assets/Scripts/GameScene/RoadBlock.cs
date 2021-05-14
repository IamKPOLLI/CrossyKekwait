using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoadBlock : MonoBehaviour
{
   //скрипт для блока дороги


    [SerializeField] private Transform _spanwPos1;
    [SerializeField] private Transform _spanwPos2;
    private mapManager _mapManager;
    private int _realSpawnPos;

    private float _moveTo = 20f;

    private void Awake()
    {
        _realSpawnPos = Random.Range(0, 2);
        _mapManager = FindObjectOfType<mapManager>();
    }

    public void AddNewCar()
    {
        StartCoroutine(CicleOfCar());
        
    }

    private IEnumerator CicleOfCar()
    {

        while (true)
        {

            var rand = Random.Range(2.5f, 6);
            yield return new WaitForSeconds(rand);

            
            var newCar = _mapManager.GetNewCar();

            if (_realSpawnPos == 0)
            {
                newCar.transform.position = _spanwPos1.position;
                newCar.move(-_moveTo);
            }
            else
            {
                newCar.transform.position = _spanwPos2.position;
                newCar.move(_moveTo);
            }
        }
    }
}
