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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TVCust
{
    public class Person
    {
        public string Name { get; set; }
        public List<Person> Children { get; set; }
    }

    public class MyDataContext2 : INotifyPropertyChanged
    {
        public MyDataContext2()
        {
            Persons = new List<Person>()
            {
                new Person
                {
                    Name = "テスト１",
                    Children = new List<Person>()
                    {
                        new Person{ Name = "テスト２"},
                        new Person{ Name = "テスト３"},
                    }
                },
                new Person
                {
                    Name = "hoge",
                    Children = new List<Person>()
                    {
                        new Person{ Name = "foo" },
                        new Person{
                            Name = "bar",
                            Children = new List<Person>
                            {
                                new Person{ Name = "last" }
                            }
                        }
                    }
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;
            storage = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        private IEnumerable<Person> _persons;
        public IEnumerable<Person> Persons
        {
            get { return _persons; }
            private set
            {
                SetProperty(ref _persons, value);
            }
        }
    }

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
