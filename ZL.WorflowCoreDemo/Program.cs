using System;

namespace ZL.WorflowCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Basic.FlowRun.Run();
            //InputDataToStep.FlowRunDynamic.Run();

            Console.WriteLine(MyFunction((x,y) => x + y,10));

            
            
        }

        static int MyFunction(Func<int,int, int> ex,int x)
        {
            return ex(x,x);
        }
    }
}
