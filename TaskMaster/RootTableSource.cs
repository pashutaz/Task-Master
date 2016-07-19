using System;
using Foundation;
using UIKit;
namespace TaskMaster
{
	public class RootTableSource : UITableViewSource
	{

		// there is NO database or storage of Tasks in this example, just an in-memory List<>
		Task[] tableItems;
		string cellIdentifier = "taskcell"; // set in the Storyboard

		public RootTableSource(Task[] items)
		{
			tableItems = items;
		}
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return tableItems.Length;
		}
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell, 
			var cell = tableView.DequeueReusableCell(cellIdentifier);
			// now set the properties as normal
			cell.TextLabel.Text = tableItems[indexPath.Row].Name;
			if (tableItems[indexPath.Row].Done)
				cell.Accessory = UITableViewCellAccessory.Checkmark;
			else
				cell.Accessory = UITableViewCellAccessory.None;
			return cell;
		}
		public Task GetItem(int id)
		{
			return tableItems[id];
		}

	}
}

