using System;
using LegacyApp.Model;
using LegacyApp.Repository;

namespace LegacyApp.Service
{
    public class UserService
    {
        
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!VerifyNames(firstName, lastName)) return false;

            if (!VerifyEmail(email)) return false;

            if (!VerifyAge(dateOfBirth)) return false;

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == "VeryImportantClient") HandleVeryImportantClient(user);
            else if (client.Type == "ImportantClient") HandleImportantClient(user);
            else HandleClient(user);

            if (!VerifyClientLimit(user)) return false;

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool VerifyClientLimit(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }

        private void HandleVeryImportantClient(User user)
        {
            user.HasCreditLimit = false;
        }

        private void HandleClient(User user)
        {
            user.HasCreditLimit = true;
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }

        private void HandleImportantClient(User user)
        {
            using (var userCreditService = new UserCreditService())
            {
                int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
        }

        private bool VerifyAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            return true;
        }

        private bool VerifyEmail(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            return true;
        }

        private bool VerifyNames(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            return true;
        }
    }
}
