using System.Windows.Forms;

namespace AWC.Controls
{
    public partial class ucEditExecuteCommandBase : UserControl
    {
        public ucEditExecuteCommandBase()
        {
            InitializeComponent();
        }

        public virtual new void Load(string strCommand)
        {

        }

        public virtual string Accept()
        {
            return "";
        }
    }
}
