using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TempleManager : MonoBehaviour {

	public Sprite[] templePicture = new Sprite[3];	//寺の絵

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 寺の絵を設定
	public void SetTemplePicture (int level) {
		GetComponent<Image> ().sprite = templePicture [level];
	}

	// 寺の大きさ設定
	public void SetTempleScale (int score, int nextScore) {
		float scale = 0.5f + (((float)score / (float)nextScore) / 2.0f);
		transform.localScale = new Vector3 (scale, scale, 1.0f);
	}
}
