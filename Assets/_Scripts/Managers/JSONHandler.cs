using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONHandler : MonoBehaviour
{
    private Setting mySetting;

    void Start()
    {
        readJSON();
    }

    //Used to read and update json after new settings
    public void readJSON()
    {
        if (System.IO.File.Exists(Application.dataPath + "/settings.txt"))
        {
            var file = File.ReadAllLines(Application.dataPath + "/settings.txt");
            var fileWord = new List<string>(file);
            mySetting = JsonUtility.FromJson<Setting>(fileWord[0]);
        }
    }
    public void OutputJSON(Setting newSetting)
    {

        string strOut = JsonUtility.ToJson(newSetting);

        File.WriteAllText(Application.dataPath + "/settings.txt", strOut);
        readJSON();
    }

    public float getVolume()
    {
        return mySetting.volume;
    }


}


public class Setting
{
    public float volume;
}


