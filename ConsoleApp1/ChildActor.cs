namespace ConsoleApp1
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
