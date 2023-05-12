using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicProject._01._CallBack_Check
{
    // 2. 인터페이스의 세부적인 내용 정의
    internal class MyCallBack : ICallBackCheck
    {
        public void CallbackCheck(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
