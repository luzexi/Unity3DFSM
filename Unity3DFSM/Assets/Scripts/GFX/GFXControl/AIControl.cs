using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//  AIControl.cs
//  Author: Lu Zexi
//  2013-11-29



/// <summary>
/// AI控制
/// </summary>
public class AIControl
{

    public AIControl()
    {
    }

    /// <summary>
    /// 逻辑更新
    /// </summary>
    /// <returns></returns>
    public virtual bool Update()
    {
        return true;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Initialize()
    {
        //
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public virtual void Destroy()
    { 
        //
    }

}

