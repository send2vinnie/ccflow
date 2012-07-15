using System;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;

namespace BP.En
{
    /// <summary>
    /// 访问控制
    /// </summary>
    public class UAC
    {
        #region 常用方法
        public void Readonly()
        {
            this.IsUpdate = false;
            this.IsDelete = false;
            this.IsInsert = false;
            this.IsAdjunct = false;
            this.IsView = true;
        }
        /// <summary>
        /// 全部开放
        /// </summary>
        public void OpenAll()
        {
            this.IsUpdate = true;
            this.IsDelete = true;
            this.IsInsert = true;
            this.IsAdjunct = false;
            this.IsView = true;
        }
        /// <summary>
        /// 为一个岗位设置全部的权限
        /// </summary>
        /// <param name="fk_station"></param>
        public void OpenAllForStation(string fk_station)
        {
            Paras ps = new Paras();
            ps.Add("user", Web.WebUser.No);
            ps.Add("st", fk_station);

            if (DBAccess.IsExits("SELECT FK_Emp FROM Port_EmpStation WHERE FK_Emp=" + SystemConfig.AppCenterDBVarStr + "user AND FK_Station=" + SystemConfig.AppCenterDBVarStr + "st", ps))
                this.OpenAll();
            else
                this.Readonly();
        }
        /// <summary>
        /// 仅仅对管理员
        /// </summary>
        public UAC OpenForSysAdmin()
        {
            if (SystemConfig.SysNo == "WebSite")
            {
                this.OpenAll();
                return this;
            }

            if (BP.Web.WebUser.No == "admin")
                this.OpenAll();
            return this;
        }
        public UAC OpenForAppAdmin()
        {
            if (BP.Web.WebUser.No.Contains("admin") == true)
            {
                this.OpenAll();
            }
            return this;
        }
        #endregion

        #region 控制属性
        /// <summary>
        /// 是否插入
        /// </summary>
        public bool IsInsert = false;
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete = false;
        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsUpdate = false;
        /// <summary>
        /// 是否查看
        /// </summary>
        public bool IsView = true;
        /// <summary>
        /// 附件
        /// </summary>
        public bool IsAdjunct = false;
        #endregion

        #region 构造
        /// <summary>
        /// 用户访问
        /// </summary>
        public UAC()
        {

        }
        #endregion
    }
    /// <summary>
    /// Entity 的摘要说明。
    /// </summary>	
    [Serializable]
    abstract public class EnObj
    {
        #region 访问控制.
        public string ToE(string no, string chVal)
        {
            return Sys.Language.GetValByUserLang(no, chVal);
        }
        public string ToEP1(string no, string chVal, string v)
        {
            return string.Format(Sys.Language.GetValByUserLang(no, chVal), v);
        }
        public string ToEP2(string no, string chVal, string v, string v1)
        {
            return string.Format(Sys.Language.GetValByUserLang(no, chVal), v, v1);
        }
        private string _DBVarStr = null;
        public string HisDBVarStr
        {
            get
            {
                if (_DBVarStr != null)
                    return _DBVarStr;

                _DBVarStr = this.EnMap.EnDBUrl.DBVarStr;
                return _DBVarStr;
            }
        }
        /// <summary>
        /// 他的访问控制.
        /// </summary>
        protected UAC _HisUAC = null;
        /// <summary>
        /// 得到 uac 控制.
        /// </summary>
        /// <returns></returns>
        public virtual UAC HisUAC
        {
            get
            {
                if (_HisUAC == null)
                {
                    _HisUAC = new UAC();
                    if (BP.Web.WebUser.No == "admin")
                    {
                        _HisUAC.IsAdjunct = false;
                        _HisUAC.IsDelete = true;
                        _HisUAC.IsInsert = true;
                        _HisUAC.IsUpdate = true;
                        _HisUAC.IsView = true;
                    }
                }
                return _HisUAC;
            }
        }
        #endregion

        #region 取出外部配置的属性信息
        /// <summary>
        /// 取出Map 的扩展属性。
        /// 用于第3方的扩展属性开发。
        /// </summary>
        /// <param name="key">属性Key</param>
        /// <returns>设置的属性</returns>
        public string GetMapExtAttrByKey(string key)
        {
            Paras ps = new Paras();
            ps.Add("enName", this.ToString());
            ps.Add("key", key);

            return (string)DBAccess.RunSQLReturnVal("select attrValue from Sys_ExtMap WHERE className=" + SystemConfig.AppCenterDBVarStr + "enName AND attrKey=" + SystemConfig.AppCenterDBVarStr + "key", ps);
        }
        #endregion

