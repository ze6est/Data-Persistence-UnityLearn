using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HightScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _nicknameAndBestScore;

    [Header("Button")]
    [SerializeField] private Button _game;
    [SerializeField] private Button _menu;    

    private void OnEnable()
    {        
        _game.onClick.AddListener(OnGameButtonClick);
        _menu.onClick.AddListener(OnMenuButtonClick);
    }

    private void OnDisable()
    {        
        _game.onClick.RemoveListener(OnGameButtonClick);
        _menu.onClick.RemoveListener(OnMenuButtonClick);
    }

    private void Start()
    {
        LoadNicknamesAndBestScores();
    }

    private void LoadNicknamesAndBestScores()
    {
        for (int i = 0; i < Progress.Instanse.LengthArray; i++)
        {
            if (Progress.Instanse.BestScore != 0)
            {
                string nickname;
                int score;

                Progress.Instanse.GetNicknameAndBestScore(i, out nickname, out score);

                if(score != 0)                
                    _nicknameAndBestScore[i].text = "Best Score : " + nickname + " : " + score;
            }
        }        
    }    

    private void OnGameButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    private void OnMenuButtonClick()
    {
        SceneManager.LoadScene(0);
    }    
}