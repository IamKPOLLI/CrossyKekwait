﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
 
public class CarController : MonoBehaviour 
{

    private Tween _move;

    public void move(float to)
    {
        
        _move = transform.DOMoveX(to, 17);
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
        yield return new WaitForSeconds(15);    
        Stop();
    }

}
