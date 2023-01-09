using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;
using UnityEngine.UIElements;
 

public class Buttons : MonoBehaviour
{
    private Button button;
    public GameObject button_object;
    private AudioSource button_audio;
    public AudioClip button_hover_sound;
    public AudioClip button_click_sound;
    private bool button_clicked = false;
    private bool button_hover_sound_canplay = false;

    private void Start()
    {
        button_audio = GetComponent<AudioSource>();
        button = button_object.GetComponent<Button>(); 
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (!button_audio.isPlaying)
        {
            button_audio.clip = null;
        }

        if (button_audio.isPlaying && button_audio.clip == button_click_sound)
        {
            button_hover_sound_canplay = false; 
        }
        else
        {
            button_hover_sound_canplay = true;
        }


    }

    public void ButtonHover()
    {
        if (button_hover_sound_canplay)
        {
            button_audio.clip = null;
            button_audio.clip = button_hover_sound;
            button_audio.Play();
        }


    }
    public void ButtonClick()
    {
        button_audio.clip = null;
        button_audio.clip = button_click_sound;
        button_audio.Play();

    }
}
