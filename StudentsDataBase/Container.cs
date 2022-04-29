using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using UI;

namespace Containers
{
    internal static class ContainerRegistretion
    {
        public static IContainer Container { get; private set; }

        static ContainerRegistretion()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<IInteractionInformation>().As<InteractionInformation>();
            builder.RegisterType<IMainForm>().As<MainForm>();

            Container = builder.Build();
        }
    }
}
