using System.Threading.Tasks;
using System.Collections.Generic;
using smart_dungeons.Domain.Shared;
using smart_dungeons.Domain.Users;
using smart_dungeons.DTO;

namespace smart_dungeons.Domain.Users
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repo;

        public UserService(IUnitOfWork unitOfWork, IUserRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<UserDTO> listDto = list.ConvertAll<UserDTO>(User => 
                new UserDTO(User.Id.AsGuid(),
                            User.Username,
                            User.Email));

            return listDto;
        }

        public async Task<UserDTO> GetByIdAsync(UserId id)
        {
            var User = await this._repo.GetByIdAsync(id);
            
            if(User == null)
                return null;

            return new UserDTO(User.Id.AsGuid(),
                       User.Username,
                       User.Email);
        }

        public async Task<UserDTO> AddAsync(UserLoginDTO dto)
        {
            User User = createHashcode(dto);
            await this._repo.AddAsync(User);

            await this._unitOfWork.CommitAsync();

            return new UserDTO(User.Id.AsGuid(),
                               User.Username,
                               User.Email);
        
        }

        public User createHashcode(UserLoginDTO dto) {
            //temp
            return new User(dto.Username,
                            dto.Email,
                            "random",
                            "random2");
        }
    }
}