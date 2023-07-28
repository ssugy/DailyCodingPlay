namespace CheckFileSize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RootPath_TextChanged(object sender, EventArgs e)
        {

        }

        // 용량 기입하는 곳
        private void SizeCapacity_TextChanged(object sender, EventArgs e)
        {

            Console.WriteLine("텍스트3");
        }

        // 용량기입 옆에 있는 버튼
        private void FileSizeChangeBtn(object sender, EventArgs e)
        {
            OverSizeFiles.Clear();
            ShowExceedFileList(RootPathBox.Text, float.Parse(SizeCapacity.Text));
        }

        // 로그 출력
        private void LogBox_TextChanged(object sender, EventArgs e)
        {

        }

        // 파일사이즈 넘치는 것들에 대한 리스트
        private void OverSizeFiles_TextChanged(object sender, EventArgs e)
        {

        }

        //-------------- 파일사이즈 체크 
        public void ShowExceedFileList(string rootPath, float cutLineFileSize)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(rootPath);

            foreach (FileInfo fi in dirInfo.GetFiles("*", SearchOption.AllDirectories))
            {
                if (fi.Length > 1024 * 1024 * cutLineFileSize)
                    OverSizeFiles.Text += "파일 리스트 : " + fi.FullName + " ||  " + fi.Length / (1024 * 1024) + " mb" + "\r\n";
            }
        }

        private void ClearSizeList_Click(object sender, EventArgs e)
        {
            OverSizeFiles.Clear();
        }

        private void IsGitIgnoreOn_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}