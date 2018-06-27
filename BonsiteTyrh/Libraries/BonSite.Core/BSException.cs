using System;
using System.Runtime.Serialization;

namespace BonSite.Core
{
    /// <summary>
    /// BonSite异常类
    /// </summary>
    [Serializable]
    public class BSException:ApplicationException
    {
        
        public BSException() { }

        public BSException(string message) : base(message) { }

        public BSException(string message, Exception inner) : base(message, inner) { }

        protected BSException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
