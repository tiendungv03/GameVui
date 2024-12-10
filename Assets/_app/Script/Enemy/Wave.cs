using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Wave
{
    private int waveID;
    private int waveNumber;
    private int duration;

    //add data wave from database
    public void AddDataWave(IDataReader readWave)
    {
        waveID = Convert.ToInt32(readWave["waveID"]);
        waveNumber = Convert.ToInt32(readWave["waveNumber"]);
        duration = Convert.ToInt32(readWave["duration"]);
    }

    public int GetWaveID() => waveID;
    public int GetWaveNumber() => waveNumber;
    public int GetDuration() => duration;

    public override string ToString()
    {
        return "waveID: " + waveID +
                ", waveNumber: " + waveNumber +
                ", duration: " + duration;
    }
}
