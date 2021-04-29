using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBorders : MonoBehaviour
{

    //скрипт для передвижения границ игры только по оси Х

    [SerializeField] private GameObject _player;
    [SerializeField] private GameController _gameController;
    private float _offset;
    void Start()
    {
        _offset = transform.position.z - _player.transform.position.z;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x ,transform.position.y, _player.transform.position.z + _offset);
    }


}
