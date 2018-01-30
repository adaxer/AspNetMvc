using Autofac;
using Southwind.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.DataAccess
{
    public class DAModule : Module
    {
        public string ConnectionString { get; set; } = "nw";

        public string ConnectionMode { get; set; } = "Offline";

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>));
            var registration = builder.Register<ISouthwindDb>(cc => new SouthwindDb($"Name={ConnectionString}", new Dictionary<string, string> { { "Mode", ConnectionMode } }));
            if (ConnectionMode == "Online")
                registration.InstancePerRequest();
            else
                registration.SingleInstance();
        }
    }
}
