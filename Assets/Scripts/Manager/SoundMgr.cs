using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

namespace Manager
{
    public class SoundMgr
    {
        private static SoundMgr _instance;
        private GameObject SoundRoot;
        private Dictionary<SoundType, AudioSource> SoundList = new Dictionary<SoundType, AudioSource>();

        public float Volume = 1.0f;
        private SoundMgr()
        {
            SoundRoot = new GameObject("SoundRoot");
            SoundRoot.AddComponent<AudioListener>();
            UnityEngine.Object.DontDestroyOnLoad(SoundRoot);
        }
        public static SoundMgr GetInstance()
        {
            if (_instance == null)
                _instance = new SoundMgr();
            return _instance;
        }

        public void CreateSound(SoundType type,bool isplay=false,bool isloop=false,float volume=1.0f)
        {
            if(!SoundList.ContainsKey(type))
            {
                GameObject temp = new GameObject(type.ToString());
                temp.transform.parent=SoundRoot.transform;
                AudioSource tempsource = temp.AddComponent<AudioSource>();
                AssetMgr.GetInstance().LoadAsset("", true, true, () => { tempsource.clip = (AudioClip)AssetMgr.GetInstance().GetAsset(); });
                tempsource.volume = Volume*volume;
                tempsource.Stop();
                SoundList.Add(type, tempsource);
                if(isplay)
                {
                    PlaySound(type);
                }
            }
        }

        public bool HasCreate(SoundType type)
        {
            return SoundList.ContainsKey(type);
        }

        public void PlaySound(SoundType type,bool isloop=false,ulong delaytime=0)
        {
            if(SoundList.ContainsKey(type))
            {
                SoundList[type].loop = isloop;
                SoundList[type].Play(delaytime);
            }
        }

        public bool IsPlaying(SoundType type)
        {
            return SoundList[type].isPlaying;
        }

        public void SetVolume(SoundType type,float volume)
        {
            SoundList[type].volume = Volume * volume;
        }

        public void SetLoop(SoundType type,bool isloop)
        {
            SoundList[type].loop = isloop;
        }

        public void StopSound(SoundType type)
        {
            if(SoundList.ContainsKey(type))
            {
                SoundList[type].Stop();
            }
        }

        public void StopAllSound()
        {
            var e = SoundList.GetEnumerator();
            while(e.MoveNext())
            {
                e.Current.Value.Stop();
            }
        }

        public void DeleteSound(SoundType type)
        {
            if(SoundList.ContainsKey(type))
            {
                StopSound(type);
                GameObject.Destroy(SoundList[type].gameObject);
                AssetMgr.GetInstance().CleanAssetPool("");
                SoundList.Remove(type);
            }
        }

        public void DeleteAllSound()
        {
            var e = SoundList.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current.Value.Stop();
                GameObject.Destroy(e.Current.Value.gameObject);
                AssetMgr.GetInstance().CleanAssetPool("");
            }
            e.Dispose();
            SoundList.Clear();
        }
    }
}
