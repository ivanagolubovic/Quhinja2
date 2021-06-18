using System;
using System.Collections.Generic;
using System.Text;

namespace Quhinja.Services.Models.InputModels.Recipe
{
    public class RecipeWithDishUpdateInputModel
    {
        public string Name { get; set; }

        public string WayOfPreparing { get; set; }

        public string Preview { get; set; }

        public string PreparationTime { get; set; }

        public string Picture { get; set; }

    }
}
