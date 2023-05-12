using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicProject._01._CallBack_Check
{
    internal class MyDosomething
    {
        // 3. 콜백함수를 실행할 지점에 관한 함수 구현
        public void DoSomething(ICallBackCheck callback)
        {
            // 4. 내가 할 일 들 진행
            Console.WriteLine("내가 할 일 여러가지 진행");

            // 5. 할일을 마치고 콜백함수를 실행
            callback.CallbackCheck("콜백함수를 실행");
        }
    }
}
