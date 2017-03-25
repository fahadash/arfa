using System;
using System.Collections.Generic;
using System.Text;

namespace arfa.Interface.Attributes
{
    public class DescriptionAttribute : Attribute
    {
        public string Description { get; set; }
        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }
    }
}
