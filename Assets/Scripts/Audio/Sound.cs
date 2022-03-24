
using UnityEngine;
using UnityEngine.Audio;


//Serializable para que se pueda ver en el inspector todo aquello relacionado con esta clase
[System.Serializable]


public class Sound  {

    public string name;           
    public AudioClip clip;
    [Range(0f,1f)]  //Range hace que en vez de poner un numero en el inspector tenemos un slider
    public float volume;
    [Range(.1f,1f)]
    public float pitch;


    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
