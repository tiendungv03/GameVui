using TMPro;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public void SetWaveText(int waveIndex)
    {
        if (waveText != null)
            waveText.text = "Wave: " + waveIndex;
    }
}
