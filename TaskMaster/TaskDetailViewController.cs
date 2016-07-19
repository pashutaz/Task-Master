using Foundation;
using System;
using UIKit;

namespace TaskMaster
{
	public partial class TaskDetailViewController : UITableViewController
	{
		Task currentTask { get; set; }
		public MasterViewController Delegate { get; set; } // will be used to Save, Delete later

		public TaskDetailViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			SaveButton.TouchUpInside += (sender, e) =>
			{
				currentTask.Name = TitleText.Text;
				currentTask.Notes = NotesText.Text;
				currentTask.Done = DoneSwitch.On;
				Delegate.SaveTask(currentTask);
			};

			DeleteButton.TouchUpInside += (sender, e) =>
			{
				Delegate.DeleteTask(currentTask);
			};
		}
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			TitleText.Text = currentTask.Name;
			NotesText.Text = currentTask.Notes;
			DoneSwitch.On = currentTask.Done;
		}

		//this will be called before the view is displayed
		public void SetTask(MasterViewController d, Task task)
		{
			Delegate = d;
			currentTask = task;
		}



	}
}