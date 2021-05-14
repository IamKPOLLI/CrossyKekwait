using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RayWallsBlock : MonoBehaviour
{
    [SerializeField] private Transform _spanwPos1;
    [SerializeField] private Transform _spanwPos2;
    [SerializeField] private Renderer _mesh1;
    [SerializeField] private Renderer _mesh2;


    private mapManager _mapManager;
    private int _realSpawnPos;

    private float _moveTo = 40f;
    private float _trainFrequency = 12;

    private void Awake()
    {
        _realSpawnPos = Random.Range(0, 2);
        _mapManager = FindObjectOfType<mapManager>();
    }

    public void AddNewTrain()
    {
        StartCoroutine(NewTrain());

    }
    

    private IEnumerator NewTrain()
    {

        while (true)
        {
            yield return new WaitForSeconds(_trainFrequency);


            
            StartCoroutine(ChangeColor());
            yield return new WaitForSeconds(2);
            var newTrain = _mapManager.GetTrain();

            if (_realSpawnPos == 0)
            {
                
                newTrain.transform.position = _spanwPos1.position;
                newTrain.move(-_moveTo);
            }
            else
            {
              
                newTrain.transform.position = _spanwPos2.position;
                newTrain.move(_moveTo);
            }

        }
    }

    private  IEnumerator ChangeColor()
    {
        _mesh1.material.color = Color.red;
        _mesh2.material.color = Color.red;

        yield return new WaitForSeconds(4);

        _mesh1.material.color = Color.green;
        _mesh2.material.color = Color.green;


    }
}
