using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace TaskMaster
{
	public partial class MasterViewController : UITableViewController
	{
		List<Task> tasks;

		public MasterViewController(IntPtr handle) : base(handle)
		{
			Title = "TaskBoard";

			// Custom initialization
			tasks = new List<Task>
			{
				new Task() {Name="Groceries", Notes="Buy bread, cheese, apples", Done=false},
				new Task() {Name="Devices", Notes="Buy Nexus, Galaxy, Droid", Done=false}
			};
		}


		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "TaskSegue")
			{ // set in Storyboard
				var navctlr = segue.DestinationViewController as TaskDetailViewController;
				if (navctlr != null)
				{
					var source = TableView.Source as RootTableSource;
					var rowPath = TableView.IndexPathForSelectedRow;
					var item = source.GetItem(rowPath.Row);
					navctlr.SetTask(this, item); // to be defined on the TaskDetailViewController
				}
			}
		}


		public void CreateTask()
		{
			// first, add the task to the underlying data
			var newId = tasks[tasks.Count - 1].Id + 1;
			var newTask = new Task { Id = newId };
			tasks.Add(newTask);

			// then open the detail view to edit it
			var detail = Storyboard.InstantiateViewController("detail") as TaskDetailViewController;
			detail.SetTask(this, newTask);
			NavigationController.PushViewController(detail, true);
		}


		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			TableView.Source = new RootTableSource(tasks.ToArray());
		}





		public void SaveTask(Task chore)
		{
			//var oldTask = tasks.Find(t => t.Id == chore.Id);
			NavigationController.PopViewController(true);
		}

		public void DeleteTask(Task chore)
		{
			var oldTask = tasks.Find(t => t.Id == chore.Id);
			tasks.Remove(oldTask);
			NavigationController.PopViewController(true);
		}





		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			addBarButton.Clicked += (sender, e) =>
			{
				CreateTask();
			};
		}
	}

}