using MagicVilla_VillaApi.Model.DTO;

namespace MagicVilla_VillaApi.Model
{
    public static class VillaDATA
    {

        public static List<VillaDTO> Villas = new List<VillaDTO>()
                {
                    new VillaDTO()
                    {
                        Id = 1,
                        Name = "Beach"
                    },

                     new VillaDTO()
                        {
                        Id = 2,
                        Name = "Ben Al-almeen"
                    },

                      new VillaDTO()
                    {
                        Id = 3,
                        Name = "6th of october"
                    },

                     new VillaDTO()
                    {
                        Id = 4,
                        Name = "6th of october - wadi degla"
                    }
                };

        public static List<VillaDTO> SomeData()
        {
            return Villas;
        }


        public static VillaDTO CreateVillaDTO(VillaDTO villaDTO)
        {
            villaDTO.Id = Villas.Count == 0 ? 1 : Villas.Max(v => v.Id) + 1;
            Villas.Add(villaDTO);

            return villaDTO;

        }

    }
}
