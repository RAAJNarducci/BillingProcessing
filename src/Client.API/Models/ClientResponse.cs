namespace Client.API.Models
{
    public class ClientResponse : BaseResponse<object>
    {
        public ClientResponse(object client) : base(client)
        {
        }

        public ClientResponse(string[] message) : base(message)
        {
        }

        public ClientResponse(string message) : base(new[] { message })
        {
        }
    }
}
