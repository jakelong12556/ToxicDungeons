using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONHandler : MonoBehaviour
{
    public TextAsset textJSON;

    private Setting mySetting;

    void Start()
    {
        mySetting = JsonUtility.FromJson<Setting>(textJSON.text);
        //mySetting = new Setting
        //{
        //    volume = 0.5f
        //};
    }
    public void OutputJSON(Setting newSetting)
    {

        string strOut = JsonUtility.ToJson(newSetting);

        File.WriteAllText(Application.dataPath + "/settings.txt", strOut);
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


