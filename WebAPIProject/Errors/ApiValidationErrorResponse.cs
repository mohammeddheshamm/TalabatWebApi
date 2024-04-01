using System.Collections;
using System.Collections.Generic;

namespace TalabatAPIS.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        // Leeh ba3at 400 fy al constructor 3l4aaan al validation error is special type of badrequest
        public ApiValidationErrorResponse():base(400)
        {
            
        }
    }
}
