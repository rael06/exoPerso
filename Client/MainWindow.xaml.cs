using Client.Communications;
using Client.Views;
using Common;
using Common.enums;
using Newtonsoft.Json;
using Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private CommunicationManager _com = new CommunicationManager();
		public string Login { get; set; }
		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
			_com.ServerResponseReceived += OnResponseReceived;
		}

		private void OnResponseReceived(object sender, PropertyChangedEventArgs e)
		{
			//Debug.WriteLine((sender as CommunicationManager).ServerResponse.ToString());
		}

		private void Login_Click(object sender, RoutedEventArgs e)
		{
			Debug.WriteLine(Login);
			if (Login != null)
			{
				var communication = new Communication { Type = EType.LOGIN, Target = ETarget.STUDENT, Content = Login, Success = true };
				var response = _com.Communicate(communication);
				if (response.Content == true)
				{
					this.Hide();
					new Window1().Show();
				}
				else
				{
					Debug.WriteLine("not found");
				}
					
				Debug.WriteLine(response.ToString());
			}
			else
			{
				Debug.WriteLine("Veuillez remplir le champ");
			}
		}
	}
}
