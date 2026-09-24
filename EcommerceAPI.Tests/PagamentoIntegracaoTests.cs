using System.Net;
using System.Net.Http.Json;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EcommerceAPI.Tests
{
    [TestClass]
    public class PagamentoIntegracaoTests
    {
        private static WebApplicationFactory<Program> _factory = null!;
        private static HttpClient _client = null!;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [ClassCleanup]
        public static void Finalizar()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [TestMethod]
        public async Task DeveProcessarPagamentoComSucesso()
        {
            //Arrange
            var pagamento = new PagamentoRequest
            {
                PedidoId = 1,
                Valor = "10000",
                Moeda = "BRL",
                MetodoPagamento = "Cartao"
            };

            //Act
            var response = await _client.PostAsJsonAsync(
                "/api/Pagamentos", pagamento);

            //Assert
            Assert.AreEqual(
                HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]

        public async Task DeveRecusarPagamentoComValorEmFormatoIncorreto()
        {
            //Arrange
            var pagamento = new PagamentoRequest
            {
                PedidoId = 2,
                Valor = "100,00", //Formato incorreto
                Moeda = "BRL",
                MetodoPagamento = "Cartao"
            };

            //Act
            var response = await _client.PostAsJsonAsync(
                "/api/Pagamentos", pagamento);

            //Assert
            Assert.AreEqual(
                HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
