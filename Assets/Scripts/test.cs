
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnGUI()
    {
        if(GUILayout.Button("同步加载"))
        {
            AssetMgr.GetInstance().LoadAsset("Cube",false,false,()=>{
                Object temp = AssetMgr.GetInstance().GetAsset();
                Instantiate(temp);
            });
        }
        if (GUILayout.Button("异步加载"))
        {
            AssetMgr.GetInstance().LoadAsset("Cube", true, false, () =>
            {
                Object temp = AssetMgr.GetInstance().GetAsset();
                Instantiate(temp);
            });
        }
    }
}
