using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{


    [SerializeField] private Text _bottles;



    private void Start()
    {
        LoadBottles();
    }
    public void StartPlay()
    {
        SceneManager.LoadScene("Main");
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

}
