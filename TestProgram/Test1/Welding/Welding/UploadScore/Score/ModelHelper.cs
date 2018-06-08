namespace JAAJ.Common
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Reflection;
    using System.Windows.Forms;

    public class ModelHelper
    {
        public static Hashtable GetPropertiesFromInfoByFieldType(object obj, FieldType type)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            Hashtable hashtable = new Hashtable();
            foreach (PropertyInfo info in properties)
            {
                DataAttribute[] customAttributes = (DataAttribute[]) info.GetCustomAttributes(typeof(DataAttribute), true);
                foreach (DataAttribute attribute in customAttributes)
                {
                    int num = (int) attribute.Type;
                    int num2 = (int) type;
                    if (((num ^ num2) & num2) == 0)
                    {
                        hashtable.Add(info.Name.ToLower(), info);
                    }
                }
            }
            return hashtable;
        }

        public static void LoadInfoData(object obj, IDataReader reader)
        {
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                hashtable.Add(reader.GetName(i).ToLower(), reader.GetValue(i));
            }
            Hashtable propertiesFromInfoByFieldType = GetPropertiesFromInfoByFieldType(obj, FieldType.DBField);
            foreach (object obj2 in propertiesFromInfoByFieldType.Keys)
            {
                PropertyInfo p = (PropertyInfo) propertiesFromInfoByFieldType[obj2];
                object v = null;
                if (hashtable.Contains(obj2))
                {
                    v = hashtable[obj2];
                }
                if (v != null)
                {
                    SetPropertyValue(ref obj, ref p, ref v);
                }
            }
        }

        protected static void SetPropertyValue(ref object obj, ref PropertyInfo p, ref object v)
        {
            try
            {
                if (p.PropertyType == typeof(int))
                {
                    if (v == null || v.ToString() == "")
                    {
                        v = "0";
                    }
                    p.SetValue(obj, int.Parse(v.ToString()), null);
                }
                else if (p.PropertyType == typeof(decimal))
                {
                    p.SetValue(obj, decimal.Parse(v.ToString()), null);
                }
                else if (p.PropertyType == typeof(Guid))
                {
                    p.SetValue(obj, new Guid(v.ToString()), null);
                }
                else if (p.PropertyType == typeof(float))
                {
                    p.SetValue(obj, float.Parse(v.ToString()), null);
                }
                else if (p.PropertyType == typeof(double))
                {
                    p.SetValue(obj, double.Parse(v.ToString()), null);
                }
                else if (p.PropertyType == typeof(DateTime))
                {
                    if (v == null ||v.ToString()=="")
                    {
                        p.SetValue(obj, DateTime.Now, null);
                    }
                    else
                    {
                        p.SetValue(obj, DateTime.Parse(v.ToString()), null);
                    }
                }
                else if (p.PropertyType == typeof(bool))
                {
                  
                    p.SetValue(obj, bool.Parse(v.ToString()), null);
                }
                else if (p.PropertyType == typeof(byte))
                {

                    p.SetValue(obj, byte.Parse(v.ToString()), null);
                }
                else if (p.PropertyType == typeof(byte[]))
                {
                    if (v == null || v.ToString() == "")
                    {
                        p.SetValue(obj, null, null);
                    }
                    else 
                    {
                        p.SetValue(obj, (byte[])v, null);
                    }
                }
                else
                {
                    if (v == null)
                    { 
                        v = "";
                    }
                    p.SetValue(obj, v.ToString(), null);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("属性赋值发生异常" + exception.Message, exception);
            }
        }
    }
}

