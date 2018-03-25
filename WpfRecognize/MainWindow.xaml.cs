using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfRecognize
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RecognizationCore _core = new RecognizationCore();

        public MainWindow()
        {
            InitializeComponent();

            this.Output.Focus();
            this.DataContext = _core;
        }

        private void WritingCanvasOnStrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            _core.Recognize(this.WritingCanvas.Strokes);
        }

        private void RecognizerSelectorOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Clear();
        }

        private void ClearButtonOnClick(object sender, RoutedEventArgs e)
        {
            this.Clear();
        }

        private void SelectCharactorButtonOnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var text = button.Content as string;
            if (text != null)
            {
                var composition = new TextComposition(InputManager.Current, this.Output, text);
                TextCompositionManager.StartComposition(composition);
                this.Clear();
            }
        }

        private void Clear()
        {
            this.WritingCanvas.Strokes.Clear();
            _core.ClearAlternates();
        }
    }
}
