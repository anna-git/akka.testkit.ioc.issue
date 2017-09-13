namespace Solution
{
    using Akka.Actor;
    using Akka.TestKit;

    public class StubChildActor : ChildActor
    {
        public StubChildActor(TestProbe probe)
        {
            Probe = probe;
            Become(Probe.Forward);
        }

        public TestProbe Probe { get; set; }
    }
}