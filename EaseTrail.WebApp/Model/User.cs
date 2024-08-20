using EaseTrail.WebApp.Inputs;
using EaseTrail.WebApp.Model.Enums;
using EaseTrail.WebApp.Model.Generic;
using System.Reflection.Metadata;

namespace EaseTrail.WebApp.Model
{
    public class User : AllClasses
    {
        public User() { }

        public User(User user) 
        {
            UserName = user.UserName;
            Name = user.Name;
            SecondName = user.SecondName;
            Email = user.Email;
            Password = user.Password;
            DocumentId = user.DocumentId;
            Status = user.Status;
            UserType = user.UserType;
            UpdateTime = DateTimeOffset.Now;
        }

        public User(string userName, string name, string secondName, string email, string password, string documentId, UserType userType)
        {
            UserName = userName;
            Name = name;
            SecondName = secondName;
            Email = email;
            Password = password;
            DocumentId = documentId;
            Status = Status.Active;
            UserType = userType;
            CreationTime = DateTimeOffset.Now;
            UpdateTime = DateTimeOffset.Now;
        }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DocumentId { get; set; }
        public Status Status { get; set; }
        public UserType UserType { get; set; }
    }
}
