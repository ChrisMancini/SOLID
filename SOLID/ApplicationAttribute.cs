using System;

namespace SOLID
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ApplicationAttribute : Attribute
    {
        public ApplicationAttribute()
        {
            Order = int.MaxValue;
            Available = true;
        }

        public int Order { get; set; }
        public string Name { get; set; }
        public bool Available { get; set; }
    }
}
