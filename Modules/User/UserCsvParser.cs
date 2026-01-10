using HomeAppliancesStore.Shared;

namespace HomeAppliancesStore.Modules.User
{
    public class UserCsvParser : ICsvParser<UserEntity>
    {
        public UserEntity Parse(string line)
        {
            var parts = line.Split(',');

            return new UserEntity
            {
                Id = int.Parse(parts[0]),
                Email = parts[1],
                PasswordHash = parts[2],
                Balance = decimal.Parse(parts[3]),
                Role = parts[4]
            };
        }

        public string ToCsv(UserEntity entity)
        {
            return $"{entity.Id},{entity.Email},{entity.PasswordHash},{entity.Balance},{entity.Role}";
        }
    }
}