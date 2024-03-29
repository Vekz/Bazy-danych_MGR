﻿namespace VegeShama.Common.DatabaseModels.RavenDB
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string VAT_number { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        public List<Order> Orders { get; set; }
    }
}
