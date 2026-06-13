using Microsoft.EntityFrameworkCore;
using Reverse_Chatbot.Interfaces;
using Reverse_Chatbot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_Chatbot.Services
{   
    //嗯,还是有机会用上比较复杂的结构的
    internal class ContextFactory:IMemoryContextFactory
    {
        public MemoryContext CreateContext()
        {
            return new MemoryContext();
        }
    }

    internal class MemoryContext:DbContext
    {
        public DbSet<DeepSeekMessageEntity> DeepSeekMessageEntities { get; set; }

        public MemoryContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("DataSource = DeepSeekMemory.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }


}
