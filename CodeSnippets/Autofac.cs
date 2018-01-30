// Standard-Config
var builder = new ContainerBuilder();
builder.RegisterType<SomeService>().As<ISomeService>();
var container = builder.Build();

// Register from Config und LoadModules
var builder = new ContainerBuilder();
builder.RegisterModule(new ConfigurationSettingsReader());
builder.RegisterModule<CoreModule>();
var container = builder.Build();
MainWindow = new MainWindow { DataContext = container.Resolve<MainViewModel>() };

// WebClient
       public static void RegisterDependencies()
        {
            // Register MVC-related dependencies
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);

            // Register services
            //builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();

            builder.RegisterModule(new ConfigurationSettingsReader());

            builder.RegisterModelBinderProvider();

            // Set the MVC dependency resolver to use Autofac
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
