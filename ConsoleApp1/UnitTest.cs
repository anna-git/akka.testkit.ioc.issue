namespace ConsoleApp1
{
    using System.Threading;
    using Akka.Actor;
    using Akka.DI.AutoFac;
    using Akka.TestKit;
    using Akka.TestKit.NUnit;
    using Autofac;
    using NUnit.Framework;

    [TestFixture]
    public class UnitTest:TestKit
    {
        private StubChildActor _childstub;

        [SetUp]
        public void SetUpContainer()
        {
            var container = new ContainerBuilder();
            _childstub = new StubChildActor(CreateTestProbe());
            container.RegisterInstance(_childstub).As<ChildActor>();
            var build = container.Build();
            new AutoFacDependencyResolver(build, Sys);
        }

        [Test]
        public void Test()
        {
            var actorRef = Sys.ActorOf(Props.Create<ParentActor>(), "ParentActor");
            actorRef.Tell("hi");
            _childstub.Probe.ExpectMsg<string>();
        }
    }
}
