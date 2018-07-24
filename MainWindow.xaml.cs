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
            //public TreeNode Node; // экземпляр         B
        }
        public static string FindCodeFromTree(char symbol, Tree tree)
        {
            return Binary.ToBinConvert(symbol);           //просто для теста  
        }
        public static char FindSymbolFromTree(string code)
        {
            return Binary.FromBinConvert(code);
        }
        public static void IncreaseTheTree(string arg, Tree curTree)
        {

        }
        public static string Decode(string binary, Tree curTree)
        {
            string boofString = binary[0] + ""; //ненуачто\
            string ret = "";
            //код в любом случае 2 символа минимум так что вроде так работает
            for (int i = 1; i < binary.Length; i++)
            {
                boofString += binary[i];
                //if похоже на код => FindSymbolFromTree
                ret += Binary.FromBinConvert(binary);


            }
            return ret;
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
            string booferString = "";
            string binaryString = "";
            string newFileAdress = fileAdress.Remove(fileAdress.Length - 4); newFileAdress += ".kusokGovna"; //замена адреса
            SymbolTree.Tree tree = new SymbolTree.Tree();//ну наверное как то так 
            //тут кароч переменную типа дерева обьявим, я потом с деревьями здесь отдельно разберусь, после целого симака ебучих деревьевС++ 
            StreamReader readFile = new StreamReader(File.Open(fileAdress, FileMode.Open, FileAccess.Read));
            while ((booferString = readFile.ReadLine()) != null)
            {
                if (debugIsOn) //ну если сделаешь окошко для дебага
                {
                    //Console.WriteLine(readFile.ReadLine()); 
                }
                SymbolTree.IncreaseTheTree(booferString, tree);

            }
            readFile.Close();
            booferString = "";

            StreamWriter writeFile = new StreamWriter(File.Open(newFileAdress, FileMode.Create, FileAccess.Write));

            readFile = new StreamReader(File.Open(fileAdress, FileMode.Open, FileAccess.Read));

            while ((booferString = readFile.ReadLine()) != null)
            //нужно в ВПФ забахать дебаг окошко для вывода текста
            {
                if (debugIsOn) //ну если сделаешь окошоко для дебага
                {
                    //Console.WriteLine(readFile.ReadLine()); 
                }
                Console.WriteLine(booferString);

                for (int i = 0; i < booferString.Length; i++)
                {
                    binaryString += SymbolTree.FindCodeFromTree(booferString[i], tree); //получаем сжатый код из дерева 

                    if (binaryString.Length > 8)
                    {
                        writeFile.Write(Binary.FromBinConvert(binaryString.Substring(0, 8))); //жрять жри его
                        //Console.WriteLine(Binary.FromBinConvert(tempCode.Substring(0, 7)));
                        //надеюсь никакую хуйню не забыл чот уже спать хочу
                        binaryString = binaryString.Substring(8); //экспроприация
                    }

                }
            }
            if (binaryString.Length > 0)
            {
                Console.WriteLine("------");
                Binary.ModifyCode(binaryString);
                writeFile.Write(Binary.FromBinConvert(binaryString));
            }
            readFile.Close();
            //File.Delete(fileAdress); для тестов пока оставим
            //write smtg
            writeFile.Close();
        }

        public static void DecodeFile(string fileAdress)
        {
            //
            string newFileAdress = fileAdress.Remove(fileAdress.Length - ".kusokGovna".Length); newFileAdress += ".txt"; //замена адреса
            BinaryReader binaryReader = new BinaryReader(File.Open(fileAdress, FileMode.Open, FileAccess.Read));
            StreamWriter streamWriter = new StreamWriter(File.Open(newFileAdress, FileMode.Create, FileAccess.Write));
            //потом потещу как лучше сыграть с бинарными файлами
            //тут надо будет считать длину таблицы/дерева 

            bool debugIsOn = false;
            string booferString = "";
            string binaryString = "";
            SymbolTree.Tree tree = new SymbolTree.Tree();
            //заполнить дерево
            int counter = 0;
            while (binaryReader.PeekChar() > -1)
            {

                binaryString += Binary.ToBinConvert(binaryReader.ReadChar());
                //надо проверить, вдруг на нормальном ЯП можно переносить символ конца строки. Да и вообще символы табуляции и прочее.
                booferString += SymbolTree.Decode(binaryString, tree);
                if (counter > 255)
                {
                    counter = 0;
                    streamWriter.WriteLine(booferString);
                    booferString = "";
                }
                else
                {
                    counter++;
                }
            }
            binaryReader.Close();
            streamWriter.Close();

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



