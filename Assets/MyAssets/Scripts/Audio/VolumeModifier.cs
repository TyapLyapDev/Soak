using System;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeModifier
{
    private const float MinLevel = -80;
    private const float MaxLevel = 20;
    private const float MinValue = 0;
    private const float MaxValue = 1;

    private readonly AudioMixer _audioMixer;

    public VolumeModifier(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }

    public void SetLevel(string group, float value)
    {
        float level = ConvertVolumeToLevel(value);
        _audioMixer.SetFloat(group, level);
    }

    private float ConvertVolumeToLevel(float value)
    {
        if (value < MinValue || value > MaxValue)
            throw new ArgumentException("«начение за пределами допустимого диапазона");

        return value == 0 ? MinLevel : Mathf.Log10(value) * MaxLevel;
    }
}