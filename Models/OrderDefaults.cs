using System;
using System.Collections.Generic;
using System.Text;

namespace YiOu.Cloud.Service.Constants
{
    internal class OrderDefaults
    {
        public static string GenerateCode()
        {
            return DateTime.Now.ToFileTime().ToString();
        }
    }
}
