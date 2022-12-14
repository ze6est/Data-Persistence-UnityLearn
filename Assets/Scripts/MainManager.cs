using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private Brick _brickPrefab;
    [SerializeField] private int _lineCount = 6;
    [SerializeField] private Rigidbody _ball;

    [Header("UI")]
    [SerializeField] private Text _nicknameAndBestScore;
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _gameOverPanel;    
    [SerializeField] private Button _menu;
    [SerializeField] private Button _scoreTable;

    private bool m_Started = false;
    private int m_Points;    
    
    private bool m_GameOver = false;

    private string _password = "RomanKopylov";

    private void OnEnable()
    {
        _menu.onClick.AddListener(OnMenuButtonClick);
        _scoreTable.onClick.AddListener(OnScoreTableButtonClick);
    }

    private void OnDisable()
    {
        _menu.onClick.RemoveListener(OnMenuButtonClick);
        _scoreTable.onClick.RemoveListener(OnScoreTableButtonClick);
    }
    
    void Start()
    {
        _gameOverPanel.SetActive(false);
        SetBestScore();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < _lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(_brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                _ball.transform.SetParent(null);
                _ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void AddPoint(int point)
    {
        m_Points += point;
        _scoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        _gameOverPanel.SetActive(true);
        TrySaveNewScore();
    }

    private void TrySaveNewScore()
    {
        if(m_Points > Progress.Instanse.WorstScore)
        {
            Progress.Instanse.SetNewScore(m_Points, _password);
            Progress.Instanse.OverwriteNicknameAndBestScore();
        }
    }

    private void SetBestScore()
    {
        if (Progress.Instanse.BestScore != 0)
        {
            _nicknameAndBestScore.text = "Best Score : " + Progress.Instanse.NicknameAsBestScore + " : " + Progress.Instanse.BestScore;
        }
    }

    private void OnMenuButtonClick()
    {
        SceneManager.LoadScene(0);        
    }

    private void OnScoreTableButtonClick()
    {
        SceneManager.LoadScene(2);
    }
}