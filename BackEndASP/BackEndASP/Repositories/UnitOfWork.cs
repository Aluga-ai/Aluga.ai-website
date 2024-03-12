


using BackEndASP.Interfaces;
using BackEndASP.Services;

public class UnitOfWork : IUnitOfWorkRepository
    {
        private IPropertyRepository _propertyRepository;

        private SystemDbContext _dbContext;

        public UnitOfWork(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }




    public ICollegeRepository CollegeRepository => throw new NotImplementedException();

    public IImageRepository ImageRepository => throw new NotImplementedException();

    public INotificationRepository NotificationRepository => throw new NotImplementedException();

    public IOwnerRepository OwnerRepository => throw new NotImplementedException();

    public IPropertyRepository PropertyRepository { get { return _propertyRepository = _propertyRepository ?? new PropertyService(_dbContext); } }

    public IStudentRepository StudentRepository => throw new NotImplementedException();

    public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _dbContext.DisposeAsync();
        }
    }

