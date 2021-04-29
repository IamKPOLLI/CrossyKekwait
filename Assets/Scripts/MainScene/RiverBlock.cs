using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverBlock : MonoBehaviour
{

    // скрипт для блока реки

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

    public void AddNewLog()
    {
        StartCoroutine(CicleOfLog());

    }


    private IEnumerator CicleOfLog()
    {

        while (true)
        {

            var rand = Random.Range(2.2f, 5f);
            yield return new WaitForSeconds(rand);


            var newLog = _mapManager.GetNewLog();

            if (_realSpawnPos == 0)
            {
                newLog.transform.position = _spanwPos1.position;
                newLog.move(-_moveTo);
            }
            else
            {
                newLog.transform.position = _spanwPos2.position;
                newLog.move(_moveTo);
            }
        }
    }
}
