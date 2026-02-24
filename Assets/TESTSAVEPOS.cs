using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using static TESTSAVEPOS;

public class TESTSAVEPOS : MonoBehaviour
{
    [SerializeField] GameObject _player;

    public class PlayerData
    {
        public float x;
        public float y;
        public float z;
    }

    private void SavePosition()
    {
        float posx = _player.transform.position.x;
        float posy = _player.transform.position.y;
        float posz = _player.transform.position.z;

        PlayerData Playerdat = new PlayerData
        {
            x = posx,
            y = posy,
            z = posz
        };

        string jsonText = JsonConvert.SerializeObject(Playerdat,Formatting.Indented);
        string saveFile = Application.persistentDataPath + "/playerdata.json";
        File.WriteAllText(saveFile, jsonText);

        Debug.Log("Ho salvato " + saveFile);

    }

    private void LoadPosition()
    {
        string saveFile = Application.persistentDataPath + "/playerdata.json";
        var jsontext = File.ReadAllText(saveFile);
        var playerdata = JsonConvert.DeserializeObject<PlayerData>(jsontext);

        Vector3 Position = new Vector3(playerdata.x, playerdata.y, playerdata.z);

        _player.transform.position = Position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SavePosition();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPosition();
        }
    }
}
