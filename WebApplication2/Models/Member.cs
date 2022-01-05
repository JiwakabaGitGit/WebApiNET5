using System;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
  public class memberopContext : DbContext
  {
        public memberopContext(DbContextOptions options) : base(options)
        {
        }

        public memberopContext() : base()
        {
        }

        public DbSet<Member> Members { get; set; }
  }

  [Keyless]
  public class Member
  {
    public int Id { get; set; }

    [DisplayName("����")]
    public string Name { get; set; }

    [DisplayName("���[���A�h���X")]
    public string Email { get; set; }

    [DisplayName("���N����")]
    //public DateTime Birth { get; set; }
    public string Birth { get; set; }

    [DisplayName("����")]
    public bool Married { get; set; }

    [DisplayName("���ȏЉ�")]
    public string Memo { get; set; }
  }
}
