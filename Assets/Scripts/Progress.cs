using System;
using System.IO;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public static Progress Instanse;

    private NicknamesAndBestScores[] nicknamesAndBestScores;
    private string _currentNickname;
    private string _nicknameAsBestScore;
    private int _bestScore;    
    private int _worstScore = 0;
    private int _lengthArray = 10;
    private string _nameJsonFileNicknameAndBestScore = "/4.json";
    private string _nameJsonFileLastNickname = "/lastnickname.json";

    private string _password = "RomanKopylov";

    public string CurrentNickname => _currentNickname;
    public string NicknameAsBestScore => _nicknameAsBestScore;
    public int BestScore => _bestScore;
    public int WorstScore => _worstScore;
    public int LengthArray => _lengthArray;    

    private void Awake()
    {
        if(Instanse != null)
        {
            Destroy(gameObject);
            return;
        }

        Instanse = this;

        DontDestroyOnLoad(gameObject);

        nicknamesAndBestScores = new NicknamesAndBestScores[_lengthArray];

        LoadNicknamesAndBestScores();
        LoadLastInputNickname();
        SetBestScoreAndNickname();
        SetWorstScore();
    }    

    public void SetNickname(string nickname, string password)
    {        
        if (password == _password)
        {
            _currentNickname = nickname;
        }
    }

    public void SetNewScore(int worstScore, string password)
    {
        if(password == _password)
        {
            _worstScore = worstScore;
        }        
    }

    public void OverwriteNicknameAndBestScore()
    {        
        nicknamesAndBestScores[nicknamesAndBestScores.Length - 1].NicknameWithBestScore = _currentNickname;
        nicknamesAndBestScores[nicknamesAndBestScores.Length - 1].BestScore = _worstScore;

        Sort();
        SetBestScoreAndNickname();
        SetWorstScore();
    }

    public void GetNicknameAndBestScore(int countNumber,out string nickname, out int score)
    {
        nickname = nicknamesAndBestScores[countNumber].NicknameWithBestScore;
        score = nicknamesAndBestScores[countNumber].BestScore;
    }

    public void SaveNicknamesAndBestScores()
    {
        SaveDataNicknameAndBestScore[] saveData = new SaveDataNicknameAndBestScore[nicknamesAndBestScores.Length];

        for (int i = 0; i < nicknamesAndBestScores.Length; i++)
        {
            if (nicknamesAndBestScores[i].BestScore != 0)
            {
                saveData[i].NicknameWithBestScore = nicknamesAndBestScores[i].NicknameWithBestScore;
                saveData[i].BestScore = nicknamesAndBestScores[i].BestScore;
            }
        }        

        string json = JsonToArray.ToJson(saveData);
        string pathToNicknameAndBestScoreJson = Application.persistentDataPath + _nameJsonFileNicknameAndBestScore;
        File.WriteAllText(pathToNicknameAndBestScoreJson, json);
    }

    public void LoadNicknamesAndBestScores()
    {
        string pathToNicknameAndBestScoreJson = Application.persistentDataPath + _nameJsonFileNicknameAndBestScore;

        if (File.Exists(pathToNicknameAndBestScoreJson))
        {
            string json = File.ReadAllText(pathToNicknameAndBestScoreJson);
            SaveDataNicknameAndBestScore[] saveData = JsonToArray.FromJson<SaveDataNicknameAndBestScore>(json);

            for (int i = 0; i < nicknamesAndBestScores.Length; i++)
            {
                if (saveData[i].BestScore != 0)
                {
                    nicknamesAndBestScores[i].NicknameWithBestScore = saveData[i].NicknameWithBestScore;
                    nicknamesAndBestScores[i].BestScore = saveData[i].BestScore;
                }
            }            
        }
    }

    public void SaveLastInputNickname()
    {
        SaveDataLastNickname saveData = new SaveDataLastNickname();

        saveData.LastInputNickname = _currentNickname;
        string json = JsonUtility.ToJson(saveData);
        string pathToNicknameAndBestScoreJson = Application.persistentDataPath + _nameJsonFileLastNickname;
        File.WriteAllText(pathToNicknameAndBestScoreJson, json);
    }

    public void LoadLastInputNickname()
    {
        string pathToNicknameAndBestScoreJson = Application.persistentDataPath + _nameJsonFileLastNickname;

        if (File.Exists(pathToNicknameAndBestScoreJson))
        {
            string json = File.ReadAllText(pathToNicknameAndBestScoreJson);
            SaveDataLastNickname saveData = JsonUtility.FromJson<SaveDataLastNickname>(json);

            _currentNickname = saveData.LastInputNickname;            
        }
    }

    private void Sort()
    {
        int length = nicknamesAndBestScores.Length;
        int i = 1;

        while (nicknamesAndBestScores[length - (i + 1)].BestScore < 
            nicknamesAndBestScores[length - i].BestScore)
        {
            (nicknamesAndBestScores[length - (i + 1)].NicknameWithBestScore,
                nicknamesAndBestScores[length - i].NicknameWithBestScore) =
                (nicknamesAndBestScores[length - i].NicknameWithBestScore,
                nicknamesAndBestScores[length - (i + 1)].NicknameWithBestScore);

            (nicknamesAndBestScores[length - (i + 1)].BestScore, 
                nicknamesAndBestScores[length - i].BestScore) =
                (nicknamesAndBestScores[length - i].BestScore, 
                nicknamesAndBestScores[length - (i + 1)].BestScore);

            i++;

            if (i == length)
                break;
        }
    }

    private void SetBestScoreAndNickname()
    {
        if(nicknamesAndBestScores[0].BestScore != 0)
        {
            _bestScore = nicknamesAndBestScores[0].BestScore;
            _nicknameAsBestScore = nicknamesAndBestScores[0].NicknameWithBestScore;
        }
    }

    private void SetWorstScore()
    {
        _worstScore = nicknamesAndBestScores[_lengthArray - 1].BestScore;
    }

    private struct NicknamesAndBestScores 
    {
        public string NicknameWithBestScore;
        public int BestScore;
    }

}

[Serializable]
public struct SaveDataNicknameAndBestScore
{
    public string NicknameWithBestScore;    
    public int BestScore;
}

public struct SaveDataLastNickname
{
    public string LastInputNickname;
}