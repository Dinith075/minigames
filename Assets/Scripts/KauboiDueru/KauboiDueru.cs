﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KauboiDueru : IMiniGame
{
    private GameManager gameManager;

    /*
     * Player can shoot only in a concret moment, if he tries to shoot in other moment, he loses.
     */
    [Header("Game State")]
    public bool playerCanShoot=false;
    //Enemy shoots!
    public bool enemyShoots = false;
    //Generated by random
    public float minShootTime;
    public float maxShootTime;
    //If player dont shoots, enemy shoots
    public float enemyShootCounter;

    [Header("Game Time")]
    public int gameTime = 10;
    private bool gameSpawnStop = false;
    public Text gameWinText;

    void Start()
    {
        //
    }
    public override void beginGame()
    {
        //KauboiDueru Begins
        Debug.Log(this.ToString() + " game Begin");
        StartCoroutine(gameTimer());
        StartCoroutine(shootTime());
    }

    public override void initGame(MiniGameDificulty difficulty, GameManager gm)
    {
        this.gameManager = gm;
    }

    public override string ToString()
    {
        return "Kauboi Dueru by Saltimbanqi";
    }

    private void Update()
    {

    }

    public void setEndGame()
    {
        StopAllCoroutines();
        gameManager.EndGame(MiniGameResult.LOSE);
    }
    
    void setEndGameWin()
    {
        StopAllCoroutines();
        gameManager.EndGame(MiniGameResult.WIN);
    }
    
    public IEnumerator gameTimer()
    {
        for(int i=gameTime;i>-1;--i)
        {
            yield return new WaitForSeconds(1f);
            if(i==0)
                gameSpawnStop = true;
            
        }
    }

    public bool getEndTime()
    {
        return gameSpawnStop;
    }

    public void setEndTime(bool _value)
    {
        gameSpawnStop = _value;
    }
    public IEnumerator endGame()
    {
        gameWinText.enabled = true;
        yield return new WaitForSecondsRealtime(2f);
        setEndGameWin();
    }

    public IEnumerator shootTime()
    {
        for(int i=0;i<3;++i)
        {
            yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(minShootTime,maxShootTime));
            Debug.Log(i + 1);
        }
        Debug.Log("Player shoots now!!");
        playerCanShoot = true;
        yield return new WaitForSecondsRealtime(enemyShootCounter);
        Debug.Log("Player loses!");
        playerCanShoot = false;
        enemyShoots = true;
        Debug.LogError("BANG!");
        Debug.Break();
        setEndGame();
    }
}
