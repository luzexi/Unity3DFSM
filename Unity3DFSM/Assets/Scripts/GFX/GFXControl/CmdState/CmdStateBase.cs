
using UnityEngine;
using Game.Gfx;


//  CmdStateBase.cs
//  Auth: Lu Zexi
//  2013-11-21



/// <summary>
/// 状态类型
/// </summary>
public enum CMD_TYPE
{
    STATE_NONE = 0,     //无
    STATE_DEFENCE = 1,  //防御
    STATE_SKILL,        //不移动单体释放技能
    STATE_ALL_SKILL,    //不移动全体释放技能
    STATE_MOVE_SKILL,   //移动单体释放技能
    STATE_MOVE_ALL_SKILL,   //移动全体释放技能
    STATE_MOVE_ATTACK,  //移动攻击
    STATE_ATTACK,       //非移动攻击
    STATE_HURT,         //受伤
    STATE_DIE,          //死亡状态
}


/// <summary>
/// 命令状态基础类
/// </summary>
public abstract class CmdStateBase
{
    protected GfxObject m_cObj;   //物体
    protected StateControl m_cControl;  //控制对象

    public CmdStateBase()
    {
        this.m_cControl = this.m_cObj.GetStateControl();
    }

    /// <summary>
    /// 获取状态类型
    /// </summary>
    /// <returns></returns>
    public abstract CMD_TYPE GetCmdType();

    /// <summary>
    /// 进入事件
    /// </summary>
    /// <returns></returns>
    public virtual bool OnEnter()
    {
        return true;
    }

    /// <summary>
    /// 退出事件
    /// </summary>
    /// <returns></returns>
    public virtual bool OnExit()
    {
        return true;
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public abstract bool Update();

    /// <summary>
    /// 销毁
    /// </summary>
    public virtual void Destory()
    {
		//
    }

}


