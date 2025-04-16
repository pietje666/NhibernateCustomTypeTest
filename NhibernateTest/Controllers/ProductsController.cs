using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using NhibernateTest.Entities;
using ISession = NHibernate.ISession;

namespace NhibernateTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ISession _session;

        public ProductsController(ISession session)
        {
            _session = session;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<ProductsDto>> Get()
        {
            var nothing = (CustomDateType?)null;

            var products = await _session.Query<Product>().Where(x => x.ValidityPeriod.Until >= nothing).ToListAsync();

            //the folowing line gives NHibernate.MappingException: 'No persister for: NhibernateTest.Entities.CustomDateType'
            var products2 = await _session.Query<Product>().Where(x => x.ValidityPeriod.Until >= nothing).ToListAsync();

            return products.Select(x => new ProductsDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
