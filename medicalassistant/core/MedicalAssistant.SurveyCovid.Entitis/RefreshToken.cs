using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAssistant.SurveyCovid.Entitis
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }

        public string Token { get; private set; }

        public DateTime Expires { get; private set; }

        public DateTime Created { get; private set; }

        public string CreatedByIp { get; private set; }

        public DateTime? Revoked { get; private set; }

        public string RevokedByIp { get; private set; }

        public string ReplacedByToken { get; private set; }

        public Guid UserId { get; private set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; private set; }

        public bool IsExpired => DateTime.UtcNow >= Expires;

        public bool IsActive => Revoked == null && !IsExpired;


        public void SetToken(string token)
        {
            Token = token;
        }

        public void SetExpires(DateTime expires)
        {
            Expires = expires;
        }

        public void SetCreated(DateTime created)
        {
            Created = created;
        }

        public void SetCreatedByIp(string createdbyIp)
        {
            CreatedByIp = createdbyIp;
        }

        public void SetRevoked(DateTime? revoked)
        {
            Revoked = revoked;
        }

        public void SetRevokedByIp(string revokedByIp)
        {
            RevokedByIp = revokedByIp;
        }

        public void SetReplacedByToken(string replacedByToken)
        {
            ReplacedByToken = replacedByToken;
        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
    }
}
