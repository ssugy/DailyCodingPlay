using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicProject._05._FileSearch
{
    /// <summary>
    /// git의 LFS를 이용하지 않기 위한 유틸리티
    /// 1. 파일용량이 50MB이상인 것들을 찾아서 리스트업
    /// 2. 해당 파일들에 대해서 
    /// </summary>
    internal class FilesizeCleanUp
    {
        static string folderPath = "C:\\Users\\DevYH\\Pictures\\인제 기적의 도서관";
        static long fileSizeCutLine = 1024 * 50;    // 기준이 되는 파일 사이즈

        static void _Main(string[] args)
        {
            Console.WriteLine("파일사이즈 " + GetDirectorySize(folderPath));
        }

        // 좀 나중에 생각해보자. 이게 이그노어 파일에 들어간 폴더들은 제외시키는 코드가 필요하다. (알고리즘 문제)
        static public long GetDirectorySize(string path)
        {
            long size = 0;
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            // 특정폴더를 제거하고 사용하고 싶다면, 링큐로 쓰면 쉽게 해결된다. - 링큐써도 이중으로 쓰고 복잡해져서 그냥 for문 돌리겠음.
            List<DirectoryInfo> DInfos = dirInfo.GetDirectories("*", SearchOption.AllDirectories).ToList();

            //DInfos.Where(x => (x.FullName.IndexOf("\\test") == -1)
            //               || (x.FullName.IndexOf("\\sample") == -1))

            for (int i = 0; DInfos.Count > i; i++)
            {
                if (DInfos[i].FullName.IndexOf("\\test") == -1)
                {
                    // 여기가 원하는 것들.
                    Console.WriteLine("@@" + DInfos[i].FullName);
                }
                else
                {
                    DInfos.Remove(DInfos[i]);
                    Console.WriteLine(DInfos[i].FullName);
                }
            }

            //foreach (FileInfo fi in dirInfo.GetFiles("*", SearchOption.AllDirectories))
            //{
            //    if (fi.Length > 1024 * 1024 * 50)
            //        Console.WriteLine("파일 리스트 : " + fi.FullName + "|| " + fi.Length / (1024 * 1024));
            //    size += fi.Length;
            //}

            return size;
        }
    }
}
