﻿using ProtoBuf;

namespace MN_Shop.Server.Models
{
    [ProtoContract]
    public class UserData
    {
        [ProtoMember(1, IsRequired = true)]
        public long Id { get; set; } = 0;

        [ProtoMember(2, IsRequired = true)]
        public string Name { get; set; } = string.Empty;

        [ProtoMember(3, IsRequired = true)]
        public string Surname { get; set; } = string.Empty;

        [ProtoMember(4, IsRequired = true)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// SHA256 Encrypted password
        /// </summary>
        [ProtoMember(5, IsRequired = true)]
        public string Password { get; set; } = string.Empty;

        [ProtoMember(6, IsRequired = true)]
        public EUserRole Role { get; set; } = EUserRole.Customer;

        [ProtoMember(7, IsRequired = true)]
        public int LanguageId { get; set; } = 2057; //en-GB //1062 lv-LV //1031 de-DE

        [ProtoMember(8, IsRequired = true)]
        public string DefaultTimeZone { get; set; } = "FLE Standard Time";

        [ProtoMember(9, IsRequired = true)]
        public bool UseDayLightSaving { get; set; } = true;

        [ProtoMember(10, IsRequired = true)]
        public long CreatedAtTicks { get; set; } = DateTime.MinValue.Ticks;
    }

    public enum EUserRole
    {
        Customer = 1,
        Admin = 2
    }
}
