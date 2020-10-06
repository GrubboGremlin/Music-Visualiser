using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Audio syncer parent class designed to be extened to allow for an audio range to be designated to an object to manipulate that object in some way once the audio ranfe is "hit".
/// </summary>
public class AudioSyncer : MonoBehaviour
{
    /// <summary>
    /// Bias value this designates what area of the specturm this class currently operates within. 
    /// </summary>
    [SerializeField] public float Bias;

    /// <summary>
    /// Time span for the minimum amount fo tme required between beat hits. 
    /// </summary>
    [SerializeField] public float MinimumBeatSpacer;

    /// <summary>
    /// Timespan of the beat visualsation. 
    /// </summary>
    [SerializeField] public float TimeToBeat;

    /// <summary>
    /// Speed/time it takes the object to return to rest after a beat hit. 
    /// </summary>
    [SerializeField] public float RestTime;


    /// <summary>
    /// Index value relating to the harze range of specturm information this is synced too. 
    /// </summary>
    [SerializeField] public int HrzChunkIndex;

    /// <summary>
    /// Previous audio value for this syncer.
    /// </summary>
    private float _PreviousAudioValue;

    /// <summary>
    /// Current Audio value for this syncer. 
    /// </summary>
    private float _AudioValue;

    /// <summary>
    /// Current amount of time since the last beat hit. 
    /// </summary>
    private float _Timer;

    /// <summary>
    /// if the beat has been hit <code>true</code>. else <code>false</code>
    /// </summary>
    protected bool _IsBeat;
   
    /// <summary>
    /// method fired when the bias value is surpassed and a "beat" has been triggered. 
    /// </summary>
    public virtual void OnBeat() 
    {
        Debug.Log("beat hit.");
        _Timer = 0;
        _IsBeat = true;

    }

    /// <summary>
    /// sub-update method used to provide general update logic. 
    /// </summary>
    public virtual void OnUpdate()
    {
        _PreviousAudioValue = _AudioValue;
        _AudioValue = AudioSpectrum.spectrumValue[HrzChunkIndex];

        if (_PreviousAudioValue > Bias && _AudioValue <= Bias) 
        {
            if (_Timer > MinimumBeatSpacer)
                OnBeat();
        }

        if (_PreviousAudioValue <= Bias && _AudioValue > Bias) 
        {
            if (_Timer > MinimumBeatSpacer)
                OnBeat();
        }

        _Timer += Time.deltaTime;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }
}
