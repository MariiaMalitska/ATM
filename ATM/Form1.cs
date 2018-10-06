using System.Windows.Forms;

//зв"язок інтерфейсу та класу
namespace ATM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
