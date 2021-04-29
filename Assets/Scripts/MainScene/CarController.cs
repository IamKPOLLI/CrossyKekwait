using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
 
public class CarController : MonoBehaviour 
{
    //контроллер машины

    private Tween _move;
    private float _timeToDie = 15f;
    private float _timeToMove = 17f;

    public void move(float to)
    {
        
        _move = transform.DOMoveX(to, _timeToMove);
        _move.Play();
        StartCoroutine(DeleteCar());
    }

    public void Stop()
    {    
        _move.Kill();
        this.gameObject.SetActive(false);
    }

    private IEnumerator DeleteCar() 
    {
        yield return new WaitForSeconds(_timeToDie);    
        Stop();
    }

}
