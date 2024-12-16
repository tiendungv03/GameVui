using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; set; }
    public AudioSource shootingSound;
    public AudioSource reloadingSound;
    public AudioSource emptyManagizeSound;
   
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
