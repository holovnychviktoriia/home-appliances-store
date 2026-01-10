using System.Collections.Generic;
using System.Linq;
using HomeAppliancesStore.Modules.Config;
using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore.Modules.User
{
    public class UserService
    {
        private readonly CsvFileRepository<UserEntity> _userRepository;

        public UserService()
        {
            _userRepository = new CsvFileRepository<UserEntity>(
                ConfigConstants.UsersPath,
                new UserCsvParser()
            );
        }

        public List<UserEntity> GetAllUsers()
        {
            return _userRepository.ReadAll();
        }

        public UserEntity GetUserById(int id)
        {
            return _userRepository.ReadAll().FirstOrDefault(u => u.Id == id);
        }

        public void TopUpBalance(int userId, decimal amount)
        {
            var users = _userRepository.ReadAll();
            var user = users.FirstOrDefault(u => u.Id == userId);
            
            if (user != null)
            {
                user.Balance += amount;
                _userRepository.WriteAll(users, ConfigConstants.UsersHeader);
            }
        }

        public void UpdateUserBalance(int userId, decimal newBalance)
        {
            var users = GetAllUsers();
            var user = users.FirstOrDefault(u => u.Id == userId);
            
            if (user != null)
            {
                user.Balance = newBalance;
                _userRepository.WriteAll(users, ConfigConstants.UsersHeader);
            }
        }
    }
}