namespace Solution
{
    using Akka.Actor;
    using Akka.DI.Core;

    public class ParentActor : ReceiveActor
    {
        private readonly IActorRef _childActor;

        public ParentActor()
        {
            _childActor = Context.ActorOf(Context.DI().Props<ChildActor>());
            Receive<string>(Do);
            Receive<object>(e=>Sender.Tell(e));
        }

        private bool Do(string obj)
        {
            _childActor.Tell(obj);
            return true;
        }
    }
}