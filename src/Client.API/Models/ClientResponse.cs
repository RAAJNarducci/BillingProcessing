namespace Client.API.Models
{
    public class ClientResponse : BaseResponse<ClientViewModel>
    {
        public ClientResponse(ClientViewModel client) : base(client)
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
