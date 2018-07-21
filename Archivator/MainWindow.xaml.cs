using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Archivator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    // А название Archiver или Archivator чё бы не ?
    // Я всё в ReadMe.md хотел описать, но похоже мой загрузчик в гит сломался
    class Binary
    {
        public static string ToBinConvert(char arg)
        {
            string ret;
            ret = Convert.ToString(arg, 2); //обычная функция получения бинарного кода
            while (ret.Length < 8)
            {
                ret = '0' + ret; //дополнение длины до 8
            }
            return ret;

        }
        public static string ToBinConvert(string arg)
        {
            string ret = "";
            for (byte i = 0; i < arg.Length; i++)
            {
                ret += ToBinConvert(arg[i]);
            }
            return ret;
        }

        public static char FromBinConvert(string arg)
        {
            int bt = 0;
            for (byte i = 0; i < 8; i++)
            {
                if (arg[i] == '1')
                {
                    bt += (int)(Math.Pow(2, 7 - i));
                }
            }
            return (char)(bt);
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FullSizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            { WindowState = WindowState.Maximized; }
            else WindowState = WindowState.Normal;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
