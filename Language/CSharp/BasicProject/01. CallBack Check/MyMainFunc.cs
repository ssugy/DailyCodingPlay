using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicProject._01._CallBack_Check
{
    internal class MyMainFunc
    {
        public static void _Main()
        {
            MyCallBack myCallBack = new MyCallBack();
            MyDosomething myDosomething = new MyDosomething();

            // 이 방식이 유니티에서는 가능한데, C# 메인함수(스태틱)에서는 불가.
            // 근본적으로 메모리 호출에 대한 순서 문제때문임. this는 참조에서 사용하는 것이고, 여기 Main함수는 static 메모리 구간이다.
            //myDosomething.DoSomething(this);
            myDosomething.DoSomething(myCallBack);  //이렇게 사용해야됨.

        }
    }
}
