using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMasterVol(float masterVol)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(masterVol) * 20);
    }
    public void setMusicVol(float musicVol)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(musicVol) * 20);
    }
    public void setSFXVol(float sfxVol)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxVol) * 20);
    }
}
