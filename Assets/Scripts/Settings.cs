using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button _scoreTable;
    [SerializeField] private Button _menu;

    private void OnEnable()
    {
        _scoreTable.onClick.AddListener(OnScoreTableButtonClick);
        _menu.onClick.AddListener(OnMenuButtonClick);
    }

    private void OnDisable()
    {
        _scoreTable.onClick.RemoveListener(OnScoreTableButtonClick);
        _menu.onClick.RemoveListener(OnMenuButtonClick);
    }

    private void OnScoreTableButtonClick()
    {
        SceneManager.LoadScene(2);
    }

    private void OnMenuButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}