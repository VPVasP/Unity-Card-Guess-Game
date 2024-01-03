using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteUnmuteButton : MonoBehaviour
{
    public Image muteIcon;
    public Image unMuteIcon;
    public AudioSource music;
    private bool isMuted;
    private void Start()
    {
        unMuteIcon.gameObject.SetActive(false);
    }
    public void MuteUnmute()
    {
        isMuted = !isMuted;
        music.mute = isMuted;
        if (isMuted)
        {
            muteIcon.gameObject.SetActive(true);
            unMuteIcon.gameObject.SetActive(false);
        }
        else
        {
            muteIcon.gameObject.SetActive(false);
            unMuteIcon.gameObject.SetActive(true);
        }
    }

}
