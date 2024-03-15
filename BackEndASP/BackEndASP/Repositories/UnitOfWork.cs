using BackEndASP.Interfaces;
using BackEndASP.Services;

public class UnitOfWork : IUnitOfWorkRepository
    {
        private IPropertyRepository _propertyRepository;
        private IStudentRepository _studentRepository;
        private IImageRepository _imageRepository;
        private ICollegeRepository _collegeRepository;    
        private IBuildingRepository _buildingRepository;

        private SystemDbContext _dbContext;

        public UnitOfWork(SystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }




    public ICollegeRepository CollegeRepository { get { return _collegeRepository = _collegeRepository ?? new CollegeService(_dbContext); } }

    public IImageRepository ImageRepository { get { return _imageRepository = _imageRepository ?? new ImageService(_dbContext); } }

    public INotificationRepository NotificationRepository => throw new NotImplementedException();

    public IOwnerRepository OwnerRepository => throw new NotImplementedException();

    public IPropertyRepository PropertyRepository { get { return _propertyRepository = _propertyRepository ?? new PropertyService(_dbContext); } }

    public IStudentRepository StudentRepository { get { return _studentRepository = _studentRepository ?? new StudentService(_dbContext); } }

    public IBuildingRepository BuildingRepository { get { return _buildingRepository = _buildingRepository ?? new BuildingService(); } }

    public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _dbContext.DisposeAsync();
        }
    }

