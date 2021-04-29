using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameController _gameController;
    void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioController.Instance.PlayBottle();
            this.gameObject.SetActive(false);
            _gameController.AddCoin();

        }
    }
}
