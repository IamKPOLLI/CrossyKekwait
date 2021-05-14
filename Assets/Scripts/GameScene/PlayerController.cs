using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //контроллер игрока
    [SerializeField] private GameObject _player; // для следования камеры за персонажем

    private int _currentCountBack = 0;

    enum  directions
    {
        right = 1,
        left,
        up,
        down

    }


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
                if (IsFree((int)directions.up)) Jump((int)directions.up);
                if(_currentCountBack > 0)
                {
                    _currentCountBack--;
                }

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
                if (IsFree((int)directions.left)) Jump((int)directions.left);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!_jump.IsPlaying())
            {
                if (IsFree((int)directions.right)) Jump((int)directions.right);
            }
        }

        //изменения после фидбека
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!_jump.IsPlaying())
            {

                if (IsFree((int)directions.down) && _currentCountBack < 3)
                {
                    Jump((int)directions.down);
                    _currentCountBack++;
                }
                    
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




        //if (_swipedUp)
        //{
        //    if (!_jump.IsPlaying())
        //    {
        //        if (IsFree(3)) Jump(3);
        //        if (transform.position.z > _maxZ)
        //        {
        //            _maxZ = transform.position.z;
        //            _gameController.AddScore();
        //        }

        //    }
        //}

        //if (_swipedRight)
        //{
        //    if (!_jump.IsPlaying())
        //    {
        //        if (IsFree(1)) Jump(1);
        //    }
        //}

        //if (_swipedLeft)
        //{
        //    if (!_jump.IsPlaying())
        //    {
        //        if (IsFree(2)) Jump(2);
        //    }
        //}


    }




    void Jump(int diraction)
    {

        switch (diraction)
        {
            case 1:
                //изменения после фидбека
                //_target = new Vector3(transform.position.x + 1.6f, transform.position.y, transform.position.z);
                //_angleOfRotate = new Vector3(0, 90, 0);
                //AudioController.Instance.PlayJump();
                //_jump = transform.DOJump(_target, 1, 1, _duration);
                //_jump.Join(transform.DORotate(_angleOfRotate, _duration));
                DoJump(new Vector2(1.6f, 0), 90);
                break;
            case 2:
                //_target = new Vector3(transform.position.x - 1.6f, transform.position.y, transform.position.z);
                //_angleOfRotate = new Vector3(0, -90, 0);
                //AudioController.Instance.PlayJump();
                //_jump = transform.DOJump(_target, 1, 1, _duration);           
                //_jump.Join(transform.DORotate(_angleOfRotate, _duration));
                DoJump(new Vector2(-1.6f, 0), -90);
                break;
            case 3:
                //_target = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.6f);
                //_angleOfRotate = new Vector3(0, 0, 0);
                //AudioController.Instance.PlayJump();
                //_jump = transform.DOJump(_target, 1, 1, _duration);                
                //_jump.Join(transform.DORotate(_angleOfRotate, _duration));
                DoJump(new Vector2(0, 1.6f), 0);
                break;
            case 4:
                //_target = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.6f);
                //_angleOfRotate = new Vector3(0, 180, 0);
                //AudioController.Instance.PlayJump();
                //_jump = transform.DOJump(_target, 1, 1, _duration);
                //_jump.Join(transform.DORotate(_angleOfRotate, _duration));
                DoJump(new Vector2(0, -1.6f), 180);
                break;
        }
       
        _jump.Play();
        _isOnLog = false;
    

    }
    //изменения после фидбека
    private void DoJump(Vector2 direction,float yAngle)
    {
        _target = new Vector3(transform.position.x + direction.x, transform.position.y, transform.position.z + direction.y);
        _angleOfRotate = new Vector3(0, yAngle, 0);
        AudioController.Instance.PlayJump();
        _jump = _player.transform.DOJump(_target, 1, 1, _duration);
        _jump.Join(transform.DORotate(_angleOfRotate, _duration));
    }


    private bool IsFree(int diraction)
    {
        Vector3 size = new Vector3(0,0,0);
        Vector3 pos = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
        switch (diraction) 
        {
            case 1:
                //изменения после фидбека
                //size = new Vector3(1.2f, 0.4f, 0.1f);
                //pos = new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z);
                setSizePos(new Vector3(1.2f, 0.4f, 0.1f), new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z), ref size, ref pos);
                break;
            case 2:
                //size = new Vector3(1.2f, 0.4f, 0.1f);
                //pos = new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z);
                setSizePos(new Vector3(1.2f, 0.4f, 0.1f), new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z), ref size, ref pos);
                break;
            case 3:
                //size = new Vector3(0.1f, 0.4f, 1.2f);
                //pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.8f);
                setSizePos(new Vector3(0.1f, 0.4f, 1.2f), new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.8f), ref size, ref pos);
                break;
            case 4:
                //size = new Vector3(0.1f, 0.4f, 1.2f);
                //pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.8f);
                setSizePos(new Vector3(0.1f, 0.4f, 1.2f), new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.8f), ref size, ref pos);
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

    //изменения после фидбека
    private void setSizePos(Vector3 sizeForSet,Vector3 posForSet,ref Vector3 size,ref Vector3 pos)
    {
        size = sizeForSet;
        pos = posForSet;
    }

    //оставил сравнение по тэгу, так как не нашел никакой альтернативы кроме getComponent("что-то") и насколько мне известно это еще хуже
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Log"))
        {
            _isOnLog = true;
            _player.transform.SetParent(other.transform);
        }
  
        
        if (other.gameObject.CompareTag("Block") )
        {
            _isOnLog = false;
            _player.transform.parent = null;
        }
        

        if (other.gameObject.CompareTag("RiverBlock") && !_isOnLog)
        {
            doAfterLose(this.gameObject);
            //AudioController.Instance.PlayGameOver();
            //this.gameObject.SetActive(false);
            //_gameController.SaveBestScore();
            //_gameController.ShowLoseButtons();
            //transform.parent = null;
        }
        if (other.gameObject.CompareTag("Car"))
        {
            doAfterLose(this.gameObject);
            //AudioController.Instance.PlayGameOver();
            //this.gameObject.SetActive(false);
            //_gameController.SaveBestScore();
            //_gameController.ShowLoseButtons();
            //transform.parent = null;
        }
        if (other.gameObject.CompareTag("Borders"))
        {
            doAfterLose(this.gameObject);
            //изменения после фидбека
            //AudioController.Instance.PlayGameOver();
            //this.gameObject.SetActive(false);
            //_gameController.SaveBestScore();
            //_gameController.ShowLoseButtons();
            //transform.parent = null;
        }

    }


    //изменения после фидбека
    private void doAfterLose(GameObject gameObject)
    {
        AudioController.Instance.PlayGameOver();
        gameObject.SetActive(false);
        _gameController.SaveBestScore();
        _gameController.ShowLoseButtons();
        _player.transform.parent = null;
    }



}
