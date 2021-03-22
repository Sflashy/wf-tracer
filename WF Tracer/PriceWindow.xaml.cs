using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WFTracer
{
    /// <summary>
    /// Interaction logic for PriceWindow.xaml
    /// </summary>
    public partial class PriceWindow : Window
    {
        public PriceWindow()
        {
            InitializeComponent();
        }

        private void HideMe(object sender, MouseButtonEventArgs e) => Hide();

        private void HMouseEnter(object sender, MouseEventArgs e) => HideApp.Background = new SolidColorBrush(Color.FromRgb(200, 10, 10));

        private void HMouseLeave(object sender, MouseEventArgs e) => HideApp.Background = Brushes.Transparent;

        private void DragMe(object sender, MouseButtonEventArgs e) => DragMove();
    }
}
