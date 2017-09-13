namespace Solution
{
    using System.Threading;
    using Akka.Actor;
    using Akka.DI.AutoFac;
    using Akka.TestKit;
    using Akka.TestKit.NUnit;
    using Autofac;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class UnitTest:TestKit
    {
        private IContainer build;

        [SetUp]
        public void SetUpContainer()
        {
            var container = new ContainerBuilder();
            container.Register(_ => CreateTestProbe()).As<TestProbe>().InstancePerDependency();
            container.RegisterType<StubChildActor>().AsSelf().As<ChildActor>().SingleInstance(); //for the test's sake, we need to get the same instance.
            build = container.Build();
            new AutoFacDependencyResolver(build, Sys);
        }

        [Test]
        public async Task Test()
        {
            var actorRef = Sys.ActorOf(Props.Create<ParentActor>(), "ParentActor");
            await actorRef.Ask(new object());
            actorRef.Tell("hi");
            build.Resolve<StubChildActor>().Probe.ExpectMsg<string>();
        }
    }
}
