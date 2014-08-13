using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;


//  SkillState.cs
//  Author: Lu Zexi
//  2013-11-29


namespace Game.Gfx
{
    /// <summary>
    /// 技能状态
    /// </summary>
    public class SkillState : StateBase
    {
        public SkillState(GfxObject obj)
            : base(obj)
        { 
            //
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <returns></returns>
        public override STATE_TYPE GetStateType()
        {
            return STATE_TYPE.STATE_SKILL;
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        /// <returns></returns>
        public override bool OnEnter()
        {
            this.m_cObj.Play("skill", WrapMode.Once, 1f, PLAY_MODE.CROSS_FADE);
            return base.OnEnter();
        }

        /// <summary>
        /// 退出状态
        /// </summary>
        /// <returns></returns>
        public override bool OnExit()
        {
            return base.OnExit();
        }

        /// <summary>
        /// 逻辑更新
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            if (this.m_cObj != null)
            {
                return this.m_cObj.IsPlaying("skill");
            }
            return false;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public override void Destory()
        {
            base.Destory();
        }
    }

}