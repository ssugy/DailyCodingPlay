namespace Video_0Color_Fix
{
    using System;
    using System.Diagnostics;

    class Program
    {
        static string absolutePath = "D:\\Videos\\새 폴더\\Videos_test\\ShortDanceVideo";

        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(absolutePath);
            foreach (var item in files)
            {
                FileInfo fileInfo = new FileInfo(item);
                if (fileInfo.Extension.Equals(".mp4"))
                {
                    Console.WriteLine(fileInfo.Name);
                    VideoChangeCMD(fileInfo.Name);   
                } 
            }
        }

        private static void VideoChangeCMD(string _fileName)
        {
            // 폴더 존재 여부 확인
            if (!Directory.Exists(absolutePath + "\\result"))
            {
                // 폴더가 존재하지 않으면 생성
                Directory.CreateDirectory(absolutePath + "\\result");
            }

            var info = new System.Diagnostics.ProcessStartInfo("cmd.exe")
            {
                Arguments = $"/c ffmpeg -i \"{_fileName}\" -color_primaries bt709 -color_trc bt709 -colorspace bt709 -color_range pc -vcodec libx264 -profile:v baseline \"result\\{_fileName}\""
            };

            info.RedirectStandardOutput = true; // 출력 리디렉션
            info.UseShellExecute = false; // 셸 사용 여부
            info.CreateNoWindow = true; // 창 표시 여부
            info.WorkingDirectory = absolutePath;

            // Process 객체 생성 및 시작
            Process process = new Process();
            process.StartInfo = info;
            process.Start();

            // 출력 읽기
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // 출력 결과 표시
            Console.WriteLine(output);
        }
    }

}
