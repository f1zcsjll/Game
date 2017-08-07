using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class GameEnter : MonoBehaviour {

	void Start () {
        FrameMgr.GetInstance();
        TimerMgr.GetInstance();
        PathMgr.GetInstance();
        ConfigMgr.GetInstance();
        AssetMgr.GetInstance();
        UIMgr.GetInstance();
        SoundMgr.GetInstance();
        LoadSceneMgr.LoadSence("test");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
