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
    private static TimeSpan playerTime1 = TimeSpan.FromSeconds(-1);
    private static TimeSpan playerTime2 = TimeSpan.FromSeconds(-1);
    private static TimeSpan playerTime3 = TimeSpan.FromSeconds(-1);

    public void Start()
    {
        LoadMedalTime();
        LoadPlayerTime();
        //debugData();
    }

    public static void SavePlayerTimes()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file =
            File.Create(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Player.dat");

        LevelData data = new LevelData();
        data.playerTime1 = playerTime1;
        data.playerTime2 = playerTime2;
        data.playerTime3 = playerTime3;

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

        LevelData data = new LevelData();
        data.bronzeTime = bronzeTime;
        data.silverTime = silverTime;
        data.goldTime = goldTime;

        bf.Serialize(file, data);
        file.Close();
        //Debug.Log("Game data saved!");
    }

    public static void LoadPlayerTime()
    {
        if (File.Exists(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Player.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Player.dat",
                    FileMode.Open);

            LevelData data = (LevelData) bf.Deserialize(file);
            file.Close();
            playerTime1 = data.playerTime1;
            playerTime2 = data.playerTime2;
            playerTime3 = data.playerTime3;

            //debugData();

            //Debug.Log("Player time loaded!");
        }
        else
        {
            //Debug.Log("No game data !");
        }
    }

    public static void LoadMedalTime()
    {
        if (File.Exists(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Times.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_Times.dat",
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
            //Debug.Log("No game data !");
        }
    }

    public static void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                File.Open(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".dat",
                    FileMode.Open);

            LevelData data = (LevelData) bf.Deserialize(file);
            file.Close();
            bronzeTime = data.bronzeTime;
            silverTime = data.silverTime;
            goldTime = data.goldTime;
            playerTime1 = data.playerTime1;
            playerTime2 = data.playerTime2;
            playerTime3 = data.playerTime3;

            //Debug.Log("Game data loaded!");
        }
        else
        {
            //Debug.Log("No game data !");
        }
    }

    public static void saveNewTime(TimeSpan time)
    {
        TimeSpan worseTime = time;
        int worseNumber = 0;

        if (worseTime < playerTime1)
        {
            worseNumber = 1;
            worseTime = playerTime1;
        }

        if (worseTime < playerTime2)
        {
            worseNumber = 2;
            worseTime = playerTime2;
        }

        if (worseTime < playerTime3)
        {
            worseNumber = 3;
            worseTime = playerTime3;
        }

        //Debug.Log("---------Worse time---------");
        //Debug.Log("actual time " + time);
        //Debug.Log("worse number " + worseNumber);
        //Debug.Log("worse time " + worseTime);

        switch (worseNumber)
        {
            case 1:
                playerTime1 = time;
                break;
            case 2:
                playerTime2 = time;
                break;
            case 3:
                playerTime3 = time;
                break;
        }
        

        //Debug.Log("-----------------------------------");
        //debugData();
        SavePlayerTimes();
        //debugData();
        //Debug.Log("-----------------------------------");
    }

    public static TimeSpan getBestTime()
    {
        LoadPlayerTime();
        //Debug.Log("--------Best time--------");
        //debugData();
        //Debug.Log("-----------------------------------");
        TimeSpan bestTime = playerTime1;

        if (playerTime2 < bestTime)
        {
            bestTime = playerTime2;
        }

        if (playerTime3 < bestTime)
        {
            bestTime = playerTime3;
        }

        //Debug.Log("best time :" + bestTime);
        //Debug.Log("-----------------------------------");
        return bestTime;
    }

    public static TimeSpan[] getTimes()
    {
        LoadPlayerTime();
        return new[] {playerTime1, playerTime2, playerTime3};
    }

    public static TimeSpan[] getMedalTimes()
    {
        LoadMedalTime();
        return new[] {bronzeTime, silverTime, goldTime};
    }

    private static void debugData()
    {
        Debug.Log("Bronze time: " + bronzeTime);
        Debug.Log("Silver time: " + silverTime);
        Debug.Log("Gold time: " + goldTime);
        Debug.Log("Player time 1: " + playerTime1);
        Debug.Log("Player time 2: " + playerTime2);
        Debug.Log("Player time 3: " + playerTime3);
    }
}

[Serializable]
class LevelData
{
    public TimeSpan bronzeTime;
    public TimeSpan silverTime;
    public TimeSpan goldTime;
    public TimeSpan playerTime1;
    public TimeSpan playerTime2;
    public TimeSpan playerTime3;
}