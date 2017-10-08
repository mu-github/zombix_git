﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class OrbManager : MonoBehaviour
{


    //オブジェクト参照
    private GameObject gameManager; // ゲームマネージャー

    public Sprite[] orbPicture = new Sprite[3]; //目玉の絵

    public enum ORB_KIND
    {   // 目玉の種類を定義
        BLUE,
        GREEN,
        PURPLE,
    }

    private ORB_KIND orbKind; //目玉の種類

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
    }

    //目玉取得
    public void TouchOrb()
    {
        if (Input.GetMouseButton(0) == false)
        {
            return;
        }

        switch (orbKind)
        {
            case ORB_KIND.BLUE:
                gameManager.GetComponent<GameManager>().GetOrb(1);
                break;
            case ORB_KIND.GREEN:
                gameManager.GetComponent<GameManager>().GetOrb(5);
                break;
            case ORB_KIND.PURPLE:
                gameManager.GetComponent<GameManager>().GetOrb(10);
                break;
        }

        Destroy(this.gameObject);

    }

    //目玉の種類を設定
    public void SetKind(ORB_KIND kind)
    {
        orbKind = kind;

        switch (orbKind)
        {
            case ORB_KIND.BLUE:
                GetComponent<Image>().sprite = orbPicture[0];
                break;
            case ORB_KIND.GREEN:
                GetComponent<Image>().sprite = orbPicture[1];
                break;
            case ORB_KIND.PURPLE:
                GetComponent<Image>().sprite = orbPicture[2];
                break;
        }
    }

}