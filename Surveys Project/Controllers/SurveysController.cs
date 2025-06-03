using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surveys_Project.Data;
using Surveys_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surveys_Project.Controllers
{
	public class SurveysController : Controller
	{
		private readonly DDBContext _context;

		public SurveysController(DDBContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult Surveys()
		{
			return View(new SurveysViewModel());
		}

		public IActionResult Surveys(SurveysViewModel model)
		{
			model.FoodOptions = new List<string> { "Pizza", "Pasta", "Pap and Wors", "Other" };

		
			if (model.SelectedFoods == null || !model.SelectedFoods.Any())
			{
				ModelState.AddModelError("SelectedFoods", "Please select at least one favorite food.");
			}
			else
			{
				model.Survey.FavoriteFoods = string.Join(",", model.SelectedFoods);
			}

			
			if (model.Survey.RateMovies == null)
			{
				ModelState.AddModelError("Survey.RateMovies", "Please rate your agreement for watching movies.");
			}
			if (model.Survey.RateRadio == null)
			{
				ModelState.AddModelError("Survey.RateRadio", "Please rate your agreement for listening to radio.");
			}
			if (model.Survey.RateEatOut == null)
			{
				ModelState.AddModelError("Survey.RateEatOut", "Please rate your agreement for eating out.");
			}
			if (model.Survey.RateTV == null)
			{
				ModelState.AddModelError("Survey.RateTV", "Please rate your agreement for watching TV.");
			}

			if (!ModelState.IsValid)
			{
				ModelState.Clear();
				TryValidateModel(model);
				if (!ModelState.IsValid)
					return View(model);
			}


			_context.Surveys.Add(model.Survey);
			_context.SaveChanges();

			TempData["SuccessMessage"] = "Thank you! Your survey has been submitted successfully.";
			return RedirectToAction("Surveys");
		}

		public async Task<IActionResult> Stats()
		{
			var surveys = await _context.Surveys.ToListAsync();

			if (!surveys.Any())
				return View(new SurveyStatsViewModel());

			var today = DateTime.Today;

			double? GetAge(Survey s)
			{
				if (!s.DOB.HasValue) return null;
				int age = today.Year - s.DOB.Value.Year;
				if (s.DOB.Value.Date > today.AddYears(-age)) age--;
				return age;
			}

			var validAges = surveys.Select(GetAge).Where(a => a.HasValue).Select(a => a.Value).ToList();
			double averageAge = validAges.Any() ? validAges.Average() : 0;
			double oldest = validAges.Any() ? validAges.Max() : 0;
			double youngest = validAges.Any() ? validAges.Min() : 0;

			double percentage(string food) =>
				surveys.Count(s => !string.IsNullOrEmpty(s.FavoriteFoods) && s.FavoriteFoods.ToLower().Contains(food.ToLower())) * 100.0 / surveys.Count;

			double SafeAverage(Func<Survey, int?> selector) =>
				surveys.Select(selector).Where(v => v.HasValue).Select(v => v.Value).DefaultIfEmpty(0).Average();

			var stats = new SurveyStatsViewModel
			{
				TotalSurveys = surveys.Count,
				AverageAge = Math.Round(averageAge, 1),
				OldestParticipant = oldest,
				YoungestParticipant = youngest,
				PizzaPercentage = Math.Round(percentage("Pizza"), 1),
				PastaPercentage = Math.Round(percentage("Pasta"), 1),
				PapAndWorsPercentage = Math.Round(percentage("Pap and Wors"), 1),
				AvgMovieRating = Math.Round(SafeAverage(s => s.RateMovies), 1),
				AvgRadioRating = Math.Round(SafeAverage(s => s.RateRadio), 1),
				AvgEatOutRating = Math.Round(SafeAverage(s => s.RateEatOut), 1),
				AvgTVRating = Math.Round(SafeAverage(s => s.RateTV), 1)
			};

			return View(stats);
		}
	}
}
