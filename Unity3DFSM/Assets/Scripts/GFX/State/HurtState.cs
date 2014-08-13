using UnityEngine;

//  HurtState.cs
//  Auth: Lu Zexi
//  2013-11-21



namespace Game.Gfx
{
    /// <summary>
    /// 受击状态
    /// </summary>
    public class HurtState : StateBase
    {

        private Vector3 m_cStartPos;    //开始位置
        private float m_fStateStartTime;    //开始时间

        private const float COST_TIME = 0.15f; //花费时间
        private Vector3 SHAKE_DIS = new Vector3(0, 0.2f, 0);   //抖动距离

        public HurtState(GfxObject obj)
            : base(obj)
        {

        }

        /// <summary>
        /// 获取状态类型
        /// </summary>
        /// <returns></returns>
        public override STATE_TYPE GetStateType()
        {
            return STATE_TYPE.STATE_HURT;
        }

        /// <summary>
        /// 进入状态
        /// </summary>
        /// <returns></returns>
        public override bool OnEnter()
        {
            this.m_cStartPos = this.m_cObj.transform.localPosition;
            this.m_fStateStartTime = Time.fixedTime;
            return true;
        }

        /// <summary>
        /// 退出状态
        /// </summary>
        /// <returns></returns>
        public override bool OnExit()
        {
            this.m_cObj.transform.localPosition = this.m_cStartPos;
            return base.OnExit();
        }

        /// <summary>
        /// 逻辑更新
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {

            float disTime = Time.fixedTime - this.m_fStateStartTime;

            if (disTime > COST_TIME)
            {
                this.m_cObj.transform.localPosition = this.m_cStartPos;
                return false;
            }

            float rate = disTime / COST_TIME;
            //float rate1 = CMath.ExponentialOut(rate, 0, 1, 1);
			float rate1 = Mathf.Lerp(0,1,rate);

            Vector3 pos = this.m_cStartPos;
            pos.x += UnityEngine.Random.Range(-SHAKE_DIS.x * rate1, SHAKE_DIS.x * rate1);
            pos.y += UnityEngine.Random.Range(0, SHAKE_DIS.y * rate1);
            pos.z += UnityEngine.Random.Range(-SHAKE_DIS.z * rate1, SHAKE_DIS.z * rate1);

            this.m_cObj.transform.localPosition = pos;

            return true;
        }

    }
}

