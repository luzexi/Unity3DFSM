using System;
using System.Collections.Generic;
using UnityEngine;


//  DieState.cs
//  Author: Lu Zexi
//  2013-12-02



namespace Game.Gfx
{
    /// <summary>
    /// 死亡状态
    /// </summary>
    public class DieState : StateBase
    {
        private Vector3 m_cStartScale;  //起始比率
        private float m_fStartTime; //时间
        private Vector3 m_cStartPos;    //开始点


        private const float COST_TIME = 0.5F; //花费时间
        private Vector3 SHAKE_DIS = new Vector3(0.01f, 0.005f, 0); //抖动位置
        private Vector3 END_SCALE = new Vector3(0, 1.5f, 1);    //最终大小

        public DieState(GfxObject obj)
            : base(obj)
        { 
            //
        }

        /// <summary>
        /// 获取状态类型
        /// </summary>
        /// <returns></returns>
        public override STATE_TYPE GetStateType()
        {
            return STATE_TYPE.STATE_DIE;
        }

        /// <summary>
        /// 进入事件
        /// </summary>
        /// <returns></returns>
        public override bool OnEnter()
        {
            //GameObject obj = this.m_cObj.GetGameObject();
            //foreach (Renderer item in obj.transform.GetComponentsInChildren<Renderer>())
            //{
            //    foreach (Material mat in item.materials)
            //    {
            //        mat.shader = Shader.Find("Transparent/Diffuse");
            //        //mat.shader = Shader.Find("Game/GameGuide");
            //    }
            //}

            this.m_cStartScale = this.m_cObj.transform.localScale;
            this.m_cStartPos = this.m_cObj.transform.localPosition;

            this.m_fStartTime = Time.fixedTime;

            this.m_cObj.Stop();

            return base.OnEnter();
        }


        /// <summary>
        /// 退出事件
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
            float disTime = Time.fixedTime - this.m_fStartTime;

            if (disTime > COST_TIME)
            {
                this.m_cObj.transform.localScale = Vector3.zero;
                return false;
            }

            float rate = disTime / COST_TIME;
            //float rate1 = CMath.ExponentialOut(rate, 0, 1, 1);
			float rate1 = Mathf.Lerp(0,1,rate);

            Vector3 pos = this.m_cStartPos;
            pos.x += UnityEngine.Random.Range(-SHAKE_DIS.x * rate1, SHAKE_DIS.x * rate1);
            pos.y += UnityEngine.Random.Range(-SHAKE_DIS.y * rate1, SHAKE_DIS.y * rate1);
            pos.z += UnityEngine.Random.Range(-SHAKE_DIS.z * rate1, SHAKE_DIS.z * rate1);

            Vector3 target = new Vector3(this.m_cStartScale.x * 0.1f, 1.3f, 1f);
            Vector3 scale = Vector3.Lerp(this.m_cStartScale, target, rate);
            //Vector3 scale  = new Vector3(th.m_cStartScale * (COST_TIME - disTime) / COST_TIME, this.m_cStartScale.y * (COST_TIME + disTime) / COST_TIME, this.m_cStartScale.z);
            this.m_cObj.transform.localScale = scale;
            this.m_cObj.transform.localPosition = pos;

            return true;
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