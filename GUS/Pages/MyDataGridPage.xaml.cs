using GUS.Core;
using System.Windows.Controls;

namespace GUS
{
    /// <summary>
    /// Interaction logic for DataGrid.xaml
    /// </summary>
    public partial class MyDataGridPage : Page
    {
        public MyDataGridPage()
        {
            InitializeComponent();

            DataContext = new MyDataGridPageViewModel();
        }
    }
}
