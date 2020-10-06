using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public static float[] spectrumValue { get; private set; } = new float[12];

    private float[] _AudioSpecturm;

    private int _SpectrumChunk;

    /// <summary>
    /// the sample rate used for this AudioSpectrum. 
    /// </summary>
    [SerializeField] public int SampleRate = 128;

    // Start is called before the first frame update
    void Start()
    {
        _AudioSpecturm = new float[SampleRate];

        _SpectrumChunk = SampleRate / spectrumValue.Length;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.GetSpectrumData(_AudioSpecturm, 0, FFTWindow.Hamming);

        if (_AudioSpecturm != null && _AudioSpecturm.Length > 0)
        {

            for (int i = 0; i <spectrumValue.Length; i++)
            {
                float samplerange = 0;

                for (int j = 0; j < _SpectrumChunk; j++)
                {
                    samplerange += _AudioSpecturm[(i * _SpectrumChunk) + j];
                }

                spectrumValue[i] = samplerange * 100;
            }
        }

    }
}
