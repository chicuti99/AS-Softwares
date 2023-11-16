using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderSheetPrinter.Auxiliar
{
    public class AuxEnum
    {
        public static string GetDescriptionFromValue(Type type, object value)
        {
            string result = "";

            try
            {
                result = Enum.GetName((value).GetType(), value);
                FieldInfo fieldInfo = type.GetField(result);
                object[] attributeArray = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                DescriptionAttribute attribute = null;
                if (attributeArray.Length > 0)
                    attribute = (DescriptionAttribute)attributeArray[0];
                if (attribute != null)
                    result = attribute.Description;
            }
            catch (ArgumentNullException)
            {
                result = value.ToString();
            }
            catch (ArgumentException)
            {
                result = value.ToString();
            }

            return result;
        }
        public static void PopulaCombo(ComboBox cmb, Type tipo)
        {
            cmb.Items.Clear();
            foreach (var enm in tipo.GetFields(BindingFlags.Public | BindingFlags.Static).Select(x => Enum.Parse(tipo, x.Name)).ToArray())
            {
                cmb.Items.Add(enm);
            }

            cmb.SelectedIndex = 0;
        }
       

        public enum DimensoImpressora
        {

            [Description("58mm")]
            _58mm = 0,

            [Description("80mm")]
            _80mm = 1
        }
    }

    public static class ExtensionsEnum
    {

        public static short StringToShort(this string value, short _default = -1)
        {
            short i;

            if (short.TryParse(value, out i))
                return i;
            else
                return _default;
        }
    }
}
