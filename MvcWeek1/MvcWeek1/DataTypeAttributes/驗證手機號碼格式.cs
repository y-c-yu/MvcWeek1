using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MvcWeek1.DataTypeAttributes
{
    public class 驗證手機號碼格式Attribute : DataTypeAttribute
    {
        public 驗證手機號碼格式Attribute() : base(DataType.Text)
        {
            ErrorMessage = "手機號碼格式錯誤";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string pattern = @"\d{4}-\d{6}";

            if(!Regex.IsMatch((string)value, pattern))
            {
                return false;
            }

            return base.IsValid(value);
        }
    }
}