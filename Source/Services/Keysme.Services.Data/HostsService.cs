﻿namespace Keysme.Services.Data
{
    using Keysme.Data;
    using Keysme.Data.Models;

    public class HostsService : IHostsService
    {
        private readonly IRepository<User> users;
        private readonly IRepository<Host> hosts;

        public HostsService(IRepository<User> users, IRepository<Host> hosts)
        {
            this.users = users;
            this.hosts = hosts;
        }

        public void Create(string userId, Host host)
        {
            var user = this.users.GetById(userId);
            user.Hosts.Add(host);
            this.users.SaveChanges();
        }
    }
}