        #region CreateInstance
        /// <summary>
        /// 创建一个实例
        /// </summary>
        /// <returns>自身的实例</returns>
        public Entity CreateInstance()
        {
            return this.GetType().Assembly.CreateInstance(this.ToString()) as Entity;
            //return ClassFactory.GetEn(this.ToString());
        }
        private Entities _GetNewEntities = null;
        #endregion

        #region 方法
        /// <summary>
        /// 重新设置默信息.
        /// </summary>
        public void ResetDefaultVal()
        {
            foreach (Attr attr in this.EnMap.Attrs)
            {
                if (attr.UIIsReadonly == false)
                    continue;
                string v = attr.DefaultVal.ToString();
                if (v.Contains("@") == false)
                    continue;

                // 设置默认值.
                switch (v)
                {
                    case "@WebUser.No":
                        this.SetValByKey(attr.Key, Web.WebUser.No);
                        break;
                    case "@WebUser.Name":
                        this.SetValByKey(attr.Key, Web.WebUser.Name);
                        break;
                    case "@WebUser.FK_Dept":
                        this.SetValByKey(attr.Key, Web.WebUser.FK_Dept);
                        break;
                    case "@WebUser.FK_DeptName":
                        this.SetValByKey(attr.Key, Web.WebUser.FK_DeptName);
                        break;
                    case "@RDT":
                        this.SetValByKey(attr.Key, DataType.CurrentData);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region 构造
        /// <summary>
        /// 实体
        /// </summary>
        public EnObj()
        {
        }
        private Map _tmpEnMap = null;
        /// <summary>
        /// Map
        /// </summary>
        protected Map _enMap
        {
            get
            {
                if (_tmpEnMap != null)
                    return _tmpEnMap;

                Map obj = Cash.GetMap(this.ToString());
                if (obj == null)
                {
                    if (_tmpEnMap == null)
                        return null;
                    else
                        return _tmpEnMap;
                }
                else
                {
                    _tmpEnMap = obj;
                }
                return _tmpEnMap;
            }
            set
            {
                if (value == null)
                {
                    _tmpEnMap = null;
                    return;
                }

                Map mp = (Map)value;
                if (SystemConfig.IsDebug)
                {
                    //#region 检查map 是否合理。
                    //if (mp != null)
                    //{
                    //    int i = 0;
                    //    foreach (Attr attr in this.EnMap.Attrs)
                    //    {
                    //        if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.PKFK)
                    //            i++;
                    //    }
                    //    if (i == 0)
                    //        throw new Exception("@没有给【" + this.EnDesc + "】定义主键。");

                    //    if (this.IsNoEntity)
                    //    {
                    //        if (mp.Attrs.Contains("No") == false)
                    //            throw new Exception("@EntityNo 类map中没有 No 属性。@类" + mp.EnDesc + " , " + this.ToString());

                    //        if (i != 1)
                    //            throw new Exception("@多个主键在 EntityNo 类中是不允许的。 @类" + mp.EnDesc + " , " + this.ToString());
                    //    }
                    //    else if (this.IsOIDEntity)
                    //    {
                    //        if (mp.Attrs.Contains("OID") == false)
                    //            throw new Exception("@EntityOID 类map中没有 OID 属性。@类" + mp.EnDesc + " , " + this.ToString());
                    //        if (i != 1)
                    //            throw new Exception("@多个主键在 EntityOID 类中是不允许的。 @类" + mp.EnDesc + " , " + this.ToString());
                    //    }
                    //    else
                    //    {
                    //        if (mp.Attrs.Contains("MyPK"))
                    //            if (i != 1)
                    //                throw new Exception("@多个主键在 EntityMyPK 类中是不允许的。 @类" + mp.EnDesc + " , " + this.ToString());
                    //    }
                    //}
                    //#endregion 检查map 是否合理。
                }

                if (mp == null || mp.DepositaryOfMap == Depositary.None)
                {
                    _tmpEnMap = mp;
                    return;
                }
                Cash.SetMap(this.ToString(), mp);
                _tmpEnMap = mp;
            }
        }
        /// <summary>
        /// 子类需要继承
        /// </summary>
        public abstract Map EnMap
        {
            get;
        }
        #endregion

        #region row
        /// <summary>
        /// 实体的 map 信息。	
        /// </summary>		
        //public abstract void EnMap();		
        private Row _row = null;
        public Row Row
        {
            get
            {
                if (this._row == null)
                {
                    this._row = new Row();
                    this._row.LoadAttrs(this.EnMap.Attrs);
                }
                return this._row;
            }
            set
            {
                this._row = value;
            }
        }
        #endregion

        #region 关于属性的操作。

        #region 设置值方法
        public void SetValByKeySuperLink(string attrKey, string val)
        {
            this.SetValByKey(attrKey, DataType.DealSuperLink(val));
        }

        /// <summary>
        /// 设置object类型的值
        /// </summary>
        /// <param name="attrKey">attrKey</param>
        /// <param name="val">val</param>
        public void SetValByKey(string attrKey, string val)
        {
            switch (val)
            {
                case null:
                case "&nbsp;":
                    val = "";
                    break;
                case "RDT":
                    if (val.Length > 4)
                    {
                        this.SetValByKey("FK_NY", val.Substring(0, 7));
                        this.SetValByKey("FK_ND", val.Substring(0, 4));
                    }
                    break;
                default:
                    break;
            }
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, int val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, Int64 val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, float val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, decimal val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, object val)
        {
            this.Row.SetValByKey(attrKey, val);
        }

        public void SetValByDesc(string attrDesc, object val)
        {
            if (val == null)
                throw new Exception("@不能设置属性[" + attrDesc + "]null 值。");
            this.Row.SetValByKey(this.EnMap.GetAttrByDesc(attrDesc).Key, val);
        }

        /// <summary>
        /// 设置关联类型的值
        /// </summary>
        /// <param name="attrKey">attrKey</param>
        /// <param name="val">val</param>
        public void SetValRefTextByKey(string attrKey, object val)
        {
            this.SetValByKey(attrKey + "Text", val);
        }
        /// <summary>
        /// 设置bool类型的值
        /// </summary>
        /// <param name="attrKey">attrKey</param>
        /// <param name="val">val</param>
        public void SetValByKey(string attrKey, bool val)
        {
            if (val)
                this.SetValByKey(attrKey, 1);
            else
                this.SetValByKey(attrKey, 0);
        }
        /// <summary>
        /// 设置默认值
        /// </summary>
        public void SetDefaultVals()
        {
            foreach (Attr attr in this.EnMap.Attrs)
            {
                this.SetValByKey(attr.Key, attr.DefaultVal);
            }
        }
        /// <summary>
        /// 设置日期类型的值
        /// </summary>
        /// <param name="attrKey">attrKey</param>
        /// <param name="val">val</param>
        public void SetDateValByKey(string attrKey, string val)
        {
            try
            {
                this.SetValByKey(attrKey, DataType.StringToDateStr(val));
            }
            catch (System.Exception ex)
            {
                throw new Exception("@不合法的日期数据格式:key=[" + attrKey + "],value=" + val + " " + ex.Message);
            }
        }
        #endregion

        #region 取值方法
        /// <summary>
        /// 取得Object
        /// </summary>
        /// <param name="attrKey"></param>
        /// <returns></returns>
        public Object GetValByKey(string attrKey)
        {
            return this.Row.GetValByKey(attrKey);

            //try
            //{
            //    return this.Row.GetValByKey(attrKey);				
            //}
            //catch(Exception ex)
            //{
            //    throw new Exception(ex.Message+"  "+attrKey+" EnsName="+this.ToString() );
            //}
        }
        /// <summary>
        /// GetValDateTime
        /// </summary>
        /// <param name="attrKey"></param>
        /// <returns></returns>
        public DateTime GetValDateTime(string attrKey)
        {
            return DataType.ParseSysDateTime2DateTime(this.GetValStringByKey(attrKey));
        }
        /// <summary>
        /// 在确定  attrKey 存在 map 的情况下才能使用它
        /// </summary>
        /// <param name="attrKey"></param>
        /// <returns></returns>
        public string GetValStrByKey(string key)
        {
            return this.Row.GetValByKey(key).ToString();
        }
        public string GetValStrByKey(string key, string isNullAs)
        {
            try
            {
                return this.Row.GetValByKey(key).ToString();
            }
            catch
            {
                return isNullAs;
            }
        }
        /// <summary>
        /// 取得String
        /// </summary>
        /// <param name="attrKey"></param>
        /// <returns></returns>
        public string GetValStringByKey(string attrKey)
        {
            switch (attrKey)
            {
                case "Doc":
                    string s = this.Row.GetValByKey(attrKey).ToString();
                    if (s == "")
                        s = this.GetValDocText();
                    return s;
                default:
                    try
                    {
                        if (this.Row == null)
                            throw new Exception("@没有初始化Row.");
                        return this.Row.GetValByKey(attrKey).ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@获取值期间出现如下异常：" + ex.Message + "  " + attrKey + " 您没有在类增加这个属性，EnsName=" + this.ToString());
                    }
                    break;
            }
        }
        public string GetValStringByKey(string attrKey, string defVal)
        {
            string val = this.GetValStringByKey(attrKey);
            if (val == null || val == "")
                return defVal;
            else
                return val;
        }
        /// <summary>
        ///  取出大块文本
        /// </summary>
        /// <returns></returns>
        public string GetValDocText()
        {
            string s = this.GetValStrByKey("Doc");
            if (s.Trim().Length != 0)
                return s;

            s = SysDocFile.GetValTextV2(this.ToString(), this.PKVal.ToString());
            this.SetValByKey("Doc", s);
            return s;
        }
        public string GetValDocHtml()
        {
            string s = this.GetValHtmlStringByKey("Doc");
            if (s.Trim().Length != 0)
                return s;

            s = SysDocFile.GetValHtmlV2(this.ToString(), this.PKVal.ToString());
            this.SetValByKey("Doc", s);
            return s;
        }
        /// <summary>
        /// 取到Html 信息。
        /// </summary>
        /// <param name="attrKey">attr</param>
        /// <returns>html.</returns>
        public string GetValHtmlStringByKey(string attrKey)
        {
            return DataType.ParseText2Html(this.GetValStringByKey(attrKey));
        }
        public string GetValHtmlStringByKey(string attrKey, string defval)
        {
            return DataType.ParseText2Html(this.GetValStringByKey(attrKey, defval));
        }
        /// <summary>
        /// 取得枚举或者外键的标签
        /// 如果是枚举就获取枚举标签.
        /// 如果是外键就获取为外键的名称.
        /// </summary>
        /// <param name="attrKey"></param>
        /// <returns></returns>
        public string GetValRefTextByKey(string attrKey)
        {
            string str = "";
            try
            {
                str = this.Row.GetValByKey(attrKey + "Text") as string;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + attrKey);
            }
            return str;
        }
        public Int64 GetValInt64ByKey(string key)
        {
            return Int64.Parse(this.GetValStringByKey(key));
        }
        public int GetValIntByKey(string key, int IsZeroAs)
        {
            int i = this.GetValIntByKey(key);
            if (i == 0)
                i = IsZeroAs;
            return i;
        }
        /// <summary>
        /// 根据key 得到int val
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetValIntByKey(string key)
        {
            try
            {
                return int.Parse(this.GetValStrByKey(key));
            }
            catch (Exception ex)
            {
                string v = this.GetValStrByKey(key).ToLower();
                if (v == "true")
                {
                    this.SetValByKey(key, 1);
                    return 1;
                }
                if (v == "false")
                {
                    this.SetValByKey(key, 0);
                    return 0;
                }

                if (key == "OID")
                {
                    this.SetValByKey("OID", 0);
                    return 0;
                }

                if (this.GetValStringByKey(key) == "")
                    return int.Parse(this.EnMap.GetAttrByKey(key).DefaultVal.ToString());

                if (SystemConfig.IsDebug == false)
                    throw new Exception("@[" + this.EnMap.GetAttrByKey(key).Desc + "]请输入数字，您输入的是[" + this.GetValStrByKey(key) + "]。");
                else
                    throw new Exception("@表[" + this.EnDesc + "]在获取属性[" + key + "]值,出现错误，不能将[" + this.GetValStringByKey(key) + "]转换为int类型.错误信息：" + ex.Message + "@请检查是否在存储枚举类型时，您在SetValbyKey中没有转换。正确做法是:this.SetValByKey( Key ,(int)value)  ");
            }
        }
        /// <summary>
        /// 根据key 得到 bool val
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetValBooleanByKey(string key)
        {
            if (this.GetValStringByKey(key).ToUpper() == "FALSE")
                return false;
            if (this.GetValStringByKey(key).ToUpper() == "TRUE")
                return true;
            if (int.Parse(this.GetValStringByKey(key)) == 0)
                return false;
            else
                return true;
        }
        public bool GetValBooleanByKey(string key, bool defval)
        {
            try
            {

                if (int.Parse(this.GetValStringByKey(key)) == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return defval;
            }
        }
        public string GetValBoolStrByKey(string key)
        {
            if (int.Parse(this.GetValStringByKey(key)) == 0)
                return "否";
            else
                return "是";
        }
        /// <summary>
        /// 根据key 得到flaot val
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public float GetValFloatByKey(string key, int blNum)
        {
            return float.Parse(float.Parse(this.Row.GetValByKey(key).ToString()).ToString("0.00"));
        }
        /// <summary>
        /// 根据key 得到flaot val
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public float GetValFloatByKey(string key)
        {
            return float.Parse(float.Parse(this.Row.GetValByKey(key).ToString()).ToString("0.00"));
        }
        public decimal GetValMoneyByKey(string key)
        {
            return decimal.Parse(this.GetValDecimalByKey(key).ToString("0.00"));
        }
        /// <summary>
        /// 根据key 得到flaot val
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public decimal GetValDecimalByKey(string key)
        {
            return decimal.Round(decimal.Parse(this.GetValStrByKey(key)), 4);
        }
        public decimal GetValDecimalByKey(string key, string items)
        {
            if (items == "" || items == null)
                return 0;

            if (items.IndexOf("@" + key) == -1)
                return 0;

            string str = items.Substring(items.IndexOf("@" + key));

            return decimal.Round(decimal.Parse(this.GetValStringByKey(key)), 4);
        }
        public double GetValDoubleByKey(string key)
        {
            try
            {
                return double.Parse(this.GetValStrByKey(key));
            }
            catch (Exception ex)
            {
                throw new Exception("@表[" + this.EnDesc + "]在获取属性[" + key + "]值,出现错误，不能将[" + this.GetValStringByKey(key) + "]转换为double类型.错误信息：" + ex.Message);
            }
        }
        public string GetValAppDateByKey(string key)
        {
            try
            {
                string str = this.GetValStringByKey(key);
                if (str == null || str == "")
                    return str;
                return DataType.StringToDateStr(str);
            }
            catch (System.Exception ex)
            {
                throw new Exception("@实例：[" + this.EnMap.EnDesc + "] " + " 属性[" + key + "]值[" + this.GetValStringByKey(key).ToString() + "]日期格式转换出现错误：" + ex.Message);
            }
            //return "2003-08-01";
        }
        #endregion

        #endregion

        #region 获取配置信息
        public string GetCfgValStr(string key)
        {
            return BP.Sys.EnsAppCfgs.GetValString(this.ToString() + "s", key);
        }

        public int GetCfgValInt(string key)
        {
            return BP.Sys.EnsAppCfgs.GetValInt(this.ToString() + "s", key);
        }

        public bool GetCfgValBoolen(string key)
        {
            return BP.Sys.EnsAppCfgs.GetValBoolen(this.ToString() + "s", key);
        }
        public void SetCfgVal(string key, object val)
        {
            BP.Sys.EnsAppCfg cfg = new EnsAppCfg();
            cfg.MyPK = this.ToString() + "s@" + key;
            cfg.CfgKey = key;
            cfg.CfgVal = val.ToString();
            cfg.EnsName = this.ToString() + "s";
            cfg.Save();
        }
        #endregion



        #region 属性
        /// <summary>
        /// 文件管理者
        /// </summary>
        public SysFileManagers HisSysFileManagers
        {
            get
            {
                return new SysFileManagers(this.ToString(), this.PKVal.ToString());
            }
        }
        public bool IsBlank
        {
            get
            {
                if (this._row == null)
                    return true;

                Attrs attrs = this.EnMap.Attrs;
                foreach (Attr attr in attrs)
                {
                    string str = this.GetValStrByKey(attr.Key);
                    if (str == "" || str == attr.DefaultVal.ToString() || str == null)
                        continue;
                    if (str == attr.DefaultVal.ToString())
                        continue;

                    if (attr.IsEnum)
                    {
                        if (attr.DefaultVal.ToString() == str)
                            continue;
                        else
                            return false;
                        continue;
                    }

                    if (attr.IsFKorEnum)
                    {
                        //if (attr.DefaultVal == null || attr.DefaultVal == "")
                        //    continue;

                        if (attr.DefaultVal.ToString() != str)
                            return false;
                        else
                            continue;
                    }

                    if (str != attr.DefaultVal.ToString())
                        return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 获取或者设置
        /// 是不是空的实体.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (this._row == null)
                {
                    return true;
                }
                else
                {
                    if (this.PKVal == null || this.PKVal.ToString() == "0" || this.PKVal.ToString() == "")
                        return true;
                    return false;
                }
            }
            set
            {
                this._row = null;
            }
        }
        /// <summary>
        /// 对这个实体的描述
        /// </summary>
        public String EnDesc
        {
            get
            {
                return this.EnMap.EnDesc;
            }
        }
        /// <summary>
        /// 取到主健值。如果它的主健不唯一，就返回第一个值。
        /// 获取或设置
        /// </summary>
        public Object PKVal
        {
            get
            {
                return this.GetValByKey(this.PK);
            }
            set
            {
                this.SetValByKey(this.PK, value);
            }
        }
        /// <summary>
        /// 如果只有一个主键,就返回PK,如果有多个就返回第一个.PK
        /// </summary>
        public int PKCount
        {
            get
            {
                switch (this.PK)
                {
                    case "OID":
                    case "No":
                    case "MyPK":
                        return 1;
                    default:
                        break;
                }

                int i = 0;
                foreach (Attr attr in this.EnMap.Attrs)
                {
                    if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.PKFK)
                        i++;
                }
                if (i == 0)
                    throw new Exception("@没有给【" + this.EnDesc + "，" + this.EnMap.PhysicsTable + "】定义主键。");
                else
                    return i;
            }
        }
        /// <summary>
        /// 是不是OIDEntity
        /// </summary>
        public bool IsOIDEntity
        {
            get
            {
                if (this.PK == "OID")
                    return true;
                return false;
            }
        }
        /// <summary>
        /// 是不是OIDEntity
        /// </summary>
        public bool IsNoEntity
        {
            get
            {
                if (this.PK == "No")
                    return true;
                return false;
            }
        }
        /// <summary>
        /// 是不是IsMIDEntity
        /// </summary>
        public bool IsMIDEntity
        {
            get
            {
                if (this.PK == "MID")
                    return true;
                return false;
            }
        }
        /// <summary>
        /// 如果只有一个主键,就返回PK,如果有多个就返回第一个.PK
        /// </summary>
        public virtual string PK
        {
            get
            {
                // throw new Exception(this.ToString()+"求PK");
                foreach (Attr attr in this.EnMap.Attrs)
                {
                    if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.PKFK)
                        return attr.Key;
                }
                throw new Exception("@没有给【" + this.EnDesc + "，" + this.EnMap.PhysicsTable + "】定义主键。");
                // throw new Exception("@没有给【" + this.EnDesc + "】定义主键。");
            }
        }
        public virtual string PKField
        {
            get
            {
                // throw new Exception(this.ToString()+"求PK");
                foreach (Attr attr in this.EnMap.Attrs)
                {
                    if (attr.MyFieldType == FieldType.PK
                        || attr.MyFieldType == FieldType.PKEnum
                        || attr.MyFieldType == FieldType.PKFK)
                        return attr.Field;
                }
                throw new Exception("@没有给【" + this.EnDesc + "】定义主键。");
            }
        }
        /// <summary>
        /// 如果只有一个主键,就返回PK,如果有多个就返回第一个.PK
        /// </summary>
        public string[] PKs
        {
            get
            {
                string[] strs1 = new string[this.PKCount];
                int i = 0;
                foreach (Attr attr in this.EnMap.Attrs)
                {
                    if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.PKFK)
                    {
                        strs1[i] = attr.Key;
                        i++;
                    }
                }
                return strs1;
            }
        }
        /// <summary>
        /// 取到主健值。
        /// </summary>
        public Hashtable PKVals
        {
            get
            {
                Hashtable ht = new Hashtable();
                string[] strs = this.PKs;
                foreach (string str in strs)
                {
                    ht.Add(str, this.GetValStringByKey(str));
                }
                return ht;
            }
        }
        #endregion


        public void domens()
        {
        }

    }

}
