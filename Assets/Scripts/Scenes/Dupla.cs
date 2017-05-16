using UnityEngine;
using System.Collections;

public class Dupla
{


    private string type;
    private AudioClip sound;

    public string Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }
    public AudioClip Sound
    {
        get
        {
            return sound;
        }
        set
        {
            sound = value;
        }
    }

    public Dupla(string s, AudioClip a)
    {
        type = s;
        sound = a;
    }

}
