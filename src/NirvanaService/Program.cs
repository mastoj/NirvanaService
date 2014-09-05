using Topshelf;

namespace NirvanaService
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceName = "";
            HostFactory.Run(x =>
            {
                x.AddCommandLineDefinition("sname", f => { serviceName = f; });
                x.ApplyCommandLine();

                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.EnableShutdown();
                x.EnableServiceRecovery(c => c.RestartService(1));

                x.Service<ServiceWrapper>(s =>
                {
                    s.ConstructUsing(() => new ServiceWrapper(serviceName));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
            });
        }
    }
}
