using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nicknameAndBestScore;
    [SerializeField] private TMP_InputField _inputNicknameWindow;

    [Header("Button")]
    [SerializeField] private Button _start;
    [SerializeField] private Button _scoreTable;
    [SerializeField] private Button _quit;

    private string _password = "RomanKopylov";

    private void OnEnable()
    {
        _start.onClick.AddListener(OnStartButtonClick);
        _scoreTable.onClick.AddListener(OnScoreTableButtonClick);
        _quit.onClick.AddListener(OnQuitButtonClick);        
    }

    private void OnDisable()
    {
        _start.onClick.RemoveListener(OnStartButtonClick);
        _scoreTable.onClick.AddListener(OnScoreTableButtonClick);
        _quit.onClick.RemoveListener(OnQuitButtonClick);        
    }

    private void Start()
    {
        LoadNicknameAndBestScore();
    }

    private void LoadNicknameAndBestScore()
    {
        if(Progress.Instanse.CurrentNickname != "")
        {
            _inputNicknameWindow.text = Progress.Instanse.CurrentNickname;
        }

        if(Progress.Instanse.BestScore != 0)
        {
            _nicknameAndBestScore.text = "Best Score : " + Progress.Instanse.NicknameAsBestScore + " : " + Progress.Instanse.BestScore;
        }
    }

    private void OnStartButtonClick()
    {
        if(_inputNicknameWindow.text != "")
        {
            Progress.Instanse.SetNickname(_inputNicknameWindow.text, _password);
            SceneManager.LoadScene(1);
        }        
    }

    private void OnScoreTableButtonClick()
    {
        SceneManager.LoadScene(2);
    }

    private void OnQuitButtonClick()
    {
        Progress.Instanse.SaveLastInputNickname();
        Progress.Instanse.SaveNicknamesAndBestScores();
        Exit();
    }

    private void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
    }
}