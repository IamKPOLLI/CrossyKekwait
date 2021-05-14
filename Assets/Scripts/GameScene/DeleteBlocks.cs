using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBlocks : MonoBehaviour
{
    //скрипт который возвращает все объекты в пул, если их уже не видно


    [SerializeField] private mapManager _mapManager;
    [SerializeField] private GameObject _player;
    private Vector3 _offset;
    void Start()
    {
        _offset = transform.position - _player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("RiverBlock"))
        {
            other.gameObject.SetActive(false);
            _mapManager.CreateNewBlock();
        }
        if ( other.gameObject.CompareTag("Coin") || other.gameObject.CompareTag("Let"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Car"))
        {
            var h = other.GetComponent<CarController>();
            h.Stop();
        }
        if (other.gameObject.CompareTag("Log"))
        {
            LogController h = other.GetComponent<LogController>();
            h.Stop();
        }



    }

}
