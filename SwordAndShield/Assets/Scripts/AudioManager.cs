using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public static AudioClip swap, dash, impact, swing, parry, footstep, youwin;

    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}