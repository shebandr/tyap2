using System;
using System.Collections.Generic;
using System.IO;
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

namespace tyap2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Dictionary<string, string> RulesDictStrings = new Dictionary<string, string>();
        private string SpecialSymbol = "_";
        private string Chain = "0000001";
        private string StartStage = "";
        private string FinalStage = "";

        public MainWindow()
        {
            InitializeComponent();
            string fileStatus = ReadFromFile("l.txt");
            Console.WriteLine(fileStatus);
            if (fileStatus == "ok")
            {
                //файл считан успешно

                Console.WriteLine(ChainCheck());


            }
            

        }


        private string ReadFromFile(string filename)
        {

            string[] lines; 
            try
            {
                lines = File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Ошибка при чтении файла:");
                Console.WriteLine(e.Message);
                return "ошибка при чтении файла";

            }
            int numRows = lines.Length;
            int numCols = 3;
            string[,] rules = new string[numRows, numCols];

            for (int i = 0; i < numRows; i++)
            {
                string[] parts = lines[i].Split(' ');
                if (parts.Length != 3) {
                    return "в строке " + i + " неправильное число параметров";
                }
            }

            for (int i = 0; i < numRows; i++) {


                string[] parts = lines[i].Split(' ');

                for (int j = 0; j < 3; j++)
                {
                    rules[i, j] = parts[j];
                }
                if (i == 0)
                {
                    StartStage = parts[0];
                }
                if (i == numRows-1)
                {
                    FinalStage = parts[2];
                }
                RulesDictStrings.Add(rules[i, 0] + SpecialSymbol + rules[i, 1], rules[i, 2]);
            }


            // запись должна выглядеть как "q0 a q1", разбиение через пробелы, в словарь оно будет записываться как "q0_a q1"


            return "ok";
        }



        private string ChainCheck()
        {
            string changes = "";
            string chain = Chain;
            string stage = StartStage;
            for (int i = 0;i < Chain.Length; i++)
            {
                changes += " (" + stage + ") "  + chain;
                if(chain.Length != 1)
                {


                    changes += " -> ";
                } else
                {
                    changes += " ";

                }
                if(RulesDictStrings.ContainsKey(stage + SpecialSymbol + chain[0]))
                {
                    stage = RulesDictStrings[stage + SpecialSymbol + chain[0]];
                    chain = chain.Substring(1);
                } else
                {
                    changes += "отсутствует правило перехода";
                    return changes;
                }
                
            }
            if (stage != FinalStage)
            {
                changes += " -> " +  stage + " автомат не в финальном состоянии, должно быть " + FinalStage;
                return changes;
            }
            else
            {
                changes += " успешно завершено";

                return changes;
            }

        }

    }
}
