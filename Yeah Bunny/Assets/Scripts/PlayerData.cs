using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class PlayerData : MonoBehaviour
{
    public void Start()
    {
        
        /*Debug.Log("Show Level 3 info");
        for (int i = 0; i < PlayerDataInfo.listPlayerInput.Count; i++)
        {
            if (PlayerDataInfo.listPlayerInput[i] != null)
            {
                if (PlayerDataInfo.listPlayerInput[i].id == 3)
                {
                    Debug.Log("id: " + PlayerDataInfo.listPlayerInput[i].id + ",\nmoveSpeed: " + PlayerDataInfo.listPlayerInput[i].moveSpeed + ",\njumpForce: " + PlayerDataInfo.listPlayerInput[i].jumpForce + ",\naddJumpSpeed: " + PlayerDataInfo.listPlayerInput[i].addJumpSpeed);
                    break;
                }
            }
        }

        foreach(var info in PlayerDataInfo.listPlayerInput)
        {
            if(info.id == 3)
            {
                //...
                break;
            }
        }

        var dataInf = PlayerDataInfo.listPlayerInput.FirstOrDefault(inf => inf.id == 3);
        if(dataInf != null)
        {
            //...
        }

        var result = from dInfo in PlayerDataInfo.listPlayerInput
                     where dInfo.id <= 3
                     select dInfo;

        result.ToList().ForEach(x => Debug.Log("id: " + x.id + ",\nmoveSpeed: " + x.moveSpeed + ",\njumpForce: " + x.jumpForce + ",\naddJumpSpeed: " + x.addJumpSpeed));
    }*/
    }
    public PlayerInfo SetPlayerData(int id, float moveSpeed, float jumpForce, float addJumpSpeed)
    {
        PlayerInfo info = new PlayerInfo();
        info.id = id;
        info.moveSpeed = moveSpeed;
        info.jumpForce = jumpForce;
        info.addJumpSpeed = addJumpSpeed;
        return info;
    }
}


public class PlayerInfo
{
    public int id;
    public float moveSpeed;
    public float jumpForce;
    public float addJumpSpeed;
}

public static class PlayerDataInfo
{
    public static List<PlayerInfo> listPlayerInput = new List<PlayerInfo>();
}

/*public class PlayerData : MonoBehaviour
{
    private void Start()
    {
        //List<PlayerInfo> lstPlayerData = new List<PlayerInfo>();
        //List<PlayerInfo> lstPlayerInput = new List<PlayerInfo>();
        //for(int i = 0; i < 10; i++)
        //{
        //    PlayerInfo info = new PlayerInfo();
        //    info = SetPlayerData(i, 20 * i, 30 * i, 40 * i);
        //    lstPlayerData.Add(info);
        //}
        //string jsonOutput = JsonConvert.SerializeObject(lstPlayerData);
        //Debug.Log(jsonOutput);

        //lstPlayerInput = JsonConvert.DeserializeObject<List<PlayerInfo>>(jsonOutput);
        //lstPlayerInput.ForEach(x =>
        //{
        //    Debug.Log("id= " + x.id + ", speed= " + x.speed + ", jumpSpeed= " + x.jumpSpeed + ", addJumpSpeed= " + x.addJumpSpeed);5
        //});
        string readValue = "";

        TextAsset jsonText = Resources.Load<TextAsset>("PlayerData");
        readValue = jsonText.text;

        List<PlayerInfo> lstPlayerInput = new List<PlayerInfo>();
        lstPlayerInput = JsonConvert.DeserializeObject<List<PlayerInfo>>(readValue);
        lstPlayerInput.ForEach(x =>
        {
            Debug.Log("id= " + x.id + ", speed= " + x.speed + ", jumpSpeed= " + x.jumpSpeed + ", addJumpSpeed= " + x.addJumpSpeed);
        });
    }

    public PlayerInfo SetPlayerData(int id, float speed, float jumpSpeed, float addJumpSpeed)
    {
        PlayerInfo info = new PlayerInfo();
        info.id = id;
        info.speed = speed;
        info.jumpSpeed = jumpSpeed;
        info.addJumpSpeed = addJumpSpeed;

        return info;
    }
}

public class PlayerInfo
{
    public int id;
    public float speed;
    public float jumpSpeed;
    public float addJumpSpeed;
}

public class HandleTextFile
{
    public static void WriteFile()
    {

    }

    public static string ReadString()
    {
        string path = "Assets/Resources/PlayerData.json";
        string readValue = "";

        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        readValue = reader.ReadToEnd();
        reader.Close();
        return readValue;
    }
}
*/