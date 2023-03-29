namespace AcademiesWebApi.DTOs.AuthDTOs
{
    public class UserLoginResponseDTO
    {
        public bool Login { get; set; }
        public string Token { get; set; }
        public List<string> Errors { get; set; }
    }
}
