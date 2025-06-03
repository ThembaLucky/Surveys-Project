using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surveys_Project.Models
{
	public class Survey
	{
		[Key]
		public int SurveyID { get; set; }

		[Required(ErrorMessage = "Full Name is required")]
		[StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address format")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Date of Birth is required")]
		public DateTime? DOB { get; set; }

		[Required(ErrorMessage = "Contact Number is required")]
		[Phone(ErrorMessage = "Invalid phone number format")]
		[StringLength(10, ErrorMessage = "Contact number cannot exceed 10 digits")]
		public string ContactNumber { get; set; }
		[Required(ErrorMessage = "Please select at least one food.")]
		public string FavoriteFoods { get; set; }

		[Required(ErrorMessage = "Please rate your agreement for watching movies.")]
		public int? RateMovies { get; set; }

		[Required(ErrorMessage = "Please rate your agreement for listening to radio.")]
		public int? RateRadio { get; set; }

		[Required(ErrorMessage = "Please rate your agreement for eating out.")]
		public int? RateEatOut { get; set; }

		[Required(ErrorMessage = "Please rate your agreement for watching TV.")]
		public int? RateTV { get; set; }



	}

}
