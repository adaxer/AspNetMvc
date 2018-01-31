using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace HowDoesDIWork
{
    class Program
    {
        static void Main(string[] args)
        {
            // Poor mans Dependency Injection
            var c = new ShopController(new ShopService(new SqlRepository()));

            // Mit Container
            var container = new MiniContainer();
            container.Register(typeof(IRepository), typeof(SqlRepository));
            var repo = container.Get(typeof(IRepository));
            container.Register(typeof(ShopController), typeof(ShopController));
            container.Register(typeof(IShopService), typeof(ShopService));

            c = container.Get(typeof(ShopController)) as ShopController;

            c.Index();

            var unityContainer = new UnityContainer();
            unityContainer.RegisterType(typeof(IRepository), typeof(SqlRepository));
            unityContainer.RegisterType(typeof(IShopService), typeof(ShopService));

            c = unityContainer.Resolve(typeof(ShopController)) as ShopController;
            c.Index();
        }
    }

    public class MiniContainer
    {
        Dictionary<Type, Type> _registrations=new Dictionary<Type, Type>();
        
        public object Get(Type type)
        {
            if (!_registrations.ContainsKey(type))
                throw new NotSupportedException($"No registration for Type {type.FullName}");
            var typeToCreate = _registrations[type];

            var ctors = typeToCreate.GetConstructors();

            if(ctors.Any(c=>c.GetParameters().Count()==0))
                return Activator.CreateInstance(typeToCreate);

            if (ctors.Count() > 1)
                throw new NotSupportedException($"{typeToCreate.FullName} Must have only 1 ctor");

            var parameters = ctors.Single().GetParameters().Select(p=>Get(p.ParameterType)).ToArray();
            return typeToCreate.InvokeMember("ctor", System.Reflection.BindingFlags.CreateInstance, null, null, parameters);
        }

        public void Register(Type serviceType, Type implementedType)
        {
            _registrations[serviceType] = implementedType;
        }
    }

    public class ShopController
    {
        IShopService _shopService;

        public ShopController(IShopService service)
        {
            _shopService = service;
        }

        public void Index()
        {
            Console.WriteLine(_shopService.GetCategories());
        }
    }

    public class ShopService : IShopService
    {
        IRepository _repository;

        public ShopService(IRepository repository)
        {
            _repository = repository;
        }

        public string GetCategories()
        {
            return $"{_repository.Connection}: Categories";
        }
    }

    public class SqlRepository : IRepository
    {
        public string Connection => "Sql-Repository";
    }

    public interface IRepository
    {
        string Connection { get; }
    }

    public interface IShopService
    {
        string GetCategories();
    }
}
