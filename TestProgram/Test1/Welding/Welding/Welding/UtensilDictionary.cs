using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Welding
{
    /// <summary>
    /// 用具字典，通过EPC标签查用具类型
    /// </summary>
    public class UtensilDictionary
    {
        Dictionary<string, Utensil> dictionary = new Dictionary<string, Utensil>();

        public UtensilDictionary()
        {
            Load();
        }
        /// <summary>
        /// 通过EPC标签查用具类型
        /// </summary>
        /// <param name="key">标签</param>
        /// <returns></returns>
        public Utensil? LookUp(string key)
        {
            if (dictionary.Keys.Contains(key))
            {
                return dictionary[key];
            }
            return null;
        }

        private void Load()
        {
            dictionary.Clear();

            var utensil = (IDictionary)ConfigurationManager.GetSection("utensil");

            foreach (string key in utensil.Keys)
            {
                dictionary.Add(key, (Utensil)Enum.Parse(typeof(Utensil), (string)utensil[key]));
            }
        }
    }
}
