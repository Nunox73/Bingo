using UnityEngine;

public class PlaySoundRuntime : MonoBehaviour
{
  [Header("Sounds")]
     public AudioSource source;      // podes deixar vazio e ele procura
    public AudioClip clickClip;     // arrasta no Inspector (ou carrega por Resources) 
    public AudioClip CorrectNumber;
    public AudioClip WrongNumber;
    void Awake()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
    }

    public void PlayCorrectNumber()
    {
        if (source == null || CorrectNumber == null) return;

        source.clip = CorrectNumber;  // assign em runtime
        source.Play();
    }
}
