using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using RPI.Scheduler.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPI.Scheduler
{
    public class JobService
    {
        
        public static void Initially()
        {
            ////工厂
            //ISchedulerFactory factory = new StdSchedulerFactory();
            ////启动
            //IScheduler scheduler = factory.GetScheduler();
            //scheduler.Start();
            ////描述工作
            //IJobDetail jobDetail = new JobDetailImpl("mylittlejob", null, typeof(MyJob));
            ////触发器
            //ISimpleTrigger trigger = new SimpleTriggerImpl("mytrigger",
            //    null,
            //    DateTime.Now,
            //    null,
            //    SimpleTriggerImpl.RepeatIndefinitely,
            //    TimeSpan.FromSeconds(10));
            ////执行
            //scheduler.ScheduleJob(jobDetail, trigger);


            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<DHT11Job>().Build();
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("triggerName", "groupName")
              .WithSimpleSchedule(t =>
                t.WithIntervalInSeconds(5)
                 .RepeatForever())
                 .Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}
