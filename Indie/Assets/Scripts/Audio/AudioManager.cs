using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {
    
    public Sound[] sonidos;
    public int pitchMaximo, pitchMinimo;
    System.Random rnd= new System.Random();
    // Use this for initialization
    /*Por cada sonido del array le ponemo: volumen,loop,clip...  (Ajustado en la clase sound)*/
    void Awake()
    {
        foreach(Sound s in sonidos)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
	void Start () {
        GameManager.instance.AvisoAudioManager(GetComponent<AudioManager>());
        AudioListener.volume = GameManager.instance.DameVolumen();
        Play("Ascensor");
        Play("Liszt");
        Play("GenteHablando");
	}
	
	

    /*Se busca en el array por el sonido "name" y si o encuentra le da a play
     si no lo encuentra no ocurre nada*/
    public void Play (string name)
    {
        Sound s=Array.Find(sonidos, sound => sound.name == name);

        if (s != null)
            s.source.Play();
        else
            Debug.Log("No encontrado");            
    }
    public void PlayRandomPitch(string name)
    {        
        Sound s = Array.Find(sonidos, sound => sound.name == name);
        if (s != null)
        {
            s.pitch = (float)rnd.Next(pitchMinimo, pitchMaximo) / 10;
            s.source.pitch = s.pitch;

            s.source.Play();
           // s.pitch = 1;
        }
    }
}
