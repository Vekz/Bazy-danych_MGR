﻿namespace VegeShama.Common.DatabaseModels.EFCore
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string VAT_number { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }

        public Customer Customer { get; set; }
    }
}
