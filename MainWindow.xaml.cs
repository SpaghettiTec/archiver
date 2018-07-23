using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

    internal static class SymbolTree //дерево символов, кодировка, загрузка
    {
        internal class Tree
        { //НЕ СМОТРИ СЮДА МНЕ ПРОСТО ЛЕНЬ ДЕЛАТЬ ДЕРЕВО 
            // подкласс "элемент дерева"
            public class TreeNode
            { //НУ ПИЗДЕЦ Я ОПОЗОРЕН
                public char Character; // численное значение
                public TreeNode Left; // левое поддерево
                public TreeNode Right; // правое поддерево
            }
        //public TreeNode Node; // экземпляр         
        }
        public static string FindCodeInTree(char symbol)
        {
            return "oops"; //Да пошло оно
        }
        public static void IncreaseTheTree(string arg, Tree curTree)
        {
            
        }

    }

    class FileOperator //работа с файлами
    {

        //string CurrentAdress;
        public static void EncodeFile(string fileAdress) //заготовка метода кодирования файла
        {
            //сделать tryCatch
            // составить таблицу/дерево встречаемых символов FormEncodingTree 
            // применить таблицу для текста, заменив символы
            ///можно сделать перегруженную функцию для кодировки/раскодировки в зависимости от строка/символ
            ///скорее всего плохая идея
            //загрузить дерево в файл pushTree
            //создать .kusokGovna
            //работа с reader
            //закрываем поток
            

            bool debugIsOn = false;
            string tempString=""; 
            string tempCode="";
            string newFileAdress = fileAdress.Remove(fileAdress.Length - 4); newFileAdress += ".kusokGovna"; //замена адреса
            SymbolTree.Tree tree = new SymbolTree.Tree();//ну наверное как то так 
            //тут кароч переменную типа дерева обьявим, я потом с деревьями здесь отдельно разберусь, после целого симака ебучих деревьевС++ 
            StreamReader readFile = new StreamReader(File.Open(fileAdress, FileMode.Open, FileAccess.Read));
            while(readFile.ReadLine() != null)
            {
                if (debugIsOn) //ну если сделаешь окошко для дебага
                {
                    //Console.WriteLine(readFile.ReadLine()); 
                }                
                SymbolTree.IncreaseTheTree(readFile.ReadLine(), tree);

            }
            readFile.Close();
            tempString = "";

            StreamWriter writeFile = new StreamWriter(File.Open(fileAdress, FileMode.Create, FileAccess.Write));

            readFile = new StreamReader(File.Open(fileAdress, FileMode.Open, FileAccess.Read));

            while (readFile.ReadLine() != null) //нужно в ВПФ забахать дебаг окошко для вывода текста
            {
                if (debugIsOn) //ну если сделаешь окошоко для дебага
                {
                    //Console.WriteLine(readFile.ReadLine()); 
                }
                tempString = readFile.ReadLine(); //строку передаем 
                for (int i = 0; i < tempString.Length; i++)
                {
                    tempCode = SymbolTree.FindCodeInTree(tempString[i]); //получаем сжатый код из дерева 
                    if (tempCode.Length > 8) 
                    {
                        writeFile.Write(Binary.FromBinConvert(tempCode.Substring(0, 7))); //жрять жри его
                        //надеюсь никакую хуйню не забыл чот уже спать хочу
                        tempCode.Substring(8); //экспроприация
                    }
                    //сравнение 
                }
            }
            if(tempCode.Length >0)
            {
                Binary.ModifyCode(tempCode);
                writeFile.Write(Binary.FromBinConvert(tempCode));
            }
            readFile.Close();
            File.Delete(fileAdress);
            //write smtg
            writeFile.Close();
        }

        public static void DecodeFile(string fileAdress)
        {
            BinaryReader _curFileReader = new BinaryReader(File.Open(fileAdress, FileMode.Open, FileAccess.Read));
            //потом потещу как лучше сыграть с бинарными файлами
            //тут надо будет считать длину таблицы/дерева 
            while(_curFileReader.PeekChar() > -1 ) //проверка конца файла
            {
                //по символу, наверное бесполезно
                //что то делаем с _curFile.ReadChar()
            }
            _curFileReader.Close();
            
            //сделать tryCatch     

            //выгрузить из файла дерево getTree
            //декодировать используя дерево
            //заменить тип файла

        }


    }

    internal static class Binary //бинарные операции
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
        public static string ModifyCode(string binCode) //добивает код до восьми символов
        {
            //да я знаю выделять под единственную (вроде) операцию целый метод в другом классе это странно
            //да я извращенец
            for(int i = binCode.Length; i<8; i++)
            {
                binCode += '0';
            }            
            return binCode;
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



