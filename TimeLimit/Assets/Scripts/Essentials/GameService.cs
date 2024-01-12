using Game;
using Game.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UI;

public class GameService:MonoSingletonGeneric<GameService>
{
    [Header("Player Service")]
    public Transform StartPos;
    public PlayerSO PlayerSO;
    [Header("Level Service")]
    public String[] Levels;

    [Header("SoundService")]
    public AudioSource SoundEffect;
    public AudioSource SoundMusic;
    [Range(0f, 1f)] public float userVolume;
    public SoundType[] Soundtypes;

    public SoundManager SoundManager { get; private set; }
    public LevelManeger LevelManeger { get; private set; }
    public PlayerService PlayerService { get; private set; }
    private void Start()
    {

        SoundManager = new SoundManager(SoundEffect, SoundMusic, Soundtypes, userVolume);
        LevelManeger = new LevelManeger(Levels);
        PlayerService = new PlayerService(StartPos, PlayerSO);

    }



}
