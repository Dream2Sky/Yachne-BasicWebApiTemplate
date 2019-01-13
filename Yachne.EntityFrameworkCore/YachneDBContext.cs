using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Yachne.Common;
using Yachne.Common.Encrypt;
using Yachne.Core;
using Yachne.Core.Entities;

namespace Yachne.EntityFrameworkCore
{
    public class YachneDBContext : DbContext
    {
        private IConfiguration configuration;

        public DbSet<UserEntity> UserEntities { get; set; }

        public YachneDBContext(DbContextOptions<YachneDBContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        public YachneDBContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = configuration["DB:DefaultConnectionString"];
                optionsBuilder.UseSqlServer(EncryptProvider.RSADecrypt(connectionString, YachneConsts.privateKey));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 设置默认值，目的是为了在 code-first 中设置ID递增的种子
            // 目前比较难看，以后看有没有方法改进
            string salt = Utils.GetRandomString();

            modelBuilder.Entity<UserEntity>().HasData(new UserEntity()
            {
                Id = 100001,
                UserName = "Yachne",
                Password = EncryptProvider.MD5Encrypt_32("888888" + salt),
                Salt = salt
            });
        }
    }
}
