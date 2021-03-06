﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SavePlayer(Player player, string savedFile)
    {
        BinaryFormatter formatter = new BinaryFormatter(); //create the binaryformatter

        string path = Application.persistentDataPath + "/" + savedFile + ".sav"; // save de data in data directory addapted to the os, with the file name = to player.sav
        FileStream stream = new FileStream(path, FileMode.Create); // we create the file and we open it so we can write in it

        PlayerData data = new PlayerData(player,savedFile); // we pass the player data to the playerdatabase

        formatter.Serialize(stream, data);//we are going to write the formatted data to the file stream 

        stream.Close(); // we close the stream 
    }

    public static PlayerData LoadPlayer(string savedFile)
    {
        string path = Application.persistentDataPath + "/" + savedFile + ".sav"; // we are going to search the data where we saved it
        if (File.Exists(path)) // we look if the path exist
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open); // we want to open the file so we can read it 

            PlayerData data = formatter.Deserialize(stream) as PlayerData; // we format the data into playerdata
            stream.Close();

            return data;
        }
        else //if not
        {
            Debug.LogError("Path doesn't exist, means no data where saved.");
            SavePlayer(new Player(), savedFile); //create a new file
            Debug.Log("create new path to : " + savedFile);
            return LoadPlayer(savedFile); //load that new file
        }


    }

}
