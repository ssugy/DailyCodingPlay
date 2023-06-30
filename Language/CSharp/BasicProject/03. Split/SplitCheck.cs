using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicProject._03._Split
{
    internal class SplitCheck
    {
        public static void _Main(string[] args)
        {
            //string str = null;        // 이렇게 null인경우는 split사용하면 널익셉션 에러 난다.
            string str = "234532";
            Console.WriteLine(str.Split('_').Length);   // 분리가 안되면 그 자체가 나타나는거라서 1이 나온다. 1 미만은 나올 수 없음.
            Console.WriteLine(str.Split('_')[0]);   // 분리안되면 자체가 나옴. -> 초기화 단계가 필요한듯.
        }
    }
}
