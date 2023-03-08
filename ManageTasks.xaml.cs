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
using System.Windows.Shapes;
using AdvancedProgramming.Contracts;
using AdvancedProgramming.Models;
using AdvancedProgramming.Data;
using Unity;

namespace AdvancedProgramming
{
    /// <summary>
    /// Interaction logic for ManageTasks.xaml
    /// </summary>
    public partial class ManageTasks : Window
    {
        User loggedInUser;
        public ManageTasks(User u)
        {
            loggedInUser = u;
            InitializeComponent();
        }
    }
}
