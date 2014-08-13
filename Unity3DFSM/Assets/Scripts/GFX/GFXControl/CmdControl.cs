using System;
using System.Collections.Generic;
using System.Collections;
using Game.Gfx;
using UnityEngine;


//  CmdControl.cs
//  Author: Lu Zexi
//  2013-11-29



/// <summary>
/// 命令控制
/// </summary>
public class CmdControl
{
    private CmdStateBase m_cState;  //当前状态
    private CmdStateWrap m_cStateWrap;  //命令状态包

    /// <summary>
    /// 命令状态包
    /// </summary>
    private class CmdStateWrap
    {

        public CmdStateWrap()
        {
            //
        }
    }

    public CmdControl()
    {
        this.m_cState = null;
        this.m_cStateWrap = new CmdStateWrap();
    }

    /// <summary>
    /// 获取命令状态
    /// </summary>
    /// <returns></returns>
    public CMD_TYPE GetCmdType()
    {
        if (this.m_cState == null)
            return CMD_TYPE.STATE_NONE;
        return this.m_cState.GetCmdType();
	}


    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public bool Update()
    {
        if (this.m_cState == null)
            return false;
        if (!this.m_cState.Update())
        {
            if (this.m_cState != null)
                this.m_cState.OnExit();
            this.m_cState = null;
            return false;
        }
        return true;
    }

}
