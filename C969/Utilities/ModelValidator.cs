using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Utilities
{    
     /// <summary>
     /// Utility class for validating models
     /// </summary>
    public class ModelValidator
    {
        /// <summary>
        /// Validates an object using the ValidationContext and returns a success boolean
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="model">Instance of the object that needs validation</param>
        /// <returns>Success boolean</returns>
        public static bool ValidateModel<T>(T model) where T : class
        {
            if (model == null) return false;
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(model, context, results, true);
        }
    }
}
