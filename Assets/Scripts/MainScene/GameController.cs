using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

    [SerializeField] private Text _scoreLabel;
    [SerializeField] private Text _countOfCoins;
    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;

    private int _currentScore = 0;
    public int coinCount = 0;
    public int bestResult;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        _currentScore++;
        _scoreLabel.text = "Score: " + _currentScore;
    }

    public void AddCoin()
    {
        coinCount++;
        _countOfCoins.text = coinCount.ToString();

    }

    public void SaveBestScore()
    {
        if(_currentScore > bestResult)
        {
            bestResult = _currentScore;
        }
        _saveManager.Save();

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");   
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShowLoseButtons()
    {
        _menuButton.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }
}
