using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiOu.Cloud.Service.Constants
{
    internal class ChannelDefaults
    {
        public const string UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1";

        public const string DouYin = "DY";
    }

    public enum PayType
    {
        [Description("微信支付")]
        wx,
        [Description("支付宝")]
        alipay

    }
}
