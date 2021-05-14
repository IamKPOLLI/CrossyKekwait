using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrainController : MonoBehaviour
{
    //контроллер поезда

    private Tween _move;
    private float _timeToDie = 4f;
    private float _timeToMove = 2f;

    public void move(float to)
    {

        _move = transform.DOMoveX(to, _timeToMove);
        _move.Play();
        StartCoroutine(DeleteTrain());
    }

    public void Stop()
    {
        _move.Kill();
        this.gameObject.SetActive(false);
    }

    private IEnumerator DeleteTrain()
    {
        yield return new WaitForSeconds(_timeToDie);
        Stop();
    }
}
