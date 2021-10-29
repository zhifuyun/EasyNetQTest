using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Newtonsoft.Json;

namespace AspnetCoreDi
{
    public class EasyNetQConsumer:IConsumeAsync<WeatherForecast>
    {
        [AutoSubscriberConsumer(SubscriptionId = "ClientMessageService.Notice")]
        public async Task ConsumeAsync(WeatherForecast message, CancellationToken cancellationToken = new CancellationToken())
        {
            await Task.Run(() =>
            {
                Debug.WriteLine(JsonConvert.SerializeObject(message));
            }, cancellationToken);

        }
    }
}