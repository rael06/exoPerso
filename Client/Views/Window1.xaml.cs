using Client.Communications;
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
using System.Windows.Shapes;

namespace Client.Views
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window, INotifyPropertyChanged
	{
		#region Students
		private ObservableCollection<Student> _students = new ObservableCollection<Student>();
		public ObservableCollection<Student> Students
		{
			get { return _students; }
			set
			{
				if (_students != value)
				{
					_students = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Students)));
				}
			}
		}
		#endregion

		#region Student
		private Student _student = new Student();

		public Student Student
		{
			get { return _student; }
			set
			{
				if (_student != value)
				{
					_student = value;
					PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Student)));
				}
			}
		}
		#endregion

		private CommunicationManager _com = new CommunicationManager();

		public event PropertyChangedEventHandler PropertyChanged;
		public Window1()
		{
			InitializeComponent();
			DataContext = this;
			_com.ServerResponseReceived += OnResponseReceived;
			InitData();
		}

		private void OnResponseReceived(object sender, PropertyChangedEventArgs e)
		{
			Debug.WriteLine((sender as CommunicationManager).ServerResponse.ToString());
		}

		public void InitData()
		{
			var communication = new Communication { Type = EType.READ, Target = ETarget.STUDENT, Success = true };
			var response = _com.Communicate(communication);
			Students = JsonConvert.DeserializeObject<ObservableCollection<Student>>(response.Content.ToString());
		}

		private void Logout_Click(object sender, RoutedEventArgs e)
		{
			this.Hide();
			Application.Current.MainWindow.Show();
		}
	}
}
