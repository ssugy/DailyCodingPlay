using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicProject._01._CallBack_Check
{
    // 1. 콜백함수용 인터페이스 원형 생성
    internal interface ICallBackCheck
    {
        void CallbackCheck(string msg);
    }
}
