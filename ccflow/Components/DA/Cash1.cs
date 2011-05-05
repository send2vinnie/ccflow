using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace BP.DA
{
    public class Cash1
    {
        #region SQL cash
        private static Hashtable _Cash;
        public static Hashtable DCash
        {
            get
            {
                if (_Cash == null)
                    _Cash = new Hashtable();
                return _Cash;
            }
        }
        #endregion

        public static void Set(string cashKey, string key, object o, int maxNum)
        {
            Hashtable ht = Cash1.DCash[cashKey] as Hashtable;
            if (ht == null)
            {
                ht = new Hashtable();
                Cash1.DCash[cashKey] = ht;
            }
            ht[key] = o;
        }
        public static object Get(string cashKey, string key)
        {
            Hashtable ht = Cash1.DCash[cashKey] as Hashtable;
            if (ht == null)
                return null;

            return ht[key];
        }
        public static void Remove(string cashKey, string key)
        {
            Hashtable ht = Cash1.DCash[cashKey] as Hashtable;
            if (ht == null)
            {
                Log.DefaultLogWriteLineWarning("不应该出现。");
                return;
            }
            ht.Remove(key);
        }
    }
}
