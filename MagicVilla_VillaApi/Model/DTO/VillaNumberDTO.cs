using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaApi.Model.DTO
{
    public class VillaNumberDTO
    {

        [Required]
        public int VillaNo { get; set; }

        [Required]
        public int VillaId { get; set; }

        public string SpecialDetails { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public VillaDTO Villa { get; set; }


    }


}
