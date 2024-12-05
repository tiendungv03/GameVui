using TMPro;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{
    public TextMeshProUGUI waveText;
    public void SetWaveText(int waveIndex)
    {
        waveText.text = "Wave: " + waveIndex;
    }
}
