


using BackEndASP.Interfaces;
using BackEndASP.Services;

public class UnitOfWork : IUnitOfWorkRepository
    {
        private IPropertyRepository _propertyRepository;
        private IStudentRepository _studentRepository;

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

    public IStudentRepository StudentRepository { get { return _studentRepository = _studentRepository ?? new StudentService(_dbContext); } }

    public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _dbContext.DisposeAsync();
        }
    }

