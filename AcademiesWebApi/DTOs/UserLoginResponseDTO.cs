namespace AcademiesWebApi.DTOs
{
    public class UserLoginResponseDTO
    {
        public bool Login { get; set; }
        public string Token { get; set; }
        public List<string> Errors { get; set; }
    }
}
