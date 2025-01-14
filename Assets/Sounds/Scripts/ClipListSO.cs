﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClipListSO")]
public class ClipListSO : ScriptableObject
{
    [Header("Музыка")]
    public List<AudioClip> clips;

    [Header("Параметры")]
    public bool shuffleClip;

    int _i;
    private void Awake()
    {
        if (shuffleClip) Shuffle();
    }

    public AudioClip NextClip(ref float clipLength)
    {
        AudioClip temp = NextClip();
        clipLength = temp.length;
        return temp;
    }

    public AudioClip NextClip()
    {
        _i++;
        Ind(clips, ref _i, shuffle: true);
        return clips[_i];
    }

    public void Shuffle()
    {
        _i = 0;
        Shuffle(clips);
    }

    #region Help code 
    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    /// <summary>
    /// Определяет элемент от любого числа без IndexOutRange
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    T Ind<T>(List<T> list, ref int index, bool shuffle = false)
    {
        if (shuffle && index >= list.Count) Shuffle(list);

        index = Mathf.Abs(index) % list.Count;
        return list[index];
    }
    #endregion
}
