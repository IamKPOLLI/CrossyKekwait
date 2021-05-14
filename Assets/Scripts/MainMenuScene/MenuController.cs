using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //контроллер стартовой сцены

    [SerializeField] private Text _bottles;
    [SerializeField] private Image _volumeOff;



    private void Start()
    {
        LoadBottles();
        
    }
    public void StartPlay()
    {
        AudioController.Instance.PlayButton();
        SceneManager.LoadScene("GamePlay");
    }

    public void VolumeClick()
    {
        AudioController.Instance.PlayButton();
        AudioController.Instance.ChangeVolumeMute();
        ChangeVolumeImage();
    }

    private void LoadBottles()
    {
        if (PlayerPrefs.HasKey("coinCount"))
        {
            _bottles.text = PlayerPrefs.GetInt("coinCount").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("coinCount", int.Parse(_bottles.text));
            PlayerPrefs.Save();
        }
    }

    public void ChangeVolumeImage()
    {
        _volumeOff.gameObject.SetActive(!_volumeOff.gameObject.activeSelf);
    }





}
