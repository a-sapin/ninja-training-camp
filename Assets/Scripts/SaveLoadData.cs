using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadData : MonoBehaviour
{
    public static TimeSpan bronzeTime;
    public static TimeSpan silverTime;
    public static TimeSpan goldTime;
    private static TimeSpan _playerTime1 = TimeSpan.FromSeconds(-1);
    private static TimeSpan _playerTime2 = TimeSpan.FromSeconds(-1);
    private static TimeSpan _playerTime3 = TimeSpan.FromSeconds(-1);

    public void Start()
    {
        LoadMedalTime(SceneManager.GetActiveScene().name);
        LoadPlayerTime(SceneManager.GetActiveScene().name);
        //debugData();
    }

    public static void SavePlayerTimes()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file =
            File.Create(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Player.dat");

        PlayerData data = new PlayerData
        {
            playerTime1 = _playerTime1,
            playerTime2 = _playerTime2,
            playerTime3 = _playerTime3
        };

        bf.Serialize(file, data);
        file.Close();
        //Debug.Log("Game data saved!");
    }

    public static void SaveTimes(TimeSpan[] medalTimes)
    {
        bronzeTime = medalTimes[2];
        silverTime = medalTimes[1];
        goldTime = medalTimes[0];

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file =
            File.Create(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Times.dat");

        LevelData data = new LevelData
        {
            bronzeTime = bronzeTime,
            silverTime = silverTime,
            goldTime = goldTime
        };

        bf.Serialize(file, data);
        file.Close();
        //Debug.Log("Game data saved!");
    }

    public static void LoadPlayerTime(String levelName)
    {
        //Debug.Log(Application.persistentDataPath + "/" + levelName + "_Player.dat");
        if (File.Exists(Application.persistentDataPath + "/" + levelName + "_Player.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath + "/" + levelName + "_Player.dat",
                    FileMode.Open);

            PlayerData data = (PlayerData) bf.Deserialize(file);
            file.Close();
            _playerTime1 = data.playerTime1;
            _playerTime2 = data.playerTime2;
            _playerTime3 = data.playerTime3;

            //debugData();

            //Debug.Log("Player time loaded!");
        }
        else
        {
            _playerTime1 = TimeSpan.Zero;
            _playerTime2 = TimeSpan.Zero;
            _playerTime3 = TimeSpan.Zero;
            //Debug.Log("No Player time !");
        }
    }

    public static void LoadMedalTime(String levelName)
    {
        //Debug.Log(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Times.dat");
        if (File.Exists(Application.persistentDataPath + "/" + levelName + "_Times.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath + "/" + levelName + "_Times.dat",
                    FileMode.Open);

            LevelData data = (LevelData) bf.Deserialize(file);
            file.Close();
            bronzeTime = data.bronzeTime;
            silverTime = data.silverTime;
            goldTime = data.goldTime;

            //Debug.Log("Medal time loaded!");
        }
        else
        {
            bronzeTime = TimeSpan.Zero;
            silverTime = TimeSpan.Zero;
            goldTime = TimeSpan.Zero;
            //Debug.Log("No medal times !");
        }
    }

    public static void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Times.dat"))
        {
            BinaryFormatter bfTime = new BinaryFormatter();
            FileStream fileTime =
                File.Open(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Times.dat",
                    FileMode.Open);

            LevelData dataTime = (LevelData) bfTime.Deserialize(fileTime);
            fileTime.Close();
            bronzeTime = dataTime.bronzeTime;
            silverTime = dataTime.silverTime;
            goldTime = dataTime.goldTime; 
            
        }
        if (File.Exists(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Player.dat")){
            BinaryFormatter bfPlayer = new BinaryFormatter();
            FileStream filePlayer =
                File.Open(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Player.dat",
                    FileMode.Open);

            PlayerData dataPlayer = (PlayerData) bfPlayer.Deserialize(filePlayer);
            filePlayer.Close();
            _playerTime1 = dataPlayer.playerTime1;
            _playerTime2 = dataPlayer.playerTime2;
            _playerTime3 = dataPlayer.playerTime3;
        }
        //Debug.Log("Game data loaded!");
    }

    public static void saveNewTime(TimeSpan time)
    {
        TimeSpan worseTime = time;
        int worseNumber = 0;

        if (worseTime != TimeSpan.Zero && (worseTime < _playerTime1 || _playerTime1 == TimeSpan.FromSeconds(-1) || _playerTime1 == TimeSpan.Zero))
        {
            worseNumber = 1;
            worseTime = _playerTime1;
        }

        if (worseTime != TimeSpan.Zero && (worseTime < _playerTime2 || _playerTime2 == TimeSpan.FromSeconds(-1) || _playerTime3 == TimeSpan.Zero))
        {
            worseNumber = 2;
            worseTime = _playerTime2;
        }

        if (worseTime != TimeSpan.Zero && (worseTime < _playerTime3 || _playerTime3 == TimeSpan.FromSeconds(-1) || _playerTime3 == TimeSpan.Zero))
        {
            worseNumber = 3;
        }

        //Debug.Log("---------Worse time---------");
        //Debug.Log("actual time " + time);
        //Debug.Log("worse number " + worseNumber);
        //Debug.Log("worse time " + worseTime);

        switch (worseNumber)
        {
            case 1:
                _playerTime1 = time;
                break;
            case 2:
                _playerTime2 = time;
                break;
            case 3:
                _playerTime3 = time;
                break;
        }
        
        //Debug.Log("-----------------------------------");
        //debugData();
        SavePlayerTimes();
        //debugData();
        //Debug.Log("-----------------------------------");
    }

    public static TimeSpan getBestTime(String levelName)
    {
        LoadPlayerTime(levelName);
        //Debug.Log("--------Best time--------");
        //debugData();
        //Debug.Log("-----------------------------------");
        TimeSpan bestTime = _playerTime1;

        if (_playerTime2 < bestTime)
        {
            bestTime = _playerTime2;
        }

        if (_playerTime3 < bestTime)
        {
            bestTime = _playerTime3;
        }

        //Debug.Log("best time :" + bestTime);
        //Debug.Log("-----------------------------------");
        return bestTime;
    }

    public static TimeSpan[] getTimes(String levelName)
    {
        LoadPlayerTime(levelName);
        return new[] {_playerTime1, _playerTime2, _playerTime3};
    }

    public static TimeSpan[] getMedalTimes(String levelName)
    {
        LoadMedalTime(levelName);
        return new[] {bronzeTime, silverTime, goldTime};
    }

    private static void debugData()
    {
        Debug.Log("Bronze time: " + bronzeTime);
        Debug.Log("Silver time: " + silverTime);
        Debug.Log("Gold time: " + goldTime);
        Debug.Log("Player time 1: " + _playerTime1);
        Debug.Log("Player time 2: " + _playerTime2);
        Debug.Log("Player time 3: " + _playerTime3);
    }
}

[Serializable]
class LevelData
{
    public TimeSpan bronzeTime;
    public TimeSpan silverTime;
    public TimeSpan goldTime;
}

[Serializable]
class PlayerData
{
    public TimeSpan playerTime1;
    public TimeSpan playerTime2;
    public TimeSpan playerTime3;
}