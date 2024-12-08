using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWave
{
    private List<Wave> waveList;
    private ManageData manageData;
    private string dbPath;

    public ManageWave(string dbPath)
    {
        this.dbPath = dbPath;
        manageData = new ManageData(dbPath);
        waveList = new List<Wave>();
    }
    public void GetWaves()
    {
        manageData.OpenConnect();
        waveList = manageData.GetDataFromTable("Waves", reader =>
        {
            Wave wave = new Wave();
            wave.AddDataWave(reader);
            return wave;
        });
        manageData.CloseConnect();
    }

    public void ShowWaves()
    {
        foreach (Wave wave in waveList)
            Debug.Log(wave);
    }

    public List<Wave> GetListWave() => waveList;
}
