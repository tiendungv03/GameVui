using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private double time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TimeUI();
    }

    public void SetTime(int time)
    {
        this.time = time;
    }

    public void TimeUI()
    {
        if (timeText == null)
            return;
        if (time > 0)
            time -= Time.deltaTime;
        timeText.text = "Time: " + Mathf.Round((float)time);
    }

    public double GetTime() => time;
}
