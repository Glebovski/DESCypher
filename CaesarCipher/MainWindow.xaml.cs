using Microsoft.Win32;
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

namespace CaesarCipher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string Selection()
        {
            if (CBC.IsSelected)
                return "CBC";
            if (CFB.IsSelected)
                return "CFB";
            if (ECB.IsSelected)
                return "ECB";
            if (CTS.IsSelected)
                return "CTS";
            if (OFB.IsSelected)
                return "OFB";
            else return "CBC";
        }

        public string SelectionCipher()
        {
            if (DES.IsSelected)
                return "DES";
            if (TripleDES.IsSelected)
                return "TripleDES";
            if (AES.IsSelected)
                return "AES";
            else return "DES";
        }

        private Crypt crypt = new Crypt();
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text Files | *.txt";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
                filearea.Text = File.ReadAllText(dialog.FileName);

        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text Files | *.txt";
            if (dialog.ShowDialog() == true)
                File.WriteAllText(dialog.FileName, filearea.Text);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Made by Zadachin Gleb, TM-51, #9");
        }

        private void Encrypt_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                filearea.Text = File.ReadAllText(crypt.Encrypt(Key.Text, IV.Text, filearea.Text,Selection(), SelectionCipher()),Encoding.Default);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Key and IV must contain only 8 symbols");
            }
        }

        private void Decrypt_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                filearea.Text = crypt.Decrypt(Key.Text, IV.Text, filearea.Text,Selection());
                File.Delete(@"d:\crypt.txt");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Key and IV must contain only 8 symbols");
            }
        }
    }
}
