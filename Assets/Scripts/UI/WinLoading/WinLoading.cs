using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC;
using Manager;
using UnityEngine.UI;

namespace UI
{
    public class WinLoading : View
    {
        [SerializeField]
        Slider Progress;
        [SerializeField]
        RawImage BG;

        void Start()
        {
            AssetMgr.GetInstance().LoadAsset("", false, false, () => {
                BG.texture = (Texture)AssetMgr.GetInstance().GetAsset();
                BG.color = Color.white;
                BG.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
            });
            FrameMgr.GetInstance().RegisterUpdate(ShowProgress);
        }

        void ShowProgress()
        {
            Progress.value = LoadSenceMgr.GetInstance().GetLoadProgress();
        }

        protected override void OnDestroy()
        {
            FrameMgr.GetInstance().UnRegisterUpdate(ShowProgress);
            base.OnDestroy();
        }
    }
}
