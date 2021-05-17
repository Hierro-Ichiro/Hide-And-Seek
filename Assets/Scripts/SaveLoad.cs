using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

namespace HideAndSeek
{
    public static class SaveLoad //класс для сохранения и загрузки данных, метод статичный, поэтому всегда есть доступ
    {
        public static int levelsCompleted;

        public static void Save()
        {
            BinaryFormatter bf = new BinaryFormatter(); //используется более безопасная система сохранения
            FileStream file = File.Create(Application.persistentDataPath + "/savedGame.gd");
            SaveData data = new SaveData();
            data.levelsCompleted = levelsCompleted;
            bf.Serialize(file, data);
            file.Close();
        }

        public static void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/savedGame.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);
                SaveData data = (SaveData)bf.Deserialize(file);
                levelsCompleted = data.levelsCompleted;
                file.Close();
            }
        }
    }

    [Serializable]
    class SaveData //можно записывать разные виды данных
    {
        public int levelsCompleted;
    }
}
