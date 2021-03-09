using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;
using static System.Convert;

namespace AppTest1
{
    public partial class MainPage : ContentPage
    {
        public IFolder rootFolder;
        public IFolder folder;
        public IFile file;
        private int count = 0;

        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "coffeeDemo.txt");

        public MainPage()
        {

            InitializeComponent();
            var pclStorageSample = PCLStorageSample();
            while (pclStorageSample.IsCompleted == false)
            {

            }
            OverWriteFile("5");
            try
            {
                count = Convert.ToInt32(file.ReadAllTextAsync());
            }
            catch (Exception e)
            {
                //Null
                count = 0;
            }
            
        }
       




        public string ReadFile()
        {

            return file.ReadAllTextAsync().ToString();
        }

        public void OverWriteFile(string value)
        {
            file.WriteAllTextAsync(value);
        }

        void ButtonAdd_Clicked(object sender, System.EventArgs e)
        {
            count++;
            LabelEllen.Text = $"Ellen's coffee card \nYou have had {count} cups of coffee";
            OverWriteFile(count.ToString());
        }

        private void ButtonLoad_Clicked(object sender, System.EventArgs e)
        {
            count = Convert.ToInt32(File.ReadAllText(fileName));
            ((Button)sender).Text = $"Load amount {count.ToString()}  ";

            //string res = ReadFile();
            //count = Convert.ToInt32(res);

            //((Button)sender).Text = $"Load amount {count.ToString()}  ";
        }

        public void ButtonRemove_Clicked(object sender, System.EventArgs e)
        {
            count--;
            LabelEllen.Text = $"Ellen's coffee card \nYou have had {count} cups of coffee";
            OverWriteFile(count.ToString());
        }

      
        public async Task PCLStorageSample()
        {
            rootFolder = FileSystem.Current.LocalStorage;
            folder = await rootFolder.CreateFolderAsync("MySubFolder",
                CreationCollisionOption.OpenIfExists);

            file = await folder.CreateFileAsync("answer.txt",
                CreationCollisionOption.ReplaceExisting);
            file.WriteAllTextAsync(count.ToString());

        }
    }
}
