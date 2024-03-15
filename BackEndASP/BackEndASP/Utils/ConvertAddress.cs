using BackEndASP.DTOs.BuildingDTOs;

namespace BackEndASP.Utils
{
    public static class ConvertAddress
    {
        public static string Convert(BuildingInsertDTO dto)
        {
            var data = $"{dto.Number} {dto.Address} {dto.District} {dto.State} Brasil";
            return Uri.EscapeDataString(data);
        }
    }
}
