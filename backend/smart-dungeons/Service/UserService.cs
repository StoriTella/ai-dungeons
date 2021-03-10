using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Security.Cryptography;
using smart_dungeons.Domain.Shared;
using smart_dungeons.DTO;
using smart_dungeons.Helpers;
using System.Text;
using System.Linq;

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


        public async Task<UserDTO> GetByUsernameAsync(string username)
        {
            var User = await this._repo.GetByUsernameAsync(username);

            if(User == null)
                return null;

            return new UserDTO(User.Id.AsGuid(),
                       User.Username,
                       User.Email);
        }

        public async Task<UserDTO> AddAsync(UserRegisterDTO dto)
        {
            User User = createHashcode(dto);
            User.UserId = Guid.NewGuid();
            await this._repo.AddAsync(User);

            await this._unitOfWork.CommitAsync();

            return new UserDTO(User.Id.AsGuid(),
                               User.Username,
                               User.Email);

        }

        public async Task<bool> Login(UserLoginDTO dto) {
            bool retBool = false;

            byte[] pwd = Encoding.Unicode.GetBytes(dto.Password);

            User u = await _repo.GetByUsernameAsync(dto.Username);

            if (u == null) {
                return retBool;
            }

            byte[] salt = u.Salt;

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            try
            {
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(pwd, salt);

                tdes.Key = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, tdes.IV);

                retBool = tdes.Key.SequenceEqual(u.Hashcode);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Clear the buffers
                PasswordManager.ClearBytes(pwd);
                PasswordManager.ClearBytes(salt);

                // Clear the key.
                tdes.Clear();

            }

            return retBool;
        }

        public User createHashcode(UserRegisterDTO dto) {
            User retUser = new User(dto.Username,
                                    dto.Email);

            byte[] pwd = Encoding.Unicode.GetBytes(dto.Password);

            byte[] salt = PasswordManager.CreateRandomSalt(7);

            // Create a TripleDESCryptoServiceProvider object.
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            try
            {
                // Create a PasswordDeriveBytes object and then create
                // a TripleDES key from the password and salt.
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(pwd, salt);

                // Create the key and set it to the Key property
                // of the TripleDESCryptoServiceProvider object.
                tdes.Key = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, tdes.IV);

                retUser.Salt = pdb.Salt;
                retUser.Hashcode = tdes.Key;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Clear the buffers
                PasswordManager.ClearBytes(pwd);
                PasswordManager.ClearBytes(salt);

                // Clear the key.
                tdes.Clear();
            }

            return retUser;
        }
    }
}