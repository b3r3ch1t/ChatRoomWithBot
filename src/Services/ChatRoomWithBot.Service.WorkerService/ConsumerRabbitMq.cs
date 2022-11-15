using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace ChatRoomWithBot.Service.WorkerService
{
    internal class ConsumerRabbitMq:IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Serilog.Log.Information($"Worker TimerJob iniciado. --> {DateTime.Now }");

            return Task.FromResult(true);
        }
    }
}
