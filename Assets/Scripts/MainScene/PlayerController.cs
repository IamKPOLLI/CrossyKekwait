using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //контроллер игрока

    private bool _isOnLog = false;

    private const float _MAX_SWIPE_TIME = 0.5f;
    private const float _MIN_SWIPE_DISTANCE = 0.1f;

    private static bool _swipedRight = false;
    private static bool _swipedLeft = false;
    private static bool _swipedUp = false;

    private Vector2 _startPos;
    private float _startTime;



    public float minSwipDelta = 10f;

    Vector3 _target;
    Vector3 _angleOfRotate;
    [SerializeField] private GameController _gameController;
    float _duration;
    Sequence _jump;
    private float _maxZ = 0;
    void Start() {
        _duration = 0.3f;
        _jump = transform.DOJump(_target, 1, 1, _duration);
        _jump.Join(transform.DORotate(_angleOfRotate, _duration));
    }


    public void Update()
    {
        _swipedRight = false;
        _swipedLeft = false;
        _swipedUp = false;


        //код для отладки на компьютере

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

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!_jump.IsPlaying())
            {
                if (IsFree(2)) Jump(2);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!_jump.IsPlaying())
            {
                if (IsFree(1)) Jump(1);
            }
        }







        // код для управления с телефона свайпами

        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                _startPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);
                _startTime = Time.time;
            }
            if (t.phase == TouchPhase.Ended)
            {
                if (Time.time - _startTime > _MAX_SWIPE_TIME) // press too long
                    return;

                Vector2 endPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);

                Vector2 swipe = new Vector2(endPos.x - _startPos.x, endPos.y - _startPos.y);

                if (swipe.magnitude < _MIN_SWIPE_DISTANCE)
                {
                    return;
                }

                if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                {
                    if (swipe.x > 0)
                    {
                        _swipedRight = true;
                    }
                    else
                    {
                        _swipedLeft = true;
                    }
                }
                else
                {
                    if (swipe.y > 0)
                    {
                        _swipedUp = true;
                    }

                }
            }
        }




        if (_swipedUp)
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

        if (_swipedRight)
        {
            if (!_jump.IsPlaying())
            {
                if (IsFree(1)) Jump(1);
            }
        }

        if (_swipedLeft)
        {
            if (!_jump.IsPlaying())
            {
                if (IsFree(2)) Jump(2);
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
                AudioController.Instance.PlayJump();
                _jump = transform.DOJump(_target, 1, 1, _duration);
                _jump.Join(transform.DORotate(_angleOfRotate, _duration));
                break;
            case 2:
                _target = new Vector3(transform.position.x - 1.6f, transform.position.y, transform.position.z);
                _angleOfRotate = new Vector3(0, -90, 0);
                AudioController.Instance.PlayJump();
                _jump = transform.DOJump(_target, 1, 1, _duration);
                _jump.Join(transform.DORotate(_angleOfRotate, _duration));
                break;
            case 3:
                _target = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.6f);
                _angleOfRotate = new Vector3(0, 0, 0);
                AudioController.Instance.PlayJump();
                _jump = transform.DOJump(_target, 1, 1, _duration);                
                _jump.Join(transform.DORotate(_angleOfRotate, _duration));
                break;
            case 4:
                _target = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.6f);
                _angleOfRotate = new Vector3(0, 180, 0);
                AudioController.Instance.PlayJump();
                _jump = transform.DOJump(_target, 1, 1, _duration);
                _jump.Join(transform.DORotate(_angleOfRotate, _duration));
                break;
        }
       
        _jump.Play();
        _isOnLog = false;
    

    }

    private bool IsFree(int diraction)
    {
        Vector3 size = new Vector3(0,0,0);
        Vector3 pos = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
        switch (diraction) 
        {
            case 1:
                size = new Vector3(1.2f, 0.4f, 0.1f);
                pos = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
                break;
            case 2:
                size = new Vector3(1.2f, 0.4f, 0.1f);
                pos = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
                break;
            case 3:
                size = new Vector3(0.1f, 0.4f, 1.2f);
                pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.8f);
                break;
            case 4:
                pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.8f);
                break;
        }

        
        Collider[] others = Physics.OverlapBox(pos,size);
        for (int i = 0; i < others.Length; i++)
        {
            if (others[i].gameObject.CompareTag("Let") || others[i].gameObject.CompareTag("Borders"))
            {
                return false;
            }
        }
        return true;


    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Log"))
        {
            _isOnLog = true;
            transform.SetParent(other.transform);
        }
        else
        {
            if (other.gameObject.CompareTag("Block") )
            {
                _isOnLog = false;
                transform.parent = null;
            }
        }

        if (other.gameObject.CompareTag("RiverBlock") && !_isOnLog)
        {

            AudioController.Instance.PlayGameOver();
            this.gameObject.SetActive(false);
            _gameController.SaveBestScore();
            _gameController.ShowLoseButtons();
            transform.parent = null;
        }
        if (other.gameObject.CompareTag("Car"))
        {
            AudioController.Instance.PlayGameOver();
            this.gameObject.SetActive(false);
            _gameController.SaveBestScore();
            _gameController.ShowLoseButtons();
            transform.parent = null;
        }
        if (other.gameObject.CompareTag("Borders"))
        {
            AudioController.Instance.PlayGameOver();
            this.gameObject.SetActive(false);
            _gameController.SaveBestScore();
            _gameController.ShowLoseButtons();
            transform.parent = null;
        }

    }



}
