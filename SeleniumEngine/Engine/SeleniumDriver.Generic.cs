using CoreLibrary.Controls;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Selenium.Engine
{
    public partial class SeleniumDriver : IGeneric
    {

        public string GetDescription(Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    var attr =
                        Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                            as DescriptionAttribute;
                    return attr != null ? attr.Description : value.ToString();
                }
            }
            return null;
        }

    }
}
