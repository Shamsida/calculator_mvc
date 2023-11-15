using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace machinetest.Models.Domain
{
    [NotMapped]
    public class InputValue
    {
        [RegularExpression("^[0-9]{1,10}$", ErrorMessage = "PV should be a non-negative number with up to 10 digits.")]
        [Range(0, int.MaxValue, ErrorMessage = "PV should be a non-negative number.")]
        public int PV { get; set; }

        [Range(0, 99, ErrorMessage = "R should be a non-negative number with 1 or 2 digits.")]
        public int R { get; set; }

        [Range(0, 99, ErrorMessage = "N should be a non-negative number with 1 or 2 digits.")]
        public int N { get; set; }
    }
}
