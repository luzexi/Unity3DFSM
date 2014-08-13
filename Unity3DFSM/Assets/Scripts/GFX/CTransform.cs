

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  CTransform.cs
//  Auth: Lu Zexi
//  2013-11-21


namespace Game.Gfx
{

    /// <summary>
    /// U3D的Transforms封装类
    /// </summary>
    public class CTransform
    {
        protected CTransform m_cParent = null;  //父节点

        protected Transform m_cTrans;    //实体

        protected string m_strName; //实体名
        protected string m_strTag;  //标签
        protected Vector3 m_vecPos; //位置
        protected Vector3 m_vecLocalPos;    //相对位置
        protected Quaternion m_quaRot;  //朝向
        protected Quaternion m_quaLoaclRot; //相对朝向
        protected Vector3 m_vecLocalScale = new Vector3(1, 1, 1);  //相对比率
        protected bool m_bActive;   //是否激活

        public CTransform()
        {
        }

        public CTransform(Transform trans)
        {
            this.m_cTrans = trans;
            this.m_vecPos = trans.position;
            this.m_quaRot = trans.rotation;
            this.m_strTag = trans.tag;
            this.m_strName = trans.name;
            this.m_vecLocalPos = trans.localPosition;
            this.m_quaLoaclRot = trans.localRotation;
            this.m_vecLocalScale = trans.localScale;
            this.m_bActive = trans.active;
        }

