using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using POS.Application.Dtos.Request;
using POS.Application.Interfaces;
using POS.Utilities.Static;

namespace POS.Test.Category
{
    [TestClass]
    public class CategoryApplicationTest
    {
        //Ayuda a hacer a inyeccion de dependencias

        private static WebApplicationFactory<Program>? _factory = null;

        //permite recuperar y almacenar los servicos del contenedor de inyeccion de dependencias
        private static IServiceScopeFactory? _scopeFactory = null;

        //va a inicializar los servicios para poder inyectar, permite utilizar o recuperar las variables en tiempo de ejecucion
        [ClassInitialize]
        public static void Initialize(TestContext _testcontext)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
        }

        //ahora si vamos a implementar nuestros metodos
        [TestMethod]
        public async Task RegisterCategory_WhenSendingNullValuesOrEmpty_ValidationErrors()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            //preparar - arrange
            var name = "";
            var descripction = "";
            var state = 1;
            var expected = ReplyMessage.MESSAGE_ERR_VALIDATE;

            //actuar Act
            var result = await context!.RegisterCategory(new CategoryRequestDto()
            {
                Name = name,
                Description = descripction,
                State = state
            });
            var current = result.Message;

            //Assert - sale el aprobado o no aprobado
            Assert.AreEqual(expected, current);
        }

        [TestMethod]
        public async Task RegisterCategory_WhenSendingCorrectValues_RegisteredSuccessFully()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<ICategoryApplication>();

            //preparar - arrange
            var name = "Nuevo registro";
            var descripction = "Nuevo registro";
            var state = 1;
            var expected = ReplyMessage.MESSAGE_SAVE ;

            //actuar Act
            var result = await context!.RegisterCategory(new CategoryRequestDto()
            {
                Name = name,
                Description = descripction,
                State = state
            });
            var current = result.Message;

            //Assert - sale el aprobado o no aprobado
            Assert.AreEqual(expected, current);
        }

    }
}
