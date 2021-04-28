using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    Vector3 _target;
    Vector3 _angleOfRotate;
    [SerializeField] Rigidbody rb;
    [SerializeField] private GameController _gameController;
    float _duration;
    Sequence _jump;
    private float _maxZ = 0;
    void Start() {
        _duration = 0.3f;
        _jump = rb.DOJump(_target, 1, 1, _duration);
        _jump.Join(transform.DORotate(_angleOfRotate, _duration));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!_jump.IsPlaying()) {
                if(IsFree(2))Jump(2);
            }               
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!_jump.IsPlaying())
            {
                if (IsFree(3)) Jump(3);
                if (transform.position.z > _maxZ)
                {
                    _maxZ = transform.position.z;
                    _gameController.AddScore();
                }
                
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!_jump.IsPlaying())
            {
                if (IsFree(1)) Jump(1);
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!_jump.IsPlaying())
            {
                if (IsFree(4)) Jump(4);
            }
        }
    }

    void Jump(int diraction)
    {

        switch (diraction)
        {
            case 1: 
                _target = new Vector3(transform.position.x + 1.6f, transform.position.y, transform.position.z);
                _angleOfRotate = new Vector3(0, 90, 0);
                _jump = transform.DOJump(_target, 1, 1, _duration);
                _jump.Join(transform.DORotate(_angleOfRotate, _duration));
                break;
            case 2:
                _target = new Vector3(transform.position.x - 1.6f, transform.position.y, transform.position.z);
                _angleOfRotate = new Vector3(0, -90, 0);
                _jump = transform.DOJump(_target, 1, 1, _duration);
                _jump.Join(transform.DORotate(_angleOfRotate, _duration));
                break;
            case 3:
                _target = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.6f);
                _angleOfRotate = new Vector3(0, 0, 0);
                _jump = transform.DOJump(_target, 1, 1, _duration);
                _jump.Join(transform.DORotate(_angleOfRotate, _duration));
                break;
            case 4:
                _target = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.6f);
                _angleOfRotate = new Vector3(0, 180, 0);
                _jump = transform.DOJump(_target, 1, 1, _duration);
                _jump.Join(transform.DORotate(_angleOfRotate, _duration));
                break;
        }
       
        _jump.Play();
    

    }

    private bool IsFree(int diraction)



    {
        Vector3 size = new Vector3(0.7f, 0.4f,  0.7f);
        Vector3 pos = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
        switch (diraction) 
        {
            case 1:
                pos = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
                break;
            case 2:
                pos = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
                break;
            case 3:
                pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.8f);
                break;
            case 4:
                pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.8f);
                break;
        }

        
        Collider[] others = Physics.OverlapBox(pos,size);
        for (int i = 0; i < others.Length; i++)
        {
            if (others[i].gameObject.CompareTag("Let"))
            {
                return false;
            }
        }
        return true;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            this.gameObject.SetActive(false);
            _gameController.SaveBestScore();
            _gameController.ShowLoseButtons();
        }
    }
}
