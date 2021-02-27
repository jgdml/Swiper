using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip eventHit;
    public AudioClip eventWin;
    public AudioClip eventBreak;
    public AudioClip[] press;
    public AudioClip[] songs;


    public AudioSource music;
    public AudioSource sfx;

    public float musicVolume = 0.3f;
    public float volume = 0.3f;

    bool normal = true;
    float maxPitch = 1f;


    void Start(){

        music.volume = musicVolume;
        sfx.volume = volume;

        music.pitch = 0.5f;
        music.clip = songs[Random.Range(0, songs.Length)];
        music.Play();
    }


    public void changeVolume(float vol){
        music.volume += vol;
    }


    public void pauseMusic(){
        music.Pause();
    
    }
    public void resumeMusic(){
        music.Play();
    }

    public void speedUpMusic(float percentage){
        maxPitch += percentage;
    }


    public void playClip(string clip){
        if (clip == "hit"){
            sfx.PlayOneShot(eventHit, volume);
        }

        else if (clip == "press"){
            sfx.PlayOneShot(press[Random.Range(0, press.Length)], volume);
        }

        else if (clip == "break"){
            sfx.PlayOneShot(eventBreak, volume);
        }
        
        else if (clip == "win"){
            sfx.PlayOneShot(eventWin, volume);
        }


    }


    public void stopAudio(){
        normal = false;
    }


    void Update(){

        if (normal){
            float rate = music.pitch + (0.6f/0.99f)*Time.unscaledDeltaTime;
            music.pitch = Mathf.Clamp(rate, 0f, maxPitch);
            
        }
        else{
            float rate = music.pitch - (0.6f/0.99f)*Time.unscaledDeltaTime;
            music.pitch = Mathf.Clamp(rate, 0f, maxPitch);
        }
        
    }

}
