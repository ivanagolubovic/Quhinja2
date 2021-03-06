using Quhinja.Data.Entiities.Quhinja.Data.Entiities;
using System.Collections.Generic;

namespace Quhinja.Data.Entiities
{
    public class Dish
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

        public int selectedRecipeId { get; set; }

        public Recipe selectedRecipe { get; set; }
        public string Rate { get; set;  }
        public float? averageRating { get; set; }

        public ICollection<UsersRatingForDish> UsersRatings { get; set; }

        public string DishType { get; set; }
        public  ICollection<UserCommentsForDish> UsersComments { get; set; }
        public int? numOfComments { get; set;  }

    }
}