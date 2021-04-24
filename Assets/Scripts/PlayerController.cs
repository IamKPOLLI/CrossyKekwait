using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    Vector3 _target;
    Vector3 _angleOfRotate;
    [SerializeField] Rigidbody rb;
    float duration;
    Sequence _jump;
    void Start() {
        duration = 0.3f;
        _jump = rb.DOJump(_target, 1, 1, duration);
        _jump.Join(transform.DORotate(_angleOfRotate, duration));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!_jump.IsPlaying()) { Jump(2); }               
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!_jump.IsPlaying()) { Jump(3); }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!_jump.IsPlaying()) { Jump(1); }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!_jump.IsPlaying()) { Jump(4); }
        }
    }

    void Jump(int diraction)
    {

        switch (diraction)
        {
            case 1: 
                _target = new Vector3(transform.position.x + 1.6f, transform.position.y, transform.position.z);
                _angleOfRotate = new Vector3(0, 90, 0);
                _jump = transform.DOJump(_target, 1, 1, duration);
                _jump.Join(transform.DORotate(_angleOfRotate, duration));
                break;
            case 2:
                _target = new Vector3(transform.position.x - 1.6f, transform.position.y, transform.position.z);
                _angleOfRotate = new Vector3(0, -90, 0);
                _jump = transform.DOJump(_target, 1, 1, duration);
                _jump.Join(transform.DORotate(_angleOfRotate, duration));
                break;
            case 3:
                _target = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.6f);
                _angleOfRotate = new Vector3(0, 0, 0);
                _jump = transform.DOJump(_target, 1, 1, duration);
                _jump.Join(transform.DORotate(_angleOfRotate, duration));
                break;
            case 4:
                _target = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.6f);
                _angleOfRotate = new Vector3(0, 180, 0);
                _jump = transform.DOJump(_target, 1, 1, duration);
                _jump.Join(transform.DORotate(_angleOfRotate, duration));
                break;
        }
       
        _jump.Play();
    

    }
}
