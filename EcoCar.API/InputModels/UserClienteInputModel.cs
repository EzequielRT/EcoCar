namespace EcoCar.API.InputModels
{
    public class UserClienteInputModel
    {
        public int UserId { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }

        public void SetId(int id) => UserId = id;
    }
}