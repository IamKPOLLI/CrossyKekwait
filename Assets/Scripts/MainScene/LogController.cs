using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LogController : MonoBehaviour
{
    private Tween _move;

    public void move(float to)
    {

        _move = transform.DOMoveX(to, 20);
        _move.Play();
        StartCoroutine(DeleteLog());
    }

    public void Stop()
    {
        _move.Kill();
        this.gameObject.SetActive(false);
    }

    private IEnumerator DeleteLog()
    {
        yield return new WaitForSeconds(15);
        Stop();
    }
}
