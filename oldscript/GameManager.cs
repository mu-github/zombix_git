using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;


public class GameManager : MonoBehaviour {
    //定数定義
    private const int MAX_ORB = 10; //目玉最大数
    private const int RESPAWN_TIME = 5; //目玉が発生する秒数
    private const int MAX_LEVEL = 2; //お寺最大レベル

    //オブジェクト参照
    public GameObject orbPrefab; //目玉プレハブ
    public GameObject smokePrefab; //煙プレハブ
    public GameObject kusudamaPrefab; //くす玉プレハブ
    public GameObject canvasGame; //ゲームキャンバス
    public GameObject textScore; //スコアテキスト
    public GameObject imageTemple; //お寺

    //メンバ変数
    private int score = 0; // 現在のスコア
    private int nextScore = 10; //レベルアップまで必要なスコア

    private int currentOrb = 0; //現在の目玉数

    private int templeLevel = 0; //寺のレベル

    private DateTime lastDateTime; //前回目玉を生成した時間

    private int[] nextScoreTable = new int[] {10, 100, 1000}; //レベルアップ数

    // Use this for initialization
    void Start()
    {
        currentOrb = 10;
        //初期目玉生成
        for (int i = 0; i < currentOrb; i++)
        {
            CreateOrb();
        }

        //初期設定
        lastDateTime = DateTime.UtcNow;
        nextScore = nextScoreTable[templeLevel];
        imageTemple.GetComponent<TempleManager>().SetTemplePicture(templeLevel);
        imageTemple.GetComponent<TempleManager>().SetTempleScale(score, nextScore);

        RefreshScoreText();

    }

    //寺のレベル管理
    void TempleLevelUp() {
        if (score >= nextScore) {
            if (templeLevel < MAX_LEVEL) {
                templeLevel++;
                score = 0;

                TempleLevelUpEffect();

                nextScore = nextScoreTable[templeLevel];
                imageTemple.GetComponent<TempleManager>().SetTemplePicture
                (templeLevel);
            }
        }
    }


    // Update is called once per frame
    void Update() {
        if (currentOrb < MAX_ORB) {
            TimeSpan timeSpan = DateTime.UtcNow - lastDateTime;

            if (timeSpan >= TimeSpan.FromSeconds(RESPAWN_TIME)) {
                while (timeSpan >= TimeSpan.FromSeconds(RESPAWN_TIME)) {
                    CreateNewOrb();
                    timeSpan -= TimeSpan.FromSeconds(RESPAWN_TIME);
                }
            }

        }

    }

    //新しい目玉の生成
    public void CreateNewOrb() {
        lastDateTime = DateTime.UtcNow;
        if (currentOrb >= MAX_ORB) {
            return;
        }
        CreateOrb();
        currentOrb++;

    }

    //目玉生成
    public void CreateOrb() {
        GameObject orb = (GameObject)Instantiate (orbPrefab);
        orb.transform.SetParent(canvasGame.transform, false);
        orb.transform.localPosition = new Vector3(
            UnityEngine.Random.Range(-300.0f, 300.0f),
            UnityEngine.Random.Range(-140.0f, -500.0f),
            0f);

    }

    // 目玉の種類を設定
    int kind = UnityEngine.Random.Range(0, templeLevel + 1);
		switch (kind) {
		case 0:
			orb.GetComponent<OrbManager>().SetKind(OrbManager.ORB_KIND.BLUE);
			break;
		case 1:
			orb.GetComponent<OrbManager>().SetKind(OrbManager.ORB_KIND.GREEN);
			break;
		case 2:
			orb.GetComponent<OrbManager>().SetKind(OrbManager.ORB_KIND.PURPLE);
			break;
		}
	}


//目玉入手
public void GetOrb (int getScore) {
    if (Score < nextScore) { 
        Score += getScore;

        //レベルアップ値を超えないよう制限
        if (score > nextScore){
            score = nextScore;
        }

        TempleLevelUp();
        RefreshScoreText();
        imageTemple.GetComponent<TempleManager>().SetTempleScale(score,
            nextScore);

        //ゲームクリア判定
        if ((score == nextScore) && (templeLevel == MAX_LEVEL))
        {
            ClearEffect();
        }
    }

        currentOrb--;
    }

    //スコアテキスト更新
    void RefreshScoreText () {
        textScore.GetComponent<Text>().text =
            "徳" + score + "/" + nextScore;


    }
           
}


//レベルアップ時の演出
    void TempleLevelUpEffect()
{
    GameObject smoke = (GameObject)Instantiate(smokePrefab);
    smoke.transform.SetParent(canvasGame.transform, false);
    smoke.transform.SetSiblingIndex(2);

    Destroy(smoke, 0.5f);
}

//寺が最後までそろった時の演出
void ClearEffect()
{
    GameObject kusudama = (GameObject)Instantiate(kusudamaPrefab);
    kusudama.transform.SetParent(canvasGame.transform, false);
}