using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

using System.Xml;

namespace XD501
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        XdomeaHandler xdomeaHandler = new XdomeaHandler();

        public MainWindow()
        {
            InitializeComponent();
            using (var s = File.OpenRead("Templates\\MainView.xaml"))
            {
                var rootElement = (UIElement)XamlReader.Load(s);
                this.AddChild(rootElement);
            }

            DataContext = xdomeaHandler;

        }


        private void SetAllCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SetAllCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.OriginalSource is Button button)
            {
                if (button == null || button.DataContext == null)
                {
                    return;
                }

                xdomeaHandler.SetAll(button.DataContext as IList<XmlNode>, button?.Content.ToString());

                e.Handled = true;
            }
        }


        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                FileName = "*0501.zip",
                DefaultExt = ".zip", 
                Filter = "XDomea2 (.zip)|*.zip", 
                CheckFileExists = true
            };

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                xdomeaHandler.OpenFile(dlg.FileName);

            }

            
        }

        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            e.CanExecute = xdomeaHandler.canSave;

        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string filename = $"{xdomeaHandler.pid}.0502.zip";

            SaveFileDialog dlg = new SaveFileDialog
            {
                FileName = filename, 
                DefaultExt = ".zip",
                Filter = "XDomea2 (.zip)|*.zip", 
                RestoreDirectory = true,
                OverwritePrompt = true
            };

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                if (File.Exists(dlg.FileName))
                {
                    File.Delete(dlg.FileName);
                }

                xdomeaHandler.SaveFile(dlg.FileName);
            }

            
        }

        

    }
}