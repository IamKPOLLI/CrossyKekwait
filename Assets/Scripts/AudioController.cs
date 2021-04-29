using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    //синглтон для управления звуками, чтобы воспроизводить их при переключении сцен


    public static AudioController Instance;
    private int _isMute;

    [SerializeField] private MenuController _menuController;
    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private AudioSource _soundBootle;
    [SerializeField] private AudioSource _soundGameOver;
    [SerializeField] private AudioSource _soundButton;
    [SerializeField] private AudioClip[] sounds;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Start()
    {
        LoadSettings();
    }


    public void PlayJump()
    {
        if (_isMute == 0) _jumpSound.PlayOneShot(sounds[0]);
        
    }

    public void PlayBottle()
    {
        if (_isMute == 0)
            _soundBootle.PlayOneShot(sounds[1]);
    }

    public void PlayGameOver()
    {
        if (_isMute == 0)
            _soundGameOver.PlayOneShot(sounds[2]);
    }

    public void PlayButton()
    {
        if (_isMute == 0)
            _soundButton.PlayOneShot(sounds[3]);
    }

    public void ChangeVolumeMute()
    {
        if(_isMute == 1)
        {
            _isMute = 0;
        }
        else
        {
            _isMute = 1;
        }
        PlayerPrefs.SetInt("volume", _isMute);
        PlayerPrefs.Save();

    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
           _isMute = PlayerPrefs.GetInt("volume");
            if(_isMute == 0)
            {
                _menuController.ChangeVolumeImage();
            }
        }
        else
        {
            Debug.Log(1111);
            _isMute = 0;
            PlayerPrefs.SetInt("volume", _isMute);
            PlayerPrefs.Save();
        }

    }  


}
