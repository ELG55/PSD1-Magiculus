using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile 
{

    //Save variables
    public int slot;
    public string mageName;
    public int level;
    public string mageClass;
    public string date;
    public string progress;
    public float dmgDone;
    public float dmgReceived;
    public float hitP;
    public float percentSum;
    public float percentTimes;

    public SaveFile(int slot1, string mageName1, int level1, string mageClass1, string date1, string progress1, float dmgDone1, float dmgReceived1, float hitP1, float percentSum1,float percentTimes1)
    {
        slot=slot1;
        mageName=mageName1;
        level= level1;
        mageClass= mageClass1;
        date=date1;
        progress=progress1;
        dmgDone=dmgDone1;
        dmgReceived=dmgReceived1;
        hitP=hitP1;
        percentSum = percentSum1;
        percentTimes = percentTimes1;
    }
}
