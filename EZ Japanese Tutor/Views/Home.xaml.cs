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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EZ_Japanese_Tutor.Views
{

    public partial class Home : UserControl
    {

        private Random rand = new Random();
        private DispatcherTimer marqueeTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
        private List<Label> lstLabels = new List<Label>();
        private List<DoubleAnimation> lstAnims = new List<DoubleAnimation>();
        private const int maxLabels = 15;

        List<string> testStrings = new List<string>()
        {
            "テスト",
            "試験",
            "試し",
            "試練",
            "トライ",
            "試み",
            "リサーチ",
            "研究",
            "エバリュエーション",
            "評価",
            "テ。ス。ト。",
            "TEST STRING 12",
            "TEST STRING 13",
            "TEST STRING 14",
            "TEST STRING 15"
        };

        public Home()
        {
            InitializeComponent();
            marqueeTimer.Tick += Marquee_Tick;
            Start_Marquee();

        }

        private void Marquee_Tick(object sender, EventArgs e)
        {
            if (lstLabels.Count < maxLabels && rand.Next(100) < 25 || lstLabels.Count == 0)
            {

                Label newLabel = new Label
                {
                    Content = testStrings[lstLabels.Count],
                    FontSize = rand.Next(10, 26)

                };
                lstLabels.Add(newLabel);
                cnvHome.Children.Add(newLabel);
                newLabel.Margin = new Thickness(0, rand.Next((int)(cnvHome.ActualHeight - newLabel.ActualHeight)), 0, 0);
                newLabel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                newLabel.Arrange(new Rect(0, 0, newLabel.DesiredSize.Width, newLabel.DesiredSize.Height));

                DoubleAnimation marqueeAnimation = new DoubleAnimation
                {
                    From = cnvHome.ActualWidth,
                    To = -newLabel.ActualWidth,
                    Duration = new Duration(TimeSpan.Parse("0:0:" + rand.Next(10, 30).ToString()))

                };

                marqueeAnimation.Completed += (se, ev) =>
                {
                    cnvHome.Children.Remove(newLabel);
                    lstLabels.Remove(newLabel);

                };

                newLabel.BeginAnimation(Canvas.LeftProperty, marqueeAnimation);

            }

        }

        public void Start_Marquee()
        {
            marqueeTimer.Start();

        }

        public void Stop_Marquee()
        {
            marqueeTimer.Stop();

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            marqueeTimer.Stop();

        }

    }

}
