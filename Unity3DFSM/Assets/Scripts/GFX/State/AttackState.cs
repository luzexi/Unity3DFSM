

using UnityEngine;

//  AttackState.cs
//  Auth: Lu Zexi
//  2013-11-21



namespace Game.Gfx
{
    /// <summary>
    /// 攻击状态
    /// </summary>
    public class AttackState : StateBase
    {
        private GfxObject m_cTargetObj;

        public AttackState(GfxObject obj)
            : base(obj)
        { 

        }

        /// <summary>
        /// 获取状态类型
        /// </summary>
        /// <returns></returns>
        public override STATE_TYPE GetStateType()
        {
            return STATE_TYPE.STATE_ATTACK;
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="target"></param>
        public void Set(GfxObject target)
        {
            this.m_cTargetObj = target;
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        /// <returns></returns>
        public override bool OnEnter()
        {
            this.m_cObj.Play("attack" , WrapMode.Once , 1 , PLAY_MODE.CROSS_FADE );
            return true;
        }

        /// <summary>
        /// 逻辑更新
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            if (this.m_cObj != null)
            {
                return this.m_cObj.IsPlaying("attack");
            }
            return false;
        }

    }
}

