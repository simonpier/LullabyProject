using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume_SP : MonoBehaviour
{
 
    public void SetListener (float sliderValue)
    {
        AudioListener.volume = sliderValue;
    }

}
