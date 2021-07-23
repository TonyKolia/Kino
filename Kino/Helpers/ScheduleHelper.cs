using Kino.Models;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kino.Models.KinoDBModel;

namespace Kino.API.Helpers
{
    public class KinoDrawJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var newDraw = ServiceHelper.GetDrawFromOPAP();
            using(var db = new KinoDBContext())
            {
                if(!db.Draws.Any(d => d.DrawId == newDraw.DrawId))
                {
                    db.Draws.Add(newDraw);
                    db.SaveChanges();
                }
            }
            System.Diagnostics.Debug.WriteLine(newDraw.DrawId);
            return Task.CompletedTask;
        }
    }

    public static class ScheduleHelper
    {
        public static async Task SetupDrawRetrievalJob()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            IScheduler sched = await schedFact.GetScheduler();

            await sched.Start();

            IJobDetail job = JobBuilder.Create<KinoDrawJob>()
                              .WithIdentity("job1", "group1")
                              .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
                .Build();

            await sched.ScheduleJob(job, trigger);
        }
    }
}
