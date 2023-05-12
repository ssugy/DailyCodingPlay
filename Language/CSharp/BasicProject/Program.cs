namespace BasicProject
{
    interface ICallback
    {
        void OnCallback();
    }

    class Program : ICallback
    {
        static void _Main(string[] args)
        {
            Program program = new Program();
            program.DoSomething(program);
        }

        public void DoSomething(ICallback callback)
        {
            Console.WriteLine("Do something before callback...");
            callback.OnCallback();
            Console.WriteLine("Do something after callback...");
        }

        public void OnCallback()
        {
            Console.WriteLine("Callback executed!");
        }
    }
}