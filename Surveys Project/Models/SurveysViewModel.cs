using System.Collections.Generic;

namespace Surveys_Project.Models
{
	public class SurveysViewModel
	{
		public Survey Survey { get; set; } = new Survey();
		public List<string> FoodOptions { get; set; } = new List<string> { "Pizza", "Pasta", "Pap and Wors", "Other" };
		public List<string> SelectedFoods { get; set; }
	}
}
