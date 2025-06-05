using System;

namespace AttributeLibrary
{
    /// <summary>
    /// Atrybut używany do oznaczania klas, które mają być ukryte w analizie hierarchii dziedziczenia
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HiddenAttribute : Attribute
    {
        public string Reason { get; set; }

        public HiddenAttribute()
        {
        }

        public HiddenAttribute(string reason)
        {
            Reason = reason;
        }
    }
}