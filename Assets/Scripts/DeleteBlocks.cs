using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBlocks : MonoBehaviour
{
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
        if (other.gameObject.CompareTag("Block"))
        {
            other.gameObject.SetActive(false);
            _mapManager.CreateNewBlock();
        }
    }
}
