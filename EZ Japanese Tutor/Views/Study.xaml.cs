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

namespace EZ_Japanese_Tutor.Views
{
    /// <summary>
    /// Interaction logic for Study.xaml
    /// </summary>
    public partial class Study : UserControl
    {
        private Random rand = new Random();

        List<string> studyStrings = new List<string>()
        {
            "私は彼を５日前、つまりこの前の金曜日に駅で見かけた。",
            "これはテストです。",
            "彼の名前はジョンです。",
            "ザックと申します。"
        };

        public Study()
        {
            InitializeComponent();
            DisplayStudyQuestion();

        }

        private void DisplayStudyQuestion()
        {
            string studyString = studyStrings[rand.Next(studyStrings.Count)];
            StringBuilder segmented = new StringBuilder(MeCab.Parse(studyString));
            StudyQuestion.Content = segmented;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DisplayStudyQuestion();

        }

    }

}
