namespace Solution
{
    using System;
    using Akka.Actor;


    public class ChildActor : ReceiveActor
    {
        public ChildActor()
        {
            Receive<string>(e => Console.WriteLine(e));
        }
    }
}
