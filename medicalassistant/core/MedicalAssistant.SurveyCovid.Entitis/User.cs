using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MedicalAssistant.SurveyCovid.Entitis
{
    public class User
    {
        public User(Guid id, string login, string password, string name)
        {
            Id = id;
            Login = login;
            HashPassword = password;
            Name = name;
            Status = true;
        }

        public User()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; private set; }

        [Required]
        public string Login { get; private set; }

        [Required]
        public string HashPassword { get; private set; }

        public string Name { get; private set; }

        public bool Status { get; private set; }

        public Guid RoleId { get; private set; }

        [Required]
        public virtual Role Role { get; private set; }

        public string HashComponent { get; private set; }

        public virtual List<RefreshToken> RefreshTokens { get; private set; }

        public void SetActive()
        {
            Status = true;
        }

        public void SetInactive()
        {
            Status = false;
        }

        public void SetHashPassword(string hashPassword)
        {
            HashPassword = hashPassword;
        }

        public void SetHashComponent(string hashComponent)
        {
            HashComponent = hashComponent;
        }

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            if (RefreshTokens != null)
            {
                RefreshTokens.Add(refreshToken);
            }
            else
            {
                RefreshTokens = new List<RefreshToken> { refreshToken };
            }
        }

        public RefreshToken GetRefreshToken(string token)
        {
            return RefreshTokens.SingleOrDefault(t => t.Token == token);
        }
    }



}

