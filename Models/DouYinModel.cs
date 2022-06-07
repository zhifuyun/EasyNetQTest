using YiOu.Cloud.Service.Constants;

namespace YiOu.Cloud.Service.PayChannels
{
    public class TextMessage
    {
        public string Text { get; set; }
    }

    public class CreateChannelOrderDto
    {
        public decimal Amount { get; set; }
        public string PayUserId { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public PayType PayType { get; set; }

        public string Ip { get; set; }
        public int Port { get; set; }


    }

}