        public CTransform(CTransform trans)
        {
            this.m_vecPos = trans.position;
            this.m_quaRot = trans.rotation;
            this.m_strTag = trans.tag;
            this.m_strName = trans.name;
            this.m_vecLocalPos = trans.localPosition;
            this.m_quaLoaclRot = trans.localRotation;
            this.m_vecLocalScale = trans.localScale;
            this.m_bActive = trans.active;

            this.m_cTrans.position = this.m_vecPos;
            this.m_cTrans.rotation = this.m_quaRot;

            this.m_cTrans.localScale = this.m_vecLocalScale;

        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public CTransform Clone()
        {
            return new CTransform(this);
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public Transform Parent
        {
            get { return this.m_cTrans.parent; }
            set { this.m_cTrans.parent = value; }
        }


        /// <summary>
        /// 世界位置
        /// </summary>
        public Vector3 position
        {
            get
            {
                if (this.m_cTrans != null)
                    return this.m_cTrans.position;
                return this.m_vecPos;
            }
            set
            {
                this.m_vecPos = value;
                if (this.m_cTrans != null)
                {
                    this.m_cTrans.position = value;
                    this.m_vecLocalPos = this.m_cTrans.localPosition;
                }
                else
                {
                    if (this.m_cParent != null)
                    {
                        this.m_vecLocalPos = this.m_vecPos - this.m_cParent.position;
                    }
                    else
                    {
                        this.m_vecLocalPos = value;
                    }
                }
            }
        }

        public Quaternion rotation
        {
            get
            {
                if (this.m_cTrans != null)
                    return this.m_cTrans.rotation;
                return this.m_quaRot;
            }
            set
            {
                this.m_quaRot = value;
                if (this.m_cTrans != null)
                {
                    this.m_cTrans.rotation = value;
                    this.m_quaLoaclRot = this.m_cTrans.localRotation;
                }
                else
                {
                    if (this.m_cParent != null)
                    {
                        Vector3 prot = this.m_cParent.rotation.eulerAngles;
                        Vector3 lrot = this.m_quaRot.eulerAngles;
                        this.m_quaLoaclRot = Quaternion.Euler(lrot - prot);
                    }
                    else
                    {
                        this.m_quaLoaclRot = value;
                    }
                }
            }
        }

        public string tag
        {
            get
            {
                if (this.m_cTrans != null)
                    return this.m_cTrans.tag;
                return this.m_strTag;
            }
            set
            {
                this.m_strTag = value;
                if (this.m_cTrans != null)
                {
                    this.m_cTrans.tag = value;
                }
            }
        }

        /// <summary>
        /// 名字
        /// </summary>
        public string name
        {
            get
            {
                if (this.m_cTrans != null)
                    return this.m_cTrans.name;
                return this.m_strName;
            }
            set
            {
                this.m_strName = value;
                if (this.m_cTrans != null)
                {
                    this.m_cTrans.name = value;
                }
            }
        }

        public Vector3 localPosition
        {
            get
            {
                if (this.m_cTrans != null)
                    return this.m_cTrans.localPosition;
                return this.m_vecLocalPos;
            }
            set
            {
                this.m_vecLocalPos = value;
                if (this.m_cTrans != null)
                {
                    this.m_cTrans.localPosition = value;
                    this.m_vecPos = this.m_cTrans.position;
                }
                else
                {
                    if (this.m_cParent != null)
                    {
                        this.m_vecPos = this.m_cParent.position + this.m_vecLocalPos;
                    }
                    else
                    {
                        this.m_vecPos = value;
                    }
                }
            }
        }

        public Quaternion localRotation
        {
            get
            {
                if (this.m_cTrans != null)
                    return this.m_cTrans.localRotation;
                return this.m_quaLoaclRot;
            }
            set
            {
                this.m_quaLoaclRot = value;
                if (this.m_cTrans != null)
                {
                    this.m_cTrans.localRotation = value;
                    this.m_quaRot = this.m_cTrans.rotation;
                }
                else
                {
                    if (this.m_cParent != null)
                    {
                        Vector3 prot = this.m_cParent.rotation.eulerAngles;
                        Vector3 lrot = this.m_quaLoaclRot.eulerAngles;

                        this.m_quaRot = Quaternion.Euler(lrot + prot);
                    }
                    else
                    {
                        this.m_quaRot = value;
                    }
                }
            }
        }

        public Vector3 forward
        {
            get
            {
                if (this.m_cTrans != null)
                    return this.m_cTrans.forward;
                return this.m_quaRot * Vector3.forward;
            }
        }

        public Vector3 right
        {
            get
            {
                if (this.m_cTrans != null)
                    return this.m_cTrans.right;
                return this.m_quaRot * Vector3.right;
            }
        }

        public Vector3 up
        {
            get
            {
                if (this.m_cTrans != null)
                    return this.m_cTrans.up;
                return this.m_quaRot * Vector3.up;
            }
        }

        public Vector3 localScale
        {
            get
            {
                if (this.m_cTrans != null)
                    return this.m_cTrans.localScale;
                return this.m_vecLocalScale;
            }
            set
            {
                this.m_vecLocalScale = value;
                if (this.m_cTrans != null)
                {
                    this.m_cTrans.localScale = value;
                }
            }
        }

        public bool active
        {
            get
            {
                if (this.m_cTrans != null)
                    return this.m_cTrans.active;
                return this.m_bActive;
            }
            set
            {
                this.m_bActive = value;
                if (this.m_cTrans != null)
                    this.m_cTrans.active = value;
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="trans"></param>
        public virtual void Reset(Transform trans)
        {
            this.m_cTrans = trans;
            this.m_cTrans.position = this.m_vecPos;
            this.m_cTrans.rotation = this.m_quaRot;
            this.m_cTrans.localPosition = this.m_vecLocalPos;
            this.m_cTrans.localRotation = this.m_quaLoaclRot;
            this.m_cTrans.localScale = this.m_vecLocalScale;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public virtual void Destory()
        {
            this.m_vecPos = Vector3.zero;
            this.m_vecLocalPos = Vector3.zero;
            this.m_quaRot = Quaternion.identity;
            this.m_quaLoaclRot = Quaternion.identity;
            this.m_vecLocalScale = new Vector3(1, 1, 1);
            this.m_cTrans = null;
        }

        /// <summary>
        /// 更新实体位置信息
        /// </summary>
        public void UpdateTrans()
        {
            this.m_vecPos = this.m_cTrans.position;
            this.m_quaRot = this.m_cTrans.rotation;
            this.m_strTag = this.m_cTrans.tag;
            this.m_strName = this.m_cTrans.name;
            this.m_vecLocalPos = this.m_cTrans.localPosition;
            this.m_quaLoaclRot = this.m_cTrans.localRotation;
            this.m_vecLocalScale = this.m_cTrans.localScale;
            this.m_bActive = this.m_cTrans.active;
        }

        /// <summary>
        /// 旋转实体
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Rotate(float x, float y, float z)
        {
            if (this.m_cTrans == null)
            {
                Vector3 angle = this.rotation.eulerAngles + new Vector3(x, y, z);
                this.rotation = Quaternion.Euler(angle);
                return;
            }
            else
                this.m_cTrans.Rotate(x, y, z);
            UpdateTrans();
        }

        /// <summary>
        /// 旋转实体
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Rotate(Vector3 rot)
        {
            if (this.m_cTrans == null)
            {
                Vector3 angle = this.rotation.eulerAngles + rot;
                this.rotation = Quaternion.Euler(angle);
                return;
            }
            else
                this.m_cTrans.Rotate(rot);
            UpdateTrans();
        }

        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        public void RotateAround(Vector3 pos, Vector3 axis, float angle)
        {
            if (this.m_cTrans == null)
            {
                //do something
                return;
            }
            else
                this.m_cTrans.RotateAround(pos, axis, angle);
            UpdateTrans();
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="dir"></param>
        public void Translate(Vector3 dir)
        {
            if (this.m_cTrans == null)
            {
                this.position = this.position + dir;
                return;
            }
            else
                this.m_cTrans.Translate(dir);
            UpdateTrans();
        }

        /// <summary>
        /// 看着某点
        /// </summary>
        /// <param name="pos"></param>
        public void LookAt(Vector3 pos)
        {
            if (this.m_cTrans == null)
            {
                Vector3 dir = pos - this.position;
                this.rotation = Quaternion.LookRotation(dir);
                return;
            }
            else
                this.m_cTrans.LookAt(pos);
            UpdateTrans();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////// 挂接 API //////////////////////////////////////////

        /// <summary>
        /// 设置子节点
        /// </summary>
        /// <param name="trans"></param>
        public bool Attached(CTransform trans, string boneName)
        {
            if (this.m_cTrans == null)
                return false;

            foreach (Transform item in this.m_cTrans)
            {
                if (item.name == boneName)
                {
                    trans.m_cTrans.parent = item;
                    trans.localPosition = Vector3.zero;
                    trans.localRotation = Quaternion.identity;
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 是否包含指定Transform
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool ContainTrans(Transform trans)
        {
            foreach (Transform item in this.m_cTrans)
            {
                if (item == trans)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否包含指定GameTrans
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool ContainTrans(CTransform trans)
        {
            foreach (Transform item in this.m_cTrans)
            {
                if (item == trans.m_cTrans)
                {
                    return true;
                }
            }
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////// Character Controller ////////////////////////////

        /// <summary>
        /// 是否着地
        /// </summary>
        /// <returns></returns>
        public virtual bool IsGround()
        {
            return false;
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="dir"></param>
        public virtual CollisionFlags Move(Vector3 dir)
        {
            return CollisionFlags.None;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 忽略碰撞检测
        /// </summary>
        /// <param name="trans"></param>
        public virtual void IgnorCollider(CTransform trans)
        {
            Collider[] colliders1 = this.m_cTrans.GetComponentsInChildren<Collider>();
            Collider[] colliders2 = trans.m_cTrans.GetComponentsInChildren<Collider>();

            foreach (Collider item1 in colliders1)
            {
                if (item1.isTrigger == true)
                    continue;
                foreach (Collider item2 in colliders2)
                {
                    if (item2.isTrigger == true)
                        continue;
                    Physics.IgnoreCollision(item1, item2);
                }
            }
        }

    }


}