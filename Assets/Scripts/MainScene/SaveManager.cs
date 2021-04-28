using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private Text _bestScorelabel;
    [SerializeField] private Text _countCoinLabel;
    [SerializeField] private GameController gameController;




    private void Start()
    {
        Load();

    }


    public void Save()
    {
     
        PlayerPrefs.SetInt("bestScore", gameController.bestResult);
        PlayerPrefs.SetInt("coinCount",gameController.coinCount);
        PlayerPrefs.Save();
        _bestScorelabel.text = "Best Score:" + gameController.bestResult;
        _countCoinLabel.text = gameController.coinCount.ToString();

    }

    public void Load()
    {

        if (PlayerPrefs.HasKey("bestScore"))
        {
            gameController.bestResult = PlayerPrefs.GetInt("bestScore");
        }
        else
        {
            PlayerPrefs.SetInt("bestScore", gameController.bestResult);
            PlayerPrefs.Save();
            gameController.bestResult = 0;
        }

        if (PlayerPrefs.HasKey("coinCount"))
        {
            gameController.coinCount = PlayerPrefs.GetInt("coinCount");
        }
        else
        {
            PlayerPrefs.SetInt("coinCount", gameController.bestResult);
            PlayerPrefs.Save();
            gameController.coinCount = 0;
        }


        _bestScorelabel.text = "Best Score:" + gameController.bestResult;
        _countCoinLabel.text = gameController.coinCount.ToString();

    }


}
