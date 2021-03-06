﻿using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer
{
    public class NoteAppDbContext : DbContext
	{
		public DbSet<Note> Notes { get; set; }
		public DbSet<User> Users { get; set; }

		public NoteAppDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}
