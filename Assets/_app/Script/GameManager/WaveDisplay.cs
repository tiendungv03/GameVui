using TMPro;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{
    public int waveIndex;
    public TextMeshProUGUI waveText;
    public void SetWaveText(int waveIndex)
    {
        this.waveIndex = waveIndex;
        if (waveText != null)
            waveText.text = "Wave: " + this.waveIndex;
    }
}
