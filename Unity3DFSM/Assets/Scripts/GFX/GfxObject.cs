using System;
using System.Collections.Generic;
using UnityEngine;

//  GfxObject.cs
//  Author: Lu Zexi
//  2013-11-21



namespace Game.Gfx
{
    /// <summary>
    /// 图形渲染类
    /// </summary>
    public class GfxObject : MonoBehaviour
    {
		public StateControl m_cStateControl; //状态控制类

        void Awake()
        {
            this.m_cStateControl = new StateControl(this);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public virtual void Destory()
        {
			this.m_cStateControl = null;

			GameObject.DestroyImmediate(this.gameObject);
        }

        /// <summary>
        /// 逻辑更新
        /// </summary>
        void FixedUpdate()
        {
			if(this.m_cStateControl != null )
				this.m_cStateControl.Update();
        }

        ////////////////////////////////////////// 状态控制 ////////////////////////////////////

        /// <summary>
        /// 获取状态控制
        /// </summary>
        /// <returns></returns>
        public StateControl GetStateControl()
        {
            return this.m_cStateControl;
        }

        /// <summary>
        /// 攻击状态
        /// </summary>
        public void AttackState()
        {
            this.m_cStateControl.Attack();
        }

        /// <summary>
        /// 空闲状态
        /// </summary>
        public void IdleState()
        {
            this.m_cStateControl.Idle();
        }

        /// <summary>
        /// 移动状态
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="costTime"></param>
        public void MoveState( Vector3 pos , float costTime )
        {
            this.m_cStateControl.Move(pos, costTime);
        }

        /// <summary>
        /// 受伤状态
        /// </summary>
        public void HurtState()
        {
            this.m_cStateControl.Hurt();
        }

        /// <summary>
        /// 技能状态
        /// </summary>
        public void SkillState()
        {
            this.m_cStateControl.Skill();
        }

        ////////////////////////////////////////////  动画 API /////////////////////////////////////////////

        /// <summary>
        /// 停止动画
        /// </summary>
        public void Stop()
        {
            if (this.animation == null)
                return;

			this.animation.Stop();
        }

        /// <summary>
        /// 播放动作
        /// </summary>
        /// <param name="name"></param>
        /// <param name="wrap"></param>
        /// <param name="speed"></param>
        /// <param name="mode"></param>
        public virtual void Play(string name, WrapMode wrap, float speed, PLAY_MODE mode)
        {
			if (this.animation == null || this.animation[name] == null)
                return;

			this.animation[name].wrapMode = wrap;
			this.animation[name].speed = speed;

            switch (mode)
            {
                case PLAY_MODE.CROSS_FADE:
				this.animation.CrossFade(name);
                    break;
                case PLAY_MODE.PLAY:
				this.animation.Play(name);
                    break;
                case PLAY_MODE.BLEND:
				this.animation.Blend(name);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 是否正在播放指定动作
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual bool IsPlaying(string name)
        {
			if (this.animation == null)
                return false;
			return this.animation.IsPlaying(name);
        }

        
        /// <summary>
        /// 获取长度
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public float GetAnimationLength(string name)
        {
			if (this.animation == null || this.animation[name] == null )
                return 0;
			return this.animation[name].length;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////

    }

}


