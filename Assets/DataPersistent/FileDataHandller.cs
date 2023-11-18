using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandller
{
    string dirPath;
    string fileName;

    public FileDataHandller (string dirPath, string fileName)
    {
        this.dirPath = dirPath;
        this.fileName = fileName;
    }

    public void SaveData(GameData gameData)
    {
        string fullpath = Path.Combine(dirPath, fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName (fullpath));
            string dataToSave = JsonUtility.ToJson(gameData, true);

            using (FileStream stream = new FileStream(fullpath,FileMode.Create))
            {
                using (StreamWriter steam = new StreamWriter(stream))
                {
                    steam.Write(dataToSave);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public GameData Load()
    {
        string fullpath = Path.Combine (dirPath, fileName);
        GameData loadedData = null;
        if(File.Exists(fullpath))
        {
            try
            {
                string dataToLoad ="";
                using (FileStream stream = new FileStream(fullpath,FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        

        return loadedData;
     }
}
