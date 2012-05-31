using System;
using IBM.Data;
using IBM.Data.Informix;
using IBM.Data.Utilities;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient ;
using System.Data;
using System.Data.Common;

namespace BP.DA
{
    /// <summary>
    /// SqlConnection
    /// </summary>
    public class ConnOfInforMix : ConnBase
    {
        public new IfxConnection Conn = null;
        public ConnOfInforMix()
        {
        }
    }
    /// <summary>
    /// SqlConnections
    /// </summary>
    public class ConnOfInforMixs : System.Collections.CollectionBase
    {
        /// <summary>
        /// SqlConnections
        /// </summary>
        public ConnOfInforMixs()
        {
        }
        public void Add(ConnOfInforMix conn)
        {
            this.InnerList.Add(conn);
        }
        /// <summary>
        ///  初始化
        /// </summary>
        public void Init()
        {
            for (int i = 0; i <= 3; i++)
            {
                ConnOfInforMix conn = new ConnOfInforMix();
                conn.IDX = i;
                this.Add(conn);
            }
        }
        public bool isLock = false;
        public ConnOfInforMix GetOne()
        {
            while (isLock)
            {
                ;
            }
            isLock = true;
            foreach (ConnOfInforMix conn in this)
            {
                if (conn.IsUsing == false)
                {
                    if (conn.Conn == null)
                        conn.Conn = new IfxConnection(SystemConfig.AppCenterDSN);
                    conn.Times++;
                    conn.IsUsing = true;
                    isLock = false;
                    return conn;
                }
            }

            //如果没有新的连接。
            ConnOfInforMix nconn = new ConnOfInforMix();
            nconn.IDX = this.Count;
            nconn.Conn = new IfxConnection(SystemConfig.AppCenterDSN);
            nconn.IsUsing = true;
            this.InnerList.Add(nconn);
            isLock = false;
            return nconn;
        }
        /// <summary>
        /// PutPool
        /// </summary>
        /// <param name="conn"></param>
        public void PutPool(ConnBase conn)
        {
            conn.IsUsing = false;
            this.InnerList[conn.IDX] = conn;
        }
    }
}
