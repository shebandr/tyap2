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




        int StatesNum = 0;
        int AlphabetLength = 0;
        string[] AlphabetArray = new string[0];
        private List<TextBox> RulesDict = new List<TextBox>();
        List<string> FirstStageRule = new List<string>();
        List<string> AlphabetRule = new List<string>();
        bool s1 = false;
        bool s2 = false;
        List<string> NonTerminalSymb = new List<string>();
        List<string> TerminalSymb = new List<string>();
        List<string> AllString = new List<string>();


        private Dictionary<string, string> RulesDictStrings = new Dictionary<string, string>();
        private string SpecialSymbol = "_";
        private string SpecialSymbolSolDelim = "\n";
        private string Chain = "";
        private string StartStage = "";
        private string FinalStage = "";

        public MainWindow()
        {
            InitializeComponent();
 /*           string fileStatus = ReadFromFile("l.txt");
            Console.WriteLine(fileStatus);
            if (fileStatus == "ok")
            {
                //файл считан успешно

                Console.WriteLine(ChainCheck());


            }
            */

        }


        private void CreateTextBoxes()
        {
            for (int i = 0; i < StatesNum; i++)
            {
                for(int j = 0; j < AlphabetLength; j++)
                {

                    var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };

                    var textBox1 = new TextBlock { Width = 30, Name = $"D{i + 1}1"};
                    textBox1.Text = i.ToString();
                    var textBox2 = new TextBlock { Width = 30, Name = $"D{i + 1}1"};
                    textBox2.Text = AlphabetArray[j];
                    var label1 = new Label { Content = "   " };
                    var label2 = new Label { Content = " -> " };
                    var textBox3 = new TextBox { Width = 30, Name = $"D{i + 1}2" };
                    FirstStageRule.Add(i.ToString());
                    AlphabetRule.Add(AlphabetArray[j].ToString());


                    stackPanel.Children.Add(textBox1);
                    stackPanel.Children.Add(label1);
                    stackPanel.Children.Add(textBox2);
                    stackPanel.Children.Add(label2);
                    stackPanel.Children.Add(textBox3);

                    MainStackPanel.Children.Add(stackPanel);

                    // Добавляем текстовые поля в словарь
                    RulesDict.Add(textBox3);

                }


            }
        }
        private void SettingsApply_Click(object sender, RoutedEventArgs e)
        {
            if (s1)
            {
                return;
            }
            if (StatesNumInput.Text == "" || AlphabetSymbolsInput.Text == "")
            {
                ErrorTextbox.Text = "пустые строки";
                return;
            }
            if (Int32.TryParse(StatesNumInput.Text, out StatesNum))
            {

            }
            else
            {
                ErrorTextbox.Text = "введено не число в число состояний";
                return;
            }
            /*            if (Int32.TryParse(AlphabetLengthInput.Text, out AlphabetLength))
                        {

                        }
                        else
                        {
                            ErrorTextbox.Text = "введено не число в длину алфавита";
                            return;
                        }*/

            if (AlphabetSymbolsInput.Text.Length == 0)
            {
                ErrorTextbox.Text = "пустой словарь";
                return;
            }
            if (StartStageInput.Text.Length == 0)
            {
                ErrorTextbox.Text = "пустое стартовое состояние";
                return;
            }
            if (FinalStageInput.Text.Length == 0)
            {
                ErrorTextbox.Text = "пустое финальное состояние";
                return;
            }
            
            AlphabetArray = AlphabetSymbolsInput.Text.Split(' ');
            StartStage = StartStageInput.Text;
            FinalStage = FinalStageInput.Text;
            AlphabetLength = AlphabetArray.Length;

            Console.WriteLine(StatesNum + " " + AlphabetSymbolsInput);
            CreateTextBoxes();
            s1 = true;
            ST3.Height = 30;
        }

        private void ManualInputButton_Click(object sender, RoutedEventArgs e)
        {
            ST1.Height = 0;
            ST2.Height = 30;
            
            //вывести на экран интерфейс для добавления правил
        }



        private void FileInputButton_Click(object sender, RoutedEventArgs e)
        {
            ST1.Height = 0;
            ST3.Height = 30;
            //вызвать функции для считывания из файла и выведения на экран

            string fileStatus = ReadFromFile("l.txt");
            Console.WriteLine(fileStatus);
            if (fileStatus == "ok")
            {
                //файл считан успешно

                Console.WriteLine(ChainCheck());


            }


        }
        private bool T = false;
        private void CalcButton_Click(object sender, RoutedEventArgs e)
        {

            if (T == false)
            {
                if (ST2.Height == 0)
                {

                }
                else
                {
                    for (int i = 0; i < AlphabetLength * StatesNum; i++)
                    {

                        if (RulesDict[i].Text != "")
                        {

                            RulesDictStrings.Add(FirstStageRule[i] + SpecialSymbol + AlphabetRule[i], RulesDict[i].Text);
                        }


                    }
                    foreach (var a in RulesDictStrings)
                    {
                        Console.WriteLine(a.Key + " " + a.Value);
                    }
                    // берутся значения из инпутов
                }

            } else
            {
                MainStackPanel.Children.Clear();
                T = true;
            }



            T = true;



            Chain = CheckStringInput.Text;


            string outputString = ChainCheck();
            var labelVar = new TextBlock { Width = 800 };
            labelVar.Text = outputString;
            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = labelVar,
                Height = 400,
                Name = "TTTT"
            };
            MainStackPanel.Children.Add(scrollViewer);

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


                    changes += SpecialSymbolSolDelim;
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
                changes += SpecialSymbolSolDelim +  stage + " автомат не в финальном состоянии, должно быть " + FinalStage;
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
