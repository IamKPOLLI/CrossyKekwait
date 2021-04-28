using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
 
public class CarController : MonoBehaviour 
{

    private Tween _move;

    public void move(float to)
    {
        
        _move = transform.DOMoveX(to, 8);
        _move.Play();
        StartCoroutine(DeleteCar());
    }

    public void Stop()
    {
        _move.Kill();
    }

    private IEnumerator DeleteCar() 
    {
        yield return new WaitForSeconds(10);

        this.gameObject.SetActive(false);
        Stop();
    }

}
