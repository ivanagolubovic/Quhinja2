using System;
using System.Collections.Generic;
using System.Text;

namespace Quhinja.Services.Mappings.InputMappings
{
    public class UsersCommentForDishInputModel
    {
        public int DishId { get; set; }

        public int UserId { get; set; }

        public string com { get; set; }
        
    }
}
