using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class TitleButton : MonoBehaviour {

    //ゲーム画面へ
    public void ChangeSceneGame(){

        SceneManager.LoadScene("GameScene");

    }

    //タイトルへ
    public void ChangeSceneTitle()
    {

        SceneManager.LoadScene("TitleScene");

    }

    //図鑑へ
    public void ChangeSceneZukan(){

        SceneManager.LoadScene("Zukan");

    }


    // Update is called once per frame
    void Update () {
		
	}
}
