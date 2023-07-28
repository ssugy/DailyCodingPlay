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

        // �뷮 �����ϴ� ��
        private void SizeCapacity_TextChanged(object sender, EventArgs e)
        {

            Console.WriteLine("�ؽ�Ʈ3");
        }

        // �뷮���� ���� �ִ� ��ư
        private void FileSizeChangeBtn(object sender, EventArgs e)
        {
            OverSizeFiles.Clear();
            ShowExceedFileList(RootPathBox.Text, float.Parse(SizeCapacity.Text));
        }

        // �α� ���
        private void LogBox_TextChanged(object sender, EventArgs e)
        {

        }

        // ���ϻ����� ��ġ�� �͵鿡 ���� ����Ʈ
        private void OverSizeFiles_TextChanged(object sender, EventArgs e)
        {

        }

        //-------------- ���ϻ����� üũ 
        public void ShowExceedFileList(string rootPath, float cutLineFileSize)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(rootPath);

            foreach (FileInfo fi in dirInfo.GetFiles("*", SearchOption.AllDirectories))
            {
                if (fi.Length > 1024 * 1024 * cutLineFileSize)
                    OverSizeFiles.Text += "���� ����Ʈ : " + fi.FullName + " ||  " + fi.Length / (1024 * 1024) + " mb" + "\r\n";
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