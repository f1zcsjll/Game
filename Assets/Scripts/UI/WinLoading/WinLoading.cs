using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using Manager;
using UnityEngine.UI;

namespace UI
{
    public class WinLoading : BaseWindow
    {
        [SerializeField]
        Slider Progress;
        [SerializeField]
        RawImage BG;

        protected override void Start()
        {
            base.Start();
            AssetMgr.GetInstance().LoadAsset("", false, false, () =>
            {
                BG.texture = (Texture)AssetMgr.GetInstance().GetAsset();
                BG.color = Color.white;
                BG.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
            });
            FrameMgr.GetInstance().RegisterUpdate(ShowProgress);
        }

        protected override Define.UIType GetUIType()
        {
            return Define.UIType.WinLoading;
        }

        void ShowProgress()
        {
            Progress.value = LoadSceneMgr.GetInstance().GetLoadProgress();
        }

        protected override void OnClickCloseButton()
        {
            base.OnClickCloseButton();
        }

        protected override void OnDestroy()
        {
            FrameMgr.GetInstance().UnRegisterUpdate(ShowProgress);
            OnClickCloseButton();
            base.OnDestroy();
        }
    }
}
