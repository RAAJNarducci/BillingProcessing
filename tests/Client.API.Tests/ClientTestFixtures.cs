using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Client.API.Tests
{
    [CollectionDefinition(nameof(ClientCollection))]
    public class ClientCollection : ICollectionFixture<ClientTestFixtures>
    { }

    public class ClientTestFixtures : IDisposable
    {
        public void Dispose()
        {
        }
    }
}
