

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  CAnimation.cs
//  Auth: Lu Zexi
//  2013-11-21


namespace Game.Gfx
{
    public enum PLAY_MODE
    {
        NONE = 0,
        CROSS_FADE = 1,
        PLAY = 2,
        BLEND = 3,
    }

    /// <summary>
    /// U3d动作封装类
    /// </summary>
    public class CAnimation
    {
        private Animation m_cAnimation = null; //U3D动作组件
        public delegate float FuncTime();
        private FuncTime m_delGetTime;   //获取时间方法
        private float m_fScale = 1f;     //缩放比率

        private string m_strCurrentAni = ""; //当前动作名
        private float m_fTime = -0xFFFF;          //当前动作所进行的时间

        private float m_fSpeed = 1f;         //当前动作的速度
        private WrapMode m_eWrap = WrapMode.Once;       //当前动作的循环模式

        private const float DEFAULT_MAX_TIME = 50f;   //默认的最长时间

        public WrapMode wrapMode
        {
            get { return this.m_cAnimation.wrapMode; }
        }

        public CAnimation(Animation ani, FuncTime timeFunc)
        {
            this.m_cAnimation = ani;
            this.m_cAnimation.cullingType = AnimationCullingType.BasedOnUserBounds;
            this.m_delGetTime = timeFunc;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="ani"></param>
        public void Reset(Animation ani)
        {
            this.m_cAnimation = ani;
            this.m_cAnimation.cullingType = AnimationCullingType.BasedOnUserBounds;

            if (this.m_cAnimation != null)
            {
                if (this.m_cAnimation[this.m_strCurrentAni] != null)
                {
                    this.m_cAnimation.Play(this.m_strCurrentAni);
                    this.m_cAnimation[this.m_strCurrentAni].speed = this.m_fSpeed*this.m_fScale;
                    this.m_cAnimation[this.m_strCurrentAni].wrapMode = this.m_eWrap;
                    this.m_cAnimation[this.m_strCurrentAni].time = (this.m_delGetTime() - this.m_fTime)*this.m_fSpeed*this.m_fScale;
                }
            }
        }

        /// <summary>
        /// 设置播放比例
        /// </summary>
        /// <param name="speed"></param>
        public void SetScale(float scale)
        {
            if (this.m_cAnimation != null)
            {
                if (this.m_cAnimation[this.m_strCurrentAni] != null)
                {
                    this.m_cAnimation[this.m_strCurrentAni].speed = scale * this.m_fSpeed;
                }
            }
            this.m_fScale = scale;
        }

        /// <summary>
        /// 渲染更新
        /// </summary>
        /// <returns></returns>
        public bool Render()
        {
            return true;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destory()
        {
            this.m_cAnimation = null;
        }

        /// <summary>
        /// 逻辑更新
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            return true;
        }

        /// <summary>
        /// 是否在播放指定动作
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsPlay(string name)
        {
            if (this.m_cAnimation != null)
            {
                return this.m_cAnimation.IsPlaying(name);
            }
            else
            {
                if (this.m_eWrap == WrapMode.Loop || this.m_eWrap == WrapMode.PingPong)
                    return true;

                if ( (this.m_delGetTime() - this.m_fTime) * this.m_fSpeed * this.m_fScale > DEFAULT_MAX_TIME)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否正在播放
        /// </summary>
        /// <returns></returns>
        public bool IsPlay()
        {
            if (this.m_cAnimation != null)
            {
                return this.m_cAnimation.isPlaying;
            }
            else
            {
                if (this.m_eWrap == WrapMode.Loop || this.m_eWrap == WrapMode.PingPong)
                    return true;

                if ((this.m_delGetTime() - this.m_fTime)*this.m_fSpeed * this.m_fScale > DEFAULT_MAX_TIME)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否包含指定动作
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsContainAni(string name)
        {
            if (this.m_cAnimation == null)
                return false;
            if (this.m_cAnimation[name] == null)
                return false;
            return true;
        }

        /// <summary>
        /// 播放动作
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Play(string name, WrapMode wrap, float speed)
        {
            if (this.m_cAnimation != null)
            {
                if (this.m_cAnimation[name] != null)
                {
                    this.m_strCurrentAni = name;
                    this.m_fTime = this.m_delGetTime();
                    this.m_eWrap = wrap;
                    this.m_fSpeed = speed;

                    this.m_cAnimation[name].speed = speed * this.m_fScale;
                    this.m_cAnimation[name].wrapMode = wrap;
                    this.m_cAnimation.Play(name);

                    return true;
                }
                return false;
            }
            else
            {
                this.m_strCurrentAni = "";
                this.m_fTime = this.m_delGetTime();
                this.m_eWrap = wrap;
                this.m_fSpeed = speed;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 融合播放动作
        /// </summary>
        /// <param name="name"></param>
        public bool CrossFade(string name, WrapMode wrap, float speed)
        {
            if (this.m_cAnimation != null)
            {
                if (this.m_cAnimation[name] != null)
                {
                    this.m_strCurrentAni = name;
                    this.m_fTime = this.m_delGetTime();
                    this.m_eWrap = wrap;
                    this.m_fSpeed = speed;

                    this.m_cAnimation[name].speed = speed * this.m_fScale;
                    this.m_cAnimation[name].wrapMode = wrap;
                    this.m_cAnimation.CrossFade(name);

                    return true;
                }

                return false;
            }
            else
            {
                this.m_strCurrentAni = "";
                this.m_fTime = this.m_delGetTime();
                this.m_eWrap = wrap;
                this.m_fSpeed = speed;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 骨骼融合播放动作
        /// </summary>
        /// <param name="name"></param>
        public bool Blend(string name, WrapMode wrap, float speed)
        {
            if (this.m_cAnimation != null)
            {
                if (this.m_cAnimation[name] != null)
                {
                    this.m_strCurrentAni = name;
                    this.m_fTime = this.m_delGetTime();
                    this.m_eWrap = wrap;
                    this.m_fSpeed = speed;

                    this.m_cAnimation[name].speed = speed * this.m_fScale;
                    this.m_cAnimation[name].wrapMode = wrap;
                    this.m_cAnimation.Blend(name);

                    return true;
                }
                return false;
            }
            else
            {
                this.m_strCurrentAni = "";
                this.m_fTime = this.m_delGetTime();
                this.m_eWrap = wrap;
                this.m_fSpeed = speed;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 停止所有动作
        /// </summary>
        public void Stop()
        {
            if (this.m_cAnimation != null)
            {
                this.m_cAnimation.Stop();
                
            }
            this.m_fTime = DEFAULT_MAX_TIME + 1f;
            return;
        }

        /// <summary>
        /// 获取动作时间长度
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public float GeLength(string name)
        {
            if (this.m_cAnimation == null)
                return DEFAULT_MAX_TIME;
            if (this.m_cAnimation[name] == null)
                return 0;
            return this.m_cAnimation[name].length;
        }

        /// <summary>
        /// 获取所有动作名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetAnimationNames()
        {
            List<string> aniNames = new List<string>();
            if (this.m_cAnimation == null)
                return aniNames;

            foreach (AnimationState item in this.m_cAnimation)
            {
                aniNames.Add(item.name);
            }
            return aniNames;
        }

    }




}