using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
            builder.RegisterType<InteractionInformation>().As<IInteractionInformation>();
            builder.RegisterType<MainForm>().As<IMainForm>().As<MainForm>();
            //builder.RegisterType<Form>();

            Container = builder.Build();
        }
    }
}